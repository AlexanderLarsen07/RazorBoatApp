using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SailClubLibrary.Models;

namespace SailClubLibrary.Interfaces
{
    /// <summary>
    /// A generic interface class for interacting with an SQL DB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepositoryAsync<T>
    {
        #region Methods
        Task<int> CountAsync(string sql);
        Task AddObjectAsync(string sql, T @object);
        Task RemoveObjectAsync(string sql, string idName, object value);
        Task UpdateObjectAsync(string sql, T updatedObject, string idName, object idValue);
        Task<List<T>> GetAllObjectsAsync(string sql, Func<SqlDataReader, T> func);
        Task<T?> SearchObjectAsync(string sql, Func<SqlDataReader, T> func, Action<SqlCommand>? parameter);
        #endregion
    }
}
