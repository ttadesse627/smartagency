
using System.Data;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class GetReportsRepository : IGetReportsRepository
    {
        private readonly SmartAgencyDbContext _context;
        public GetReportsRepository(SmartAgencyDbContext context)
        {
            _context = context;

        }

        public string GetOperator(SqlOperator? sqlOper, object value)
        {
            string sqlOperator = "";

            if (sqlOper != null)
            {
                switch (sqlOper)
                {
                    case SqlOperator.Contain:
                        sqlOperator = $"LIKE '%{value}%'";
                        break;

                    case SqlOperator.EqualTo:
                        sqlOperator = $"={value}";
                        break;
                    case SqlOperator.GreaterThan:
                        sqlOperator = $">{value}";
                        break;
                    case SqlOperator.LessThan:
                        sqlOperator = $"<{value}";
                        break;
                    case SqlOperator.Between:
                        sqlOperator = "BETWEEN";
                        break;
                    case SqlOperator.NotEqual:
                        sqlOperator = $"<>{value}";
                        break;
                    case SqlOperator.GreaterThanOrEqual:
                        sqlOperator = $">={value}";
                        break;
                    case SqlOperator.LessThanOrEqual:
                        sqlOperator = $"<={value}";
                        break;
                    case SqlOperator.NotContain:
                        sqlOperator = $"NOT LIKE '%{value}%'";
                        break;
                    case SqlOperator.NotBetween:
                        sqlOperator = "NOT BETWEEN";
                        break;
                    default:
                        break;
                }
            }

            return sqlOperator;
        }

        public List<string> GetObjectTypes()
        {
            var entityTypes = new List<String>();
            var objectNames = new string[] { "Applicants", "Orders", "Users" };
            entityTypes.AddRange(objectNames);
            return entityTypes;
        }

        public List<string> GetObjectProperties(IEntityType entityType)
        {
            var properties = new List<string>();
            if (entityType != null)
            {
                var props = entityType.GetProperties();
                foreach (var prop in props)
                {
                    properties.Add(prop.Name);
                }
            }
            return properties;
        }

        public async Task<(List<object>, List<String>)> GetApplicantReport(string filterSql)
        {
            var entities = new List<object>();
            var properties = new List<string>();
            var entityType = _context.Model.FindEntityType(typeof(Applicant));

            if (entityType != null)
            {
                properties = GetObjectProperties(entityType);
            }

            var processes = await _context.Set<Applicant>().FromSqlRaw(filterSql).ToListAsync();
            entities = processes.Cast<object>().ToList();

            return (entities, properties);
        }

        public async Task<(IEnumerable<Dictionary<string, object>>, List<String>)> GetReportData(string reportName, List<string>? columns = null, List<Filter>? filters = null, List<Aggregate>? aggregates = null)
        {
            var propertyNames = new List<String>();
            string filterSql = "";
            string selectColumn = "*";
            string selectColumnList = "";

            if (columns.Count > 0 || columns != null)
            {
                foreach (var column in columns)
                {
                    selectColumnList += $"{column}, ";
                }

                selectColumn = selectColumnList;
            }
            if (filters != null && filters.Count > 0)
            {
                filters.Sort();
                var lastFilter = filters.Last();
                foreach (var filter in filters)
                {
                    var conditionStr = "";
                    conditionStr = $"{filter.PropertyName} {GetOperator(filter.Operator, filter.Value)}";
                    if (filter != lastFilter)
                    {
                        conditionStr += " AND ";
                    }
                    filterSql += conditionStr;
                }
                filterSql = " WHERE " + filterSql;
            }

            string groupBySql = "";
            string aggregateSql = "";
            if (aggregates != null && aggregates.Count > 0)
            {
                aggregates.Sort();
                var lastAggregate = aggregates.Last();

                foreach (var aggregate in aggregates)
                {
                    var propertyName = aggregate.PropertyName;

                    var aggregateMethod = aggregate.AggregateMethod ?? SqlAggregate.Count;

                    propertyNames.Add(propertyName);

                    switch (aggregateMethod)
                    {
                        case SqlAggregate.GroupBy:
                            groupBySql += $"GROUP BY Id, {propertyName}, ";
                            break;
                        case SqlAggregate.OrderBy:
                            groupBySql += $"{propertyName} ASC, ";
                            break;
                        case SqlAggregate.Count:
                            aggregateSql += $", COUNT({propertyName}) AS Count_{propertyName}";
                            break;
                        case SqlAggregate.Max:
                            aggregateSql += $", MAX({propertyName}) AS Max_{propertyName}";
                            break;
                        case SqlAggregate.Min:
                            aggregateSql += $", MIN({propertyName}) AS Min_{propertyName}";
                            break;
                        case SqlAggregate.Average:
                            aggregateSql += $", AVG({propertyName}) AS Average_{propertyName}";
                            break;
                    }
                }

                // Remove the trailing comma and space from the group by clause
                if (!string.IsNullOrEmpty(groupBySql))
                {
                    groupBySql = groupBySql.Remove(groupBySql.Length - 2);
                }
            }
            var sql = $"SELECT {selectColumn} {aggregateSql} FROM {reportName} {filterSql} {groupBySql}";

            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            using var reader = await command.ExecuteReaderAsync();

            List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                resultList.Add(row);
            }
            var response = (resultList, propertyNames);

            return response;
        }

        // public async Task 
        public async Task<IEnumerable<Dictionary<string, object>>> GetTestData()
        {
            string viewName = "UserInfo";
            string sql = $"SELECT UserName FROM {viewName};";

            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            using var reader = await command.ExecuteReaderAsync();

            List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                resultList.Add(row);
            }

            return resultList;
        }
        public async Task<List<DbTable>> GetDatabaseTablesAsync()
        {
            var dbTables = new List<DbTable>();

            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            var schema = connection.GetSchema("Tables");
            foreach (DataRow row in schema.Rows)
            {
                var tableType = (string)row["TABLE_TYPE"];
                if (tableType == "BASE TABLE" || tableType == "VIEW")
                {
                    var tableName = (string)row["TABLE_NAME"];
                    var columns = await GetTableColumnsAsync(tableName, tableType);
                    dbTables.Add(new DbTable { Name = tableName, Columns = columns });
                }
            }

            return dbTables;
        }

        private async Task<List<DbColumn>> GetTableColumnsAsync(string tableName, string tableType)
        {
            var columns = new List<DbColumn>();

            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            var schema = connection.GetSchema("Columns", new[] { null, null, tableName });
            foreach (DataRow row in schema.Rows)
            {
                var columnName = (string)row["COLUMN_NAME"];
                var dataTypeName = (string)row["DATA_TYPE"];
                var dataType = Type.GetType(dataTypeName);

                columns.Add(new DbColumn { Name = columnName, Type = dataType });
            }
            return columns;
        }

        public Task<(IEnumerable<Dictionary<string, object>>, List<string>)> GetReportData(string reportName, List<Filter>? filters = null, List<Aggregate>? aggregates = null)
        {
            throw new NotImplementedException();
        }
    }
}