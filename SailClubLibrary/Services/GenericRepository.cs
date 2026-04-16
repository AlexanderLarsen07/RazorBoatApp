using System.Reflection;
using Microsoft.Data.SqlClient;
using SailClubLibrary.Helpers.Attributes;
using SailClubLibrary.Interfaces;

namespace SailClubLibrary.Services
{
    public class GenericRepository<T> : Connection, IGenericRepositoryAsync<T>
    {
        #region Methods
        /// <summary>
        /// Inserts an object into the database by mapping its properties to SQL parameters.
        /// </summary>
        /// <param name="sql">
        /// The SQL query to execute. It is expected to contain parameters that match
        /// the property names of the object (e.g., @PropertyName).
        /// </param>
        /// <param name="object">
        /// The object to be inserted into the database. Its public properties are
        /// used as parameters for the SQL query.
        /// </param>
        /// <returns>
        /// An asynchronous task representing the insert operation.
        /// </returns>
        /// <exception cref="SqlException">
        /// Thrown if an error occurs while connecting to the database or executing the SQL command.
        /// </exception>
        /// <remarks>
        /// This method uses reflection to iterate through all properties of type <typeparamref name="T"/>
        /// and adds them as parameters to the SQL command. If a property value is <c>null</c>,
        /// it is replaced with <see cref="DBNull.Value"/>.
        /// </remarks>
        public async Task AddObjectAsync(string sql, T @object)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();
                var properties = typeof(T).GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(@object) ?? DBNull.Value;
                    command.Parameters.AddWithValue("@" + prop.Name, value);
                }
                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        /// Executes a SQL query that returns a single scalar value representing a count,
        /// and converts the result to an integer.
        /// </summary>
        /// <param name="sql">
        /// The SQL query to execute. It is expected to return a single value
        /// (e.g., using COUNT in SQL).
        /// </param>
        /// <returns>
        /// An asynchronous task that returns the count as an <see cref="int"/>.
        /// </returns>
        /// <exception cref="SqlException">
        /// Thrown if an error occurs while connecting to the database
        /// or executing the SQL command.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if the result cannot be converted to an integer.
        /// </exception>
        /// <remarks>
        /// This method opens a database connection asynchronously and uses
        /// <c>ExecuteScalarAsync</c> to retrieve a single value from the database.
        /// </remarks>
        public async Task<int> CountAsync(string sql)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();
                object? result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
        /// <summary>
        /// Executes a SQL query and maps each row in the result set to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="sql">
        /// The SQL query to execute.
        /// </param>
        /// <param name="func">
        /// A mapping function that takes a <see cref="SqlDataReader"/> and converts the current row
        /// into an instance of <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// An asynchronous task that returns a list of objects of type <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="SqlException">
        /// Thrown if an error occurs while connecting to the database or executing the SQL command.
        /// </exception>
        /// <remarks>
        /// This method opens a database connection asynchronously and uses <c>ExecuteReaderAsync</c>
        /// to retrieve multiple rows. Each row is passed to the provided mapping function to create
        /// a corresponding object.
        /// </remarks>
        public async Task<List<T>> GetAllObjectsAsync(string sql, Func<SqlDataReader, T> func)
        {
            var results = new List<T>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        results.Add(func(reader));
                    }
                }
            }
            return results;
        }
        /// <summary>
        /// Executes a SQL command to remove an object from the database using a specified identifier.
        /// </summary>
        /// <param name="sql">
        /// The SQL query to execute. It is expected to contain a parameter matching the provided identifier name
        /// (e.g., WHERE Id = @Id).
        /// </param>
        /// <param name="idName">
        /// The name of the parameter representing the identifier (without the '@' prefix).
        /// </param>
        /// <param name="value">
        /// The value of the identifier used to locate and remove the object. If <c>null</c>,
        /// it will be replaced with <see cref="DBNull.Value"/>.
        /// </param>
        /// <returns>
        /// An asynchronous task representing the delete operation.
        /// </returns>
        /// <exception cref="SqlException">
        /// Thrown if an error occurs while connecting to the database or executing the SQL command.
        /// </exception>
        /// <remarks>
        /// This method opens a database connection asynchronously and executes a non-query command
        /// (typically a DELETE statement). The identifier parameter is dynamically added based on the provided name.
        /// </remarks>
        public async Task RemoveObjectAsync(string sql, string idName, object value)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();

                command.Parameters.AddWithValue("@"+idName, value ?? DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        /// Executes a SQL query and returns the first matching object of type <typeparamref name="T"/>,
        /// or <c>null</c> if no result is found.
        /// </summary>
        /// <param name="sql">
        /// The SQL query to execute.
        /// </param>
        /// <param name="func">
        /// A mapping function that converts the current row of a <see cref="SqlDataReader"/>
        /// into an instance of <typeparamref name="T"/>.
        /// </param>
        /// <param name="parameterize">
        /// An optional action used to add parameters to the <see cref="SqlCommand"/> before execution.
        /// </param>
        /// <returns>
        /// A task that returns the first matching object of type <typeparamref name="T"/>,
        /// or <c>null</c> if no rows are returned.
        /// </returns>
        /// <exception cref="SqlException">
        /// Thrown if an error occurs while connecting to the database or executing the SQL command.
        /// </exception>
        /// <remarks>
        /// This method executes the query asynchronously and reads only the first row from the result set.
        /// If a row exists, it is mapped using the provided function; otherwise, the default value is returned.
        /// </remarks>
        public async Task<T?> SearchObjectAsync(string sql, Func<SqlDataReader, T> func, Action<SqlCommand>? parameterize = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(sql, connection);

            parameterize?.Invoke(command);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return func(reader);

            return default;
        }
        /// <summary>
        /// Updates an existing object in the database by mapping its properties to SQL parameters,
        /// excluding properties marked with <see cref="IgnoreUpdateAttribute"/>.
        /// </summary>
        /// <param name="sql">
        /// The SQL query to execute. It is expected to contain parameters matching the property names
        /// of the object as well as the identifier parameter.
        /// </param>
        /// <param name="updatedObject">
        /// The object containing the updated values. Its public properties are used as SQL parameters,
        /// except those marked with <see cref="IgnoreUpdateAttribute"/>.
        /// </param>
        /// <param name="idName">
        /// The name of the identifier parameter (without the '@' prefix) used to locate the record to update.
        /// </param>
        /// <param name="idValue">
        /// The value of the identifier used in the update operation. If <c>null</c>, it is replaced with
        /// <see cref="DBNull.Value"/>.
        /// </param>
        /// <returns>
        /// An asynchronous task representing the update operation.
        /// </returns>
        /// <exception cref="SqlException">
        /// Thrown if an error occurs while connecting to the database or executing the SQL command.
        /// </exception>
        /// <remarks>
        /// This method uses reflection to iterate through the properties of type <typeparamref name="T"/>.
        /// Properties decorated with <see cref="IgnoreUpdateAttribute"/> are excluded from the update.
        /// All other properties are added as SQL parameters. The identifier parameter is added separately.
        /// </remarks>
        public async Task UpdateObjectAsync(string sql, T updatedObject, string idName, object idValue)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();
                var properties = typeof(T).GetProperties().Where(p => p.GetCustomAttribute<IgnoreUpdateAttribute>() == null);
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(updatedObject) ?? DBNull.Value;
                    command.Parameters.AddWithValue("@" + prop.Name, value);
                }
                command.Parameters.AddWithValue("@" + idName, idValue);
                await command.ExecuteNonQueryAsync();
            }
        }
        #endregion
    }
}
