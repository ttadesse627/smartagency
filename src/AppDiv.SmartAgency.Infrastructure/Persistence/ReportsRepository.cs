
using System.Data;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Contracts;
using AppDiv.SmartAgency.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly SmartAgencyDbContext _context;
        public ReportsRepository(SmartAgencyDbContext context)
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
                        if (value == null || string.IsNullOrEmpty(value.ToString())) sqlOperator = "IS NULL";
                        else sqlOperator = $"= '{value}'";
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
                        if (value == null || string.IsNullOrEmpty(value.ToString())) sqlOperator = "IS NOT NULL";
                        else sqlOperator = $"<>{value}";
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

        public async Task<IEnumerable<JObject>> GetObjectProperties(string viewName)
        {
            var properties = new List<JObject>();

            var sql = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{viewName}'";

            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var jObject = new JObject();
                string columnName = reader.GetString(0);
                jObject["PropertyName"] = columnName;
                properties.Add(jObject);
            }
            return properties;
        }

        public async Task<JObject> GetReportData(string reportName, List<string>? columns = null, List<Filter>? filters = null, List<Aggregate>? aggregates = null)
        {
            var reportData = new JObject();
            string filterSql = "";
            string selectColumn = "*";
            string selectColumnList = "";

            if (columns != null && columns.Count > 0)
            {
                foreach (var column in columns)
                {
                    selectColumnList += $"{column}, ";
                }

                selectColumn = selectColumnList.Remove(selectColumnList.Length - 2);
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
                    switch (aggregateMethod)
                    {
                        case SqlAggregate.GroupBy:
                            groupBySql += $"GROUP BY Id, {propertyName}, ";
                            break;
                        case SqlAggregate.OrderBy:
                            groupBySql += $"ORDER BY {propertyName} ASC, ";
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
            var sql = $"SELECT {selectColumn} {aggregateSql} FROM `{reportName}` {filterSql} {groupBySql}";

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
            connection.Close();

            var reportItems = new JArray();
            var properties = new JArray();
            var propertyNames = await GetObjectProperties(reportName);

            foreach (var dictionary in resultList)
            {
                var jsonDictionary = new Dictionary<string, JToken>();
                foreach (var kvp in dictionary)
                {
                    jsonDictionary[kvp.Key] = JToken.FromObject(kvp.Value);
                }
                reportItems.Add(JObject.FromObject(jsonDictionary));
            }

            if (propertyNames.Count() > 0 || propertyNames != null)
            {
                foreach (var prop in propertyNames)
                {
                    properties.Add(prop);
                }
            }

            Console.WriteLine(resultList);
            reportData["reportData"] = reportItems;
            reportData["reportProperties"] = properties;
            return reportData;
        }

        public async Task<ServiceResponse<Int32>> CreateReport(string reportName, string tableName, List<string>? columns = null, List<Filter>? filters = null, List<Aggregate>? aggregates = null)
        {
            var response = new ServiceResponse<int>();
            var propertyNames = new List<String>();
            string filterSql = "";
            string selectColumn = "*";
            string selectColumnList = "";

            if (string.IsNullOrEmpty(reportName) || reportName == string.Empty)
            {
                response.Message = "The report name should not be empty";
                response.Errors?.Add("Empty Report name");
                return response;
            }
            else if (string.IsNullOrEmpty(tableName) || tableName == string.Empty)
            {
                response.Message = "You should provide the type of report to be generated from.";
                response.Errors?.Add("Empty Report type");
                return response;
            }

            if (columns.Count > 0 || columns != null)
            {
                foreach (var column in columns)
                {
                    selectColumnList += $"{column}, ";
                }

                selectColumn = selectColumnList.Remove(selectColumnList.Length - 2);
            }
            if (filters != null && filters.Count > 0)
            {
                filters.Sort();
                var lastFilter = filters.Last();
                foreach (var filter in filters)
                {
                    var conditionStr = "";
                    conditionStr = $"{filter.PropertyName} {GetOperator(filter.Operator, filter.Value)};";
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
                            groupBySql += $"Order By {propertyName} ASC, ";
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
            var sql = $" CREATE VIEW `{reportName}` AS SELECT {selectColumn} {aggregateSql} FROM `{tableName}` {filterSql} {groupBySql}";
            try
            {
                response.Data = await _context.Database.ExecuteSqlRawAsync(sql);
                if (response.Data > 0)
                {
                    response.Success = true;
                    response.Message = $"Created a report {reportName} successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Errors?.Add(ex.Message);
                throw new ApplicationException(ex.Message);
            }

            return response;
        }
        public async Task<ServiceResponse<Int32>> CreateReportAsync(string reportName, string query)
        {
            var response = new ServiceResponse<int>();
            string sql = "";

            if (string.IsNullOrEmpty(reportName))
            {
                response.Message = "The report name should not be empty";
                response.Errors?.Add("Empty Report name");
                return response;
            }
            else if (string.IsNullOrEmpty(query))
            {
                response.Message = "You should provide the report query statement.";
                response.Errors?.Add("Empty Report type");
                return response;
            }
            else
            {
                sql = $" CREATE VIEW `{reportName}` AS {query}";
                try
                {
                    response.Data = await _context.Database.ExecuteSqlRawAsync(sql);
                    response.Message = $"Created a report {reportName} successfully.";
                }
                catch (Exception ex)
                {
                    response.Errors?.Add(ex.Message);
                    throw new BadRequestException(ex.Message);
                }
            }

            return response;
        }

        public async Task<JObject> GetReports()
        {

            var reportObjects = new JObject();

            var sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_SCHEMA = 'smartagency'";

            var connectionString = _context.Database.GetDbConnection().ConnectionString + ";Pooling=false;Allow User Variables=true";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            using var viewReader = await command.ExecuteReaderAsync();
            var objectNames = new JArray();
            while (await viewReader.ReadAsync())
            {
                var reportObject = new JObject();
                string reportName = viewReader.GetString(0);

                reportObject["ReportName"] = reportName;
                objectNames.Add(reportObject);
            }
            viewReader.Close();

            connection.Close();
            reportObjects["items"] = objectNames;

            return reportObjects;
        }

        public async Task<JObject> GetTestData()
        {

            var reportObjects = new JObject();

            var sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_SCHEMA = 'smartagency'";

            var connectionString = _context.Database.GetDbConnection().ConnectionString + ";Pooling=false;Allow User Variables=true";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            using var viewReader = await command.ExecuteReaderAsync();
            var objectNames = new JArray();
            while (await viewReader.ReadAsync())
            {
                var reportObject = new JObject();
                string viewName = viewReader.GetString(0);

                reportObject["objectName"] = viewName;
                objectNames.Add(reportObject);
            }
            viewReader.Close();

            foreach (var objectName in objectNames)
            {
                // Execute the first SQL statement to retrieve the view columns
                var columnsSql = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{objectName["objectName"]}'";
                using var colCommand = connection.CreateCommand();
                colCommand.CommandText = columnsSql;
                colCommand.CommandType = CommandType.Text;

                using var colReader = await colCommand.ExecuteReaderAsync();

                var columns = new JArray();
                while (await colReader.ReadAsync())
                {
                    var column = new JObject();
                    string columnName = colReader.GetString(0);
                    column["propertyName"] = columnName;
                    columns.Add(column);
                }
                objectName["objectProperties"] = columns;
                colReader.Close();
            }

            reportObjects["reportObjects"] = objectNames;

            return reportObjects;
        }
    }
}
