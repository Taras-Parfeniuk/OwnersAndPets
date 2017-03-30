using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace DAL
{
    internal static class DbContextFactory
    {
        public static OwnersAndPetsContext CreateContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OwnersAndPetsContext"].ConnectionString.Replace("{AppDir}",
                AppDomain.CurrentDomain.BaseDirectory);
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            var location = connectionString.Substring(12,
              connectionString.IndexOf(";", StringComparison.Ordinal) - 12);
            if (!File.Exists(location))
            {
                SQLiteConnection.CreateFile(location);
                connection.Open();
                var sql = "CREATE TABLE \"Owners\" ( `Id` TEXT, `Name` TEXT, PRIMARY KEY(`Id`) ); " +
                           "CREATE TABLE \"Pets\" ( `Id` TEXT, `Name` TEXT, `OwnerId` TEXT, PRIMARY KEY(`Id`) )";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
            }
            OwnersAndPetsContext context = new OwnersAndPetsContext(connection);
            return context;
        }
    }
}
