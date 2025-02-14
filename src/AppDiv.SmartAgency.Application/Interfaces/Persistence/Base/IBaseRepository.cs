﻿using AppDiv.SmartAgency.Utility.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence.Base
{
    public interface IBaseRepository<T> where T : class
    {

        public IQueryable<T> GetAll();

        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> Ids, Expression<Func<T, bool>>? predicate = null, params string[] eagerLoadedProperties);
        void Delete(Expression<Func<T, bool>>? predicate = null);
        void Delete(IEnumerable<T> entities);
        void Delete(T entity);
        Task DeleteAsync(IEnumerable<object[]> ids);
        Task DeleteAsync(IEnumerable<object> ids);
        Task DeleteAsync(object id);
        Task DeleteAsync(object[] id);
        Task<Int32> DeleteMany(List<Guid> ids);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> orderBy, int skip, int limit);
        Task<IEnumerable<T>> GetAllWithAsync(Expression<Func<T, bool>>? predicate = null, params string[] eagerLoadedProperties);
        Task<IEnumerable<T>> GetAllWithAsync(params string[] eagerLoadedProperties);
        Task<IQueryable<T>> GetAllWithSearchAsync(string? searchTerm = null, Expression<Func<T, bool>>? predicate = null, params string[] eagerLoadedProperties);
        Task<SearchModel<T>> GetAllWithPredicateFilterAsync(int pageNumber, int pageSize, List<Filter>? filters = null, Expression<Func<T, bool>>? predicate = null, params string[] eagerLoadedProperties);
        Task<SearchModel<T>> PaginateItems(int pageNumber, int pageSize, SortingDirection sortingDirection, IQueryable<T> items, string? orderBy = null);
        Task<T> GetWithPredicateAsync(Expression<Func<T, bool>>? predicate = null, params string[] eagerLoadedProperties);
        Task<List<T>> GetAllWithPredicateAsync(Expression<Func<T, bool>>? predicate = null, params string[] eagerLoadedProperties);

        Task<SearchModel<T>> GetAllWithPredicateSearchAsync(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection, Expression<Func<T, bool>>? predicate = null, params string[] eagerLoadedProperties);
        Task<T?> GetAsync(object id);
        Task<T?> GetAsync(object[] id);
        T GetAtIndex(int i);
        T GetAtIndexWith(int i, params string[] eagerLoadedProperties);
        Task<int> GetCountAsyc(Expression<Func<T, bool>>? predicate = null);
        Task<T?> GetFirstEntryAsync();
        Task<T?> GetFirstEntryAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, SortingDirection sorting_direction = SortingDirection.Ascending);
        Task<T?> GetFirstEntryWithAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, SortingDirection sorting_direction = SortingDirection.Ascending, params string[] eagerLoadedProperties);
        Task<Dictionary<string, int>> GetGroupedCountAsync(Expression<Func<T, string>>? keySelector = null);
        Task<Dictionary<string, int>> GetGroupedCountAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, string>>? keySelector = null);
        Task<T?> GetLastEntryAsync();
        Task<T?> GetLastEntryWithAsync(params string[] eagerLoadedProperties);
        Task<int> GetLastIndexAsync();
        Task<T?> GetLastObjectAsync();
        Task<T?> GetLastObjectWithAsync(params string[] eagerLoadedProperties);
        Task<int> GetMaxPageAsync(int pagingSize);
        Task<SearchModel<T>> GetPagedSearchResultAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, int page = 0, int pageSize = 15, SortingDirection sorting_direction = SortingDirection.Ascending);
        Task<SearchModel<T2>> GetPagedSearchResultAsync<T2>(Expression<Func<T, T2>>? select = null, Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, int page = 0, int pageSize = 15, SortingDirection sorting_direction = SortingDirection.Ascending) where T2 : class;
        Task<SearchModel<T>> GetPagedSearchResultWithAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, int page = 0, int pageSize = 0, SortingDirection sorting_direction = SortingDirection.Descending, params string[] eagerLoadedProperties);
        Task<SearchModel<T2>> GetPagedSearchResultWithAsync<T2>(Expression<Func<T, T2>> select, Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, int page = 0, int pageSize = 0, SortingDirection sorting_direction = SortingDirection.Descending, params string[] eagerLoadedProperties) where T2 : class;
        Task<SearchModel<T>> GetSearchResultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, SortingDirection sorting_direction = SortingDirection.Ascending);
        Task<SearchModel<T>> GetSearchResultWithAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, SortingDirection sorting_direction = SortingDirection.Descending, params string[] eagerLoadedProperties);
        Task<T?> GetWithAsync(object id, Dictionary<string, NavigationPropertyType> explicitLoadedProperties);
        Task<T?> GetWithAsync(object[] id, Dictionary<string, NavigationPropertyType> explicitLoadedProperties);
        Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task InsertAsync(T entity, CancellationToken cancellationToken);
        bool SaveChanges();
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
        void Update(IEnumerable<T> entities);
        void Update(T entity);
        Task UpdateAsync(IEnumerable<T> entities, Func<T, int> getKey);
        Task UpdateAsync(IEnumerable<T> entities, Func<T, object> getKey);
        Task UpdateAsync(T entity, Func<T, int> getKey);
        Task UpdateAsync(T entity, Func<T, object[]> getKey);
        Task UpdateAsync(T entity, Func<T, object> getKey);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null);
        Task<List<PropertyInfo>> GetProperties();
        Task<int> IsPassportUnique(string passportNumber);

    }
}
