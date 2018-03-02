using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Tinyblog.Tests.Helpers
{
    /// <summary>
    /// Helper to work with scripts.
    /// </summary>
    public class ScriptHelper
    {
        /// <summary>
        /// Gets the script from resource.
        /// </summary>
        /// <param name="scriptName">Name of the script.</param>
        /// <returns>Script text.</returns>
        public static string GetScriptFromResource(string scriptName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var createDbScriptResourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.Contains(scriptName));
            if (createDbScriptResourceName == null)
            {
                return null;
            }

            Stream stream = assembly.GetManifestResourceStream(createDbScriptResourceName);
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                return streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// Executes the script to postgre.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="script">The script.</param>
        public static void ExecuteScriptToPostgre(string connectionString, string script)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand(script, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
