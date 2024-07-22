using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Controllers
{
    class CodingController
    {

        public static void createDB()
        {
            var sql = @"CREATE TABLE CodingSessions(
                  ID INTEGER PRIMARY KEY,
                  StartTime TEXT,
                  EndTime TEXT,
                  Duration INT
                  )";

            try
            {
                using var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["Pubs"].ConnectionString);
                connection.Open();

                using var command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();

                Console.WriteLine("Table 'CodingSessions' created successfully.");

            }
            catch (SqliteException ex)
            {
               if (!ex.ErrorCode.Equals(-2147467259))
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
