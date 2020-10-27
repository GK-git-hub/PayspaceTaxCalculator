using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RestAPI.DataAccess.QueryProvider
{
    public class DataQueryProvider : IDataQueryProvider
    {
        private readonly IConfiguration _configuration;

        public DataQueryProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public TResponse Query<TResponse>(Func<SqlConnection, TResponse> execute) where TResponse : new()
        {
            TResponse response;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("TaxCalculation")))
            {
                connection.Open();
                response = execute.Invoke(connection);
            }

            return response;
        }

        public void Query(Action<SqlConnection> execute)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TaxCalculation")))
            {
                connection.Open();
                execute.Invoke(connection);
            }
        }

        public IEnumerable<TResponse> QueryCollection<TResponse>(Func<SqlConnection, IEnumerable<TResponse>> execute)
        {
            IEnumerable<TResponse> response;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("TaxCalculation")))
            {
                connection.Open();
                response = execute.Invoke(connection);
            }

            return response;
        }
            
    }
}