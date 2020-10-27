using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RestAPI.DataAccess.QueryProvider
{
    public interface IDataQueryProvider
    {
        TResponse Query<TResponse>(Func<SqlConnection, TResponse> execute) where TResponse : new();
        void Query(Action<SqlConnection> execute);
        IEnumerable<TResponse> QueryCollection<TResponse>(Func<SqlConnection, IEnumerable<TResponse>> execute);
    }
}