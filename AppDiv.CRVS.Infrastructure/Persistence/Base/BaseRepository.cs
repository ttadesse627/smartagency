using AppDiv.CRVS.Application.Interfaces.Persistence;
using AppDiv.CRVS.Application.Interfaces.Persistence.Base;
using AppDiv.CRVS.Domain.Base;
using AppDiv.CRVS.Utility.Contracts;
using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Reflection;

namespace AppDiv.CRVS.Infrastructure.Persistence
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private static readonly MethodInfo OrderByAscMethod = typeof(Queryable).GetMethods()
           .Where(method => method.Name == "OrderBy")
           .Where(method => method.GetParameters().Length == 2)
           .Single();

        private static readonly MethodInfo OrderByDescMethod = typeof(Queryable).GetMethods()
           .Where(method => method.Name == "OrderByDescending")
           .Where(method => method.GetParameters().Length == 2)
           .Single();

        private readonly CRVSDbContext _dbContext;
        public BaseRepository(CRVSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllWithAsync(params string[] eagerLoadedProperties)
        {
            var list = _dbContext.Set<T>().AsQueryable();
            foreach (var nav_property in eagerLoadedProperties)
            {
                list = list.Include(nav_property);
            }
            return await list.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllWithAsync(Expression<Func<T, bool>> predicate = null, params string[] eagerLoadedProperties)
        {
            var entiries = _dbContext.Set<T>().AsQueryable();
            foreach (var nav_property in eagerLoadedProperties)
            {
                entiries = entiries.Include(nav_property);
            }
            return await entiries.Where(predicate).ToListAsync();
        }

        public virtual T GetAtIndex(int i)
        {
            return _dbContext.Set<T>().AsQueryable().ElementAt(i);
        }

        public virtual T GetAtIndexWith(int i, params string[] eagerLoadedProperties)
        {
            var entiries = _dbContext.Set<T>().AsQueryable();
            foreach (var nav_property in eagerLoadedProperties)
            {
                entiries = entiries.Include(nav_property);
            }
            return entiries.ElementAt(i);
        }

        public virtual async Task<T> GetAsync(object id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetWithAsync(object id, Dictionary<String, NavigationPropertyType> explicitLoadedProperties)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                foreach (var nav_property in explicitLoadedProperties)
                {
                    switch (nav_property.Value)
                    {
                        case NavigationPropertyType.REFERENCE:
                            await this._dbContext.Entry(entity).Reference(nav_property.Key).LoadAsync();
                            break;
                        default:
                            await this._dbContext.Entry(entity).Collection(nav_property.Key).LoadAsync();
                            break;
                    }
                }
            }
            return entity;
        }

        public virtual async Task<T> GetAsync(object[] id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetWithAsync(object[] id, Dictionary<string, NavigationPropertyType> explicitLoadedProperties)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                foreach (var nav_property in explicitLoadedProperties)
                {
                    switch (nav_property.Value)
                    {
                        case NavigationPropertyType.REFERENCE:
                            await this._dbContext.Entry(entity).Reference(nav_property.Key).LoadAsync();
                            break;
                        default:
                            await this._dbContext.Entry(entity).Collection(nav_property.Key).LoadAsync();
                            break;
                    }
                }
            }
            return entity;
        }

        public virtual async Task InsertAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public virtual async Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task DeleteAsync(object id)
        {
            T entityToDelete = await _dbContext.Set<T>().FindAsync(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public virtual async Task DeleteAsync(IEnumerable<object> ids)
        {
            List<T> entities = new List<T> { };
            foreach (var id in ids)
            {
                T entityToDelete = await _dbContext.Set<T>().FindAsync(id);
                entities.Add(entityToDelete);
            }
            if (entities != null && entities.Count() > 0)
            {
                Delete(entities);
            }
        }

        public virtual async Task DeleteAsync(IEnumerable<object[]> ids)
        {
            List<T> entities = new List<T> { };
            foreach (var id in ids)
            {
                T entityToDelete = await _dbContext.Set<T>().FindAsync(id);
                entities.Add(entityToDelete);
            }
            if (entities != null && entities.Count() > 0)
            {
                Delete(entities);
            }
        }

        public virtual async Task DeleteAsync(object[] id)
        {
            T entityToDelete = await _dbContext.Set<T>().FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entity)
        {

            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entity);
            }
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate = null)
        {
            IEnumerable<T> list = _dbContext.Set<T>().Where(predicate);
            if (list != null)
            {
                _dbContext.Set<T>().RemoveRange(list);
            }
        }

        public virtual void Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Set<T>().Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        /*
         * Check if an entity with the same key is already tracked by the _dbContext and modify that entity instead of attaching the current one:
         *   This is to overcome an error:
         *      object with the same key already exists in the ObjectStateManager. The ObjectStateManager cannot track multiple objects with the same key
         */

        public virtual async Task UpdateAsync(T entity, Func<T, object> getKey)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = _dbContext.Entry<T>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = _dbContext.Set<T>();
                T attachedEntity = await set.FindAsync(getKey(entity));

                if (attachedEntity != null)
                {
                    var attachedEntry = _dbContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
        }

        public virtual async Task UpdateAsync(T entity, Func<T, object[]> getKey)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = _dbContext.Entry<T>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = _dbContext.Set<T>();
                T attachedEntity = await set.FindAsync(getKey(entity));

                if (attachedEntity != null)
                {
                    var attachedEntry = _dbContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
        }

        public virtual async Task UpdateAsync(IEnumerable<T> entities, Func<T, object> getKey)
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    throw new ArgumentException("Cannot add a null entity.");
                }
                var entry = _dbContext.Entry<T>(entity);

                if (entry.State == EntityState.Detached)
                {
                    var set = _dbContext.Set<T>();
                    T attachedEntity = await set.FindAsync(getKey(entity));

                    if (attachedEntity != null)
                    {
                        var attachedEntry = _dbContext.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        entry.State = EntityState.Modified; // This should attach entity
                    }
                }
            }
        }

        public virtual async Task UpdateAsync(T entity, Func<T, int> getKey)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = _dbContext.Entry<T>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = _dbContext.Set<T>();
                T attachedEntity = await set.FindAsync(getKey(entity));

                if (attachedEntity != null)
                {
                    var attachedEntry = _dbContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
        }

        public virtual async Task UpdateAsync(IEnumerable<T> entities, Func<T, int> getKey)
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    throw new ArgumentException("Cannot add a null entity.");
                }
                var entry = _dbContext.Entry<T>(entity);

                if (entry.State == EntityState.Detached)
                {
                    var set = _dbContext.Set<T>();
                    T attachedEntity = await set.FindAsync(getKey(entity));

                    if (attachedEntity != null)
                    {
                        var attachedEntry = _dbContext.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        entry.State = EntityState.Modified; // This should attach entity
                    }
                }
            }
        }

        public virtual async Task<int> GetCountAsyc(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                return await _dbContext.Set<T>().CountAsync(predicate);
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public virtual async Task<Dictionary<String, int>> GetGroupedCountAsync(Expression<Func<T, string>> keySelector)
        {
            return await _dbContext.Set<T>().AsQueryable().GroupBy(keySelector).Select(g => new { name = g.Key, count = g.Count() }).ToDictionaryAsync(k => k.name, i => i.count);
        }

        public virtual async Task<Dictionary<String, int>> GetGroupedCountAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> keySelector)
        {
            return await _dbContext.Set<T>().Where(predicate).AsQueryable().GroupBy(keySelector).Select(g => new { name = g.Key, count = g.Count() }).ToDictionaryAsync(k => k.name, i => i.count);
        }

        public virtual async Task<T> GetFirstEntryAsync()
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync();
        }

        public virtual async Task<T> GetFirstEntryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, SortingDirection sorting_direction)
        {

            var propertyExpression = orderBy.Body is UnaryExpression ? (MemberExpression)((UnaryExpression)orderBy.Body).Operand : (MemberExpression)orderBy.Body;

            var parameters = orderBy.Parameters;

            if (propertyExpression.Type == typeof(string))
            {
                var newExpression = Expression.Lambda<Func<T, string>>(propertyExpression, parameters);
                return await (sorting_direction == SortingDirection.Ascending ? _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).FirstOrDefaultAsync() : _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(DateTime))
            {
                var newExpression = Expression.Lambda<Func<T, DateTime>>(propertyExpression, parameters);
                return await (sorting_direction == SortingDirection.Ascending ? _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).FirstOrDefaultAsync() : _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(DateTime?))
            {
                var newExpression = Expression.Lambda<Func<T, DateTime?>>(propertyExpression, parameters);
                return await (sorting_direction == SortingDirection.Ascending ? _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).FirstOrDefaultAsync() : _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(int))
            {
                var newExpression = Expression.Lambda<Func<T, int>>(propertyExpression, parameters);
                return await (sorting_direction == SortingDirection.Ascending ? _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).FirstOrDefaultAsync() : _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(bool))
            {
                var newExpression = Expression.Lambda<Func<T, bool>>(propertyExpression, parameters);
                return await (sorting_direction == SortingDirection.Ascending ? _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).FirstOrDefaultAsync() : _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(Decimal))
            {
                var newExpression = Expression.Lambda<Func<T, Decimal>>(propertyExpression, parameters);
                return await (sorting_direction == SortingDirection.Ascending ? _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).FirstOrDefaultAsync() : _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(Double))
            {
                var newExpression = Expression.Lambda<Func<T, Double>>(propertyExpression, parameters);
                return await (sorting_direction == SortingDirection.Ascending ? _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).FirstOrDefaultAsync() : _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(long))
            {
                var newExpression = Expression.Lambda<Func<T, long>>(propertyExpression, parameters);
                return await (sorting_direction == SortingDirection.Ascending ? _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).FirstOrDefaultAsync() : _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type.IsEnum)
            {
                return await (sorting_direction == SortingDirection.Ascending ? _dbContext.Set<T>().Where(predicate).AsQueryable().OrderBy(q => propertyExpression.Member.Name).FirstOrDefaultAsync() : _dbContext.Set<T>().Where(predicate).AsQueryable().OrderByDescending(q => propertyExpression.Member.Name).FirstOrDefaultAsync());
            }

            else
            {
                throw new Exception("Not implemented");
            }

        }

        public virtual async Task<int> GetLastIndexAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<T> GetLastEntryAsync()
        {
            return await _dbContext.Set<T>().LastOrDefaultAsync();
        }

        public virtual async Task<T> GetLastEntryWithAsync(params string[] eagerLoadedProperties)
        {
            var entiries = _dbContext.Set<T>().AsQueryable();
            foreach (var nav_property in eagerLoadedProperties)
            {
                entiries = entiries.Include(nav_property);
            }
            return await entiries.LastOrDefaultAsync();
        }

        public virtual async Task<int> GetMaxPageAsync(int pagingSize)
        {
            return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(await _dbContext.Set<T>().CountAsync() / (pagingSize * 1.0))));
        }

        public virtual async Task<T> GetLastObjectAsync()
        {
            return await _dbContext.Set<T>().LastOrDefaultAsync();
        }

        public virtual async Task<T> GetLastObjectWithAsync(params string[] eagerLoadedProperties)
        {
            var entiries = _dbContext.Set<T>().AsQueryable();
            foreach (var nav_property in eagerLoadedProperties)
            {
                entiries = entiries.Include(nav_property);
            }
            return await entiries.LastOrDefaultAsync();
        }

        public virtual async Task<SearchModel<T>> GetPagedSearchResultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, int page = 0, int pageSize = 15, SortingDirection sorting_direction = SortingDirection.Ascending)
        {
            IEnumerable<T> list = null;
            long maxPage = 1;
            long total_items = await _dbContext.Set<T>().LongCountAsync(predicate);

            if (total_items > 0)
            {
                maxPage = Convert.ToInt64(Math.Ceiling(Convert.ToDouble(total_items) / pageSize));
                if (page >= maxPage)
                {
                    page = Convert.ToInt32(maxPage);
                }
                var propertyExpression = orderBy.Body is UnaryExpression ? (MemberExpression)((UnaryExpression)orderBy.Body).Operand : (MemberExpression)orderBy.Body;

                var parameters = orderBy.Parameters;

                if (propertyExpression.Type == typeof(string))
                {
                    var newExpression = Expression.Lambda<Func<T, string>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime?))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(int))
                {
                    var newExpression = Expression.Lambda<Func<T, int>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(bool))
                {
                    var newExpression = Expression.Lambda<Func<T, bool>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Decimal))
                {
                    var newExpression = Expression.Lambda<Func<T, Decimal>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Double))
                {
                    var newExpression = Expression.Lambda<Func<T, Double>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(long))
                {
                    var newExpression = Expression.Lambda<Func<T, long>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type.IsEnum)
                {
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).AsQueryable().OrderBy(q => propertyExpression.Member.Name).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await _dbContext.Set<T>().Where(predicate).AsQueryable().OrderByDescending(q => propertyExpression.Member.Name).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else
                {
                    throw new Exception("Not implemented");
                }
            }

            return new SearchModel<T>
            {
                CurrentPage = page,
                MaxPage = maxPage,
                PagingSize = pageSize,
                Entities = list,
                TotalItems = total_items
            };
        }

        public virtual async Task<SearchModel<T>> GetSearchResultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, SortingDirection sorting_direction = SortingDirection.Ascending)
        {
            IEnumerable<T> list = null;
            long total_items = await _dbContext.Set<T>().LongCountAsync(predicate);

            if (total_items > 0)
            {

                var propertyExpression = orderBy.Body is UnaryExpression ? (MemberExpression)((UnaryExpression)orderBy.Body).Operand : (MemberExpression)orderBy.Body;

                var parameters = orderBy.Parameters;

                if (propertyExpression.Type == typeof(string))
                {
                    var newExpression = Expression.Lambda<Func<T, string>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime?))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(int))
                {
                    var newExpression = Expression.Lambda<Func<T, int>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(bool))
                {
                    var newExpression = Expression.Lambda<Func<T, bool>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Decimal))
                {
                    var newExpression = Expression.Lambda<Func<T, Decimal>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Double))
                {
                    var newExpression = Expression.Lambda<Func<T, Double>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(long))
                {
                    var newExpression = Expression.Lambda<Func<T, long>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type.IsEnum)
                {
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).AsQueryable().OrderBy(q => propertyExpression.Member.Name).ToListAsync() : await _dbContext.Set<T>().Where(predicate).AsQueryable().OrderByDescending(q => propertyExpression.Member.Name).ToListAsync();
                }

                else
                {
                    throw new Exception("Not implemented");
                }
            }

            return new SearchModel<T>
            {
                Entities = list,
            };
        }

        public virtual async Task<SearchModel<T>> GetPagedSearchResultWithAsync(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, int page = 0, int pageSize = 0, SortingDirection sorting_direction = SortingDirection.Descending, params string[] eagerLoadedProperties)
        {
            IEnumerable<T> list = null;
            long total_items = await _dbContext.Set<T>().LongCountAsync(predicate);
            var entiries = _dbContext.Set<T>().Where(predicate).AsQueryable();
            foreach (var nav_property in eagerLoadedProperties)
            {
                entiries = entiries.Include(nav_property);
            }

            int maxPage = 1;
            if (total_items > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(total_items) / pageSize));
                if (page >= maxPage)
                {
                    page = Convert.ToInt32(maxPage);
                }

                var propertyExpression = orderBy.Body is UnaryExpression ? (MemberExpression)((UnaryExpression)orderBy.Body).Operand : (MemberExpression)orderBy.Body;
                var parameters = orderBy.Parameters;

                if (propertyExpression.Type == typeof(string))
                {
                    var newExpression = Expression.Lambda<Func<T, string>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime?))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(int))
                {
                    var newExpression = Expression.Lambda<Func<T, int>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(bool))
                {
                    var newExpression = Expression.Lambda<Func<T, bool>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Decimal))
                {
                    var newExpression = Expression.Lambda<Func<T, Decimal>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }
                else if (propertyExpression.Type == typeof(Decimal?))
                {
                    var newExpression = Expression.Lambda<Func<T, Decimal?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Double))
                {
                    var newExpression = Expression.Lambda<Func<T, Double>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Double?))
                {
                    var newExpression = Expression.Lambda<Func<T, Double?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(long))
                {
                    var newExpression = Expression.Lambda<Func<T, long>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else if (propertyExpression.Type.IsEnum)
                {
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(q => propertyExpression.Member.Name).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync() : await entiries.OrderByDescending(q => propertyExpression.Member.Name).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).ToListAsync();
                }

                else
                {
                    throw new Exception("Not implemented");
                }
            }

            return new SearchModel<T>
            {
                CurrentPage = page,
                MaxPage = maxPage,
                PagingSize = pageSize,
                Entities = list,
                TotalItems = total_items
            };
        }

        public virtual async Task<SearchModel<T>> GetSearchResultWithAsync(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, SortingDirection sorting_direction = SortingDirection.Descending, params string[] eagerLoadedProperties)
        {
            IEnumerable<T> list = null;
            long total_items = await _dbContext.Set<T>().LongCountAsync(predicate);
            var entiries = _dbContext.Set<T>().Where(predicate).AsQueryable();
            foreach (var nav_property in eagerLoadedProperties)
            {
                entiries = entiries.Include(nav_property);
            }

            int maxPage = 1;
            if (total_items > 0)
            {

                var propertyExpression = orderBy.Body is UnaryExpression ? (MemberExpression)((UnaryExpression)orderBy.Body).Operand : (MemberExpression)orderBy.Body;
                var parameters = orderBy.Parameters;

                if (propertyExpression.Type == typeof(string))
                {
                    var newExpression = Expression.Lambda<Func<T, string>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).ToListAsync() : await entiries.OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).ToListAsync() : await entiries.OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime?))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).ToListAsync() : await entiries.OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(int))
                {
                    var newExpression = Expression.Lambda<Func<T, int>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).ToListAsync() : await entiries.OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(bool))
                {
                    var newExpression = Expression.Lambda<Func<T, bool>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).ToListAsync() : await entiries.OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Decimal))
                {
                    var newExpression = Expression.Lambda<Func<T, Decimal>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).ToListAsync() : await entiries.OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Double))
                {
                    var newExpression = Expression.Lambda<Func<T, Double>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).ToListAsync() : await entiries.OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(long))
                {
                    var newExpression = Expression.Lambda<Func<T, long>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).ToListAsync() : await entiries.OrderByDescending(newExpression).ToListAsync();
                }

                else if (propertyExpression.Type.IsEnum)
                {
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(q => propertyExpression.Member.Name).ToListAsync() : await entiries.OrderByDescending(q => propertyExpression.Member.Name).ToListAsync();
                }

                else
                {
                    throw new Exception("Not implemented");
                }
            }

            return new SearchModel<T>
            {
                Entities = list
            };
        }

        public virtual async Task<SearchModel<T2>> GetPagedSearchResultAsync<T2>(Expression<Func<T, T2>> select, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, int page = 0, int pageSize = 15, SortingDirection sorting_direction = SortingDirection.Ascending) where T2 : class
        {
            IEnumerable<T2> list = null;
            long maxPage = 1;
            long total_items = await _dbContext.Set<T>().LongCountAsync(predicate);

            if (total_items > 0)
            {
                maxPage = Convert.ToInt64(Math.Ceiling(Convert.ToDouble(total_items) / pageSize));
                if (page >= maxPage)
                {
                    page = Convert.ToInt32(maxPage);
                }
                var propertyExpression = orderBy.Body is UnaryExpression ? (MemberExpression)((UnaryExpression)orderBy.Body).Operand : (MemberExpression)orderBy.Body;

                var parameters = orderBy.Parameters;

                if (propertyExpression.Type == typeof(string))
                {
                    var newExpression = Expression.Lambda<Func<T, string>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime?))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(int))
                {
                    var newExpression = Expression.Lambda<Func<T, int>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(bool))
                {
                    var newExpression = Expression.Lambda<Func<T, bool>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Decimal))
                {
                    var newExpression = Expression.Lambda<Func<T, Decimal>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Decimal?))
                {
                    var newExpression = Expression.Lambda<Func<T, Decimal?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Double))
                {
                    var newExpression = Expression.Lambda<Func<T, Double>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Double?))
                {
                    var newExpression = Expression.Lambda<Func<T, Double?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(long))
                {
                    var newExpression = Expression.Lambda<Func<T, long>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().Where(predicate).OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type.IsEnum)
                {
                    list = sorting_direction == SortingDirection.Ascending ? await _dbContext.Set<T>().AsQueryable().OrderBy(q => propertyExpression.Member.Name).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await _dbContext.Set<T>().AsQueryable().OrderByDescending(q => propertyExpression.Member.Name).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else
                {
                    throw new Exception("Not implemented");
                }
            }

            return new SearchModel<T2>
            {
                CurrentPage = page,
                MaxPage = maxPage,
                PagingSize = pageSize,
                Entities = list,
                TotalItems = total_items
            };
        }

        public virtual async Task<SearchModel<T2>> GetPagedSearchResultWithAsync<T2>(Expression<Func<T, T2>> select, Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, int page = 0, int pageSize = 0, SortingDirection sorting_direction = SortingDirection.Descending, params string[] eagerLoadedProperties) where T2 : class
        {
            IEnumerable<T2> list = null;
            long total_items = await _dbContext.Set<T>().LongCountAsync(predicate);
            var entiries = _dbContext.Set<T>().Where(predicate).AsQueryable();
            foreach (var nav_property in eagerLoadedProperties)
            {
                entiries = entiries.Include(nav_property);
            }

            int maxPage = 1;
            if (total_items > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(total_items) / pageSize));
                if (page >= maxPage)
                {
                    page = Convert.ToInt32(maxPage);
                }

                var propertyExpression = orderBy.Body is UnaryExpression ? (MemberExpression)((UnaryExpression)orderBy.Body).Operand : (MemberExpression)orderBy.Body;
                var parameters = orderBy.Parameters;

                if (propertyExpression.Type == typeof(string))
                {
                    var newExpression = Expression.Lambda<Func<T, string>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(DateTime?))
                {
                    var newExpression = Expression.Lambda<Func<T, DateTime?>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(int))
                {
                    var newExpression = Expression.Lambda<Func<T, int>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(bool))
                {
                    var newExpression = Expression.Lambda<Func<T, bool>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Decimal))
                {
                    var newExpression = Expression.Lambda<Func<T, Decimal>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(Double))
                {
                    var newExpression = Expression.Lambda<Func<T, Double>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await entiries.OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type == typeof(long))
                {
                    var newExpression = Expression.Lambda<Func<T, long>>(propertyExpression, parameters);
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await entiries.Where(predicate).OrderByDescending(newExpression).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else if (propertyExpression.Type.IsEnum)
                {
                    list = sorting_direction == SortingDirection.Ascending ? await entiries.OrderBy(q => propertyExpression.Member.Name).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync() : await entiries.OrderByDescending(q => propertyExpression.Member.Name).Skip((page != 0 ? (page - 1) : 0) * pageSize).Take(pageSize).Select(select).ToListAsync();
                }

                else
                {
                    throw new Exception("Not implemented");
                }
            }

            return new SearchModel<T2>
            {
                CurrentPage = page,
                MaxPage = maxPage,
                PagingSize = pageSize,
                Entities = list,
                TotalItems = total_items
            };
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> orderBy, int skip, int limit)
        {
            return await this._dbContext.Set<T>().OrderBy(orderBy).Skip(skip).Take(limit).ToListAsync();
        }

        public virtual bool SaveChanges()
        {
            this._dbContext.SaveChanges();
            return true;
        }

        public virtual async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        {
            // Get all the entities that inherit from AuditableEntity
            // and have a state of Added or Modified
            var entries = _dbContext.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseAuditableEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            // For each entity we will set the Audit properties
            foreach (var entityEntry in entries)
            {
                // If the entity state is Added let's set
                // the CreatedAt and CreatedBy properties
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseAuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseAuditableEntity)entityEntry.Entity).CreatedBy = _dbContext.GetCurrentUserId();
                }
                else
                {
                    // If the state is Modified then we don't want
                    // to modify the CreatedAt and CreatedBy properties
                    // so we set their state as IsModified to false
                   _dbContext.Entry((BaseAuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    _dbContext.Entry((BaseAuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }

                // In any case we always want to set the properties
                // ModifiedAt and ModifiedBy
                ((BaseAuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((BaseAuditableEntity)entityEntry.Entity).ModifiedBy = _dbContext.GetCurrentUserId();
            }

            // After we set all the needed properties
            // we call the base implementation of SaveChangesAsync
            // to actually save our entities in the database
             await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<T> GetFirstEntryWithAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, SortingDirection sorting_direction, params string[] eagerLoadedProperties)
        {

            var propertyExpression = orderBy.Body is UnaryExpression ? (MemberExpression)((UnaryExpression)orderBy.Body).Operand : (MemberExpression)orderBy.Body;

            var parameters = orderBy.Parameters;

            var entiries = _dbContext.Set<T>().Where(predicate).AsQueryable();

            foreach (var nav_property in eagerLoadedProperties)
            {
                entiries = entiries.Include(nav_property);
            }

            if (propertyExpression.Type == typeof(string))
            {
                var newExpression = Expression.Lambda<Func<T, string>>(propertyExpression, parameters);
                return await(sorting_direction == SortingDirection.Ascending ? entiries.OrderBy(newExpression).FirstOrDefaultAsync() : entiries.OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(DateTime))
            {
                var newExpression = Expression.Lambda<Func<T, DateTime>>(propertyExpression, parameters);
                return await(sorting_direction == SortingDirection.Ascending ? entiries.OrderBy(newExpression).FirstOrDefaultAsync() : entiries.OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(DateTime?))
            {
                var newExpression = Expression.Lambda<Func<T, DateTime?>>(propertyExpression, parameters);
                return await(sorting_direction == SortingDirection.Ascending ? entiries.OrderBy(newExpression).FirstOrDefaultAsync() : entiries.OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(int))
            {
                var newExpression = Expression.Lambda<Func<T, int>>(propertyExpression, parameters);
                return await(sorting_direction == SortingDirection.Ascending ? entiries.OrderBy(newExpression).FirstOrDefaultAsync() : entiries.OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(bool))
            {
                var newExpression = Expression.Lambda<Func<T, bool>>(propertyExpression, parameters);
                return await(sorting_direction == SortingDirection.Ascending ? entiries.OrderBy(newExpression).FirstOrDefaultAsync() : entiries.OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(Decimal))
            {
                var newExpression = Expression.Lambda<Func<T, Decimal>>(propertyExpression, parameters);
                return await(sorting_direction == SortingDirection.Ascending ? entiries.OrderBy(newExpression).FirstOrDefaultAsync() : entiries.OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(Double))
            {
                var newExpression = Expression.Lambda<Func<T, Double>>(propertyExpression, parameters);
                return await(sorting_direction == SortingDirection.Ascending ? entiries.OrderBy(newExpression).FirstOrDefaultAsync() : entiries.OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type == typeof(long))
            {
                var newExpression = Expression.Lambda<Func<T, long>>(propertyExpression, parameters);
                return await(sorting_direction == SortingDirection.Ascending ? entiries.OrderBy(newExpression).FirstOrDefaultAsync() : entiries.OrderByDescending(newExpression).FirstOrDefaultAsync());
            }

            else if (propertyExpression.Type.IsEnum)
            {
                return await(sorting_direction == SortingDirection.Ascending ? entiries.AsQueryable().OrderBy(q => propertyExpression.Member.Name).FirstOrDefaultAsync() : entiries.AsQueryable().OrderByDescending(q => propertyExpression.Member.Name).FirstOrDefaultAsync());
            }

            else
            {
                throw new Exception("Not implemented");
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await this._dbContext.Set<T>().AnyAsync(predicate);
        }
    }
}
