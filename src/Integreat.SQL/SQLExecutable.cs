using Integreat.Core;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Integreat.SQL
{
    public sealed class SQLExecutable : FileExecutable
    {
        private readonly string _connectionString;
        private readonly CommandType _commandType;

        public SQLExecutable(IFileStorage fileStorage, string file, string connectionString, CommandType commandType) 
            : base(fileStorage, file)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString),
                       $"ConnectionString value is required to execute {GetType().FullName} executable.");
            }

            _connectionString = connectionString.Trim();
            _commandType = commandType;
        }

        protected override void OnExecute(ExecutableContext context, IFile file)
        {
            context.Log($"Executing SQL script at '{file.FullPath}'...");

            string sql = FileStorage.Read(file.FullPath);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var command = new SqlCommand(sql, conn))
                {
                    command.CommandType = _commandType;
                    command.CommandTimeout = context.Timeout;
                    command.AddParameters(context.Parameters);

                    context.Log($"Parameter Arguments: {context.Parameters}");

                    var result = command.ExecuteNonQuery();

                    context.Log($"SQL command executed. Number of records affected: {result}.");
                }

                conn.Close();
            }
        }
    }
}
