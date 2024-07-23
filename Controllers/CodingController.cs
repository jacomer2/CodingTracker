using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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

        public static void insertDB(CodingSession session)
        {
            try
            {
                using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["Pubs"].ConnectionString))
                {

                    var sql = "INSERT INTO CodingSessions (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";

                    var rowsAffected = connection.Execute(sql, session);
                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void deleteDB(int Id)
        {

            CodingSession? oldSession = CodingController.getSession(Id);

            if (oldSession == null)
            {
                return;
            }

            try
            {
                using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["Pubs"].ConnectionString))
                {

                    var sql = "DELETE FROM CodingSessions WHERE Id = @Id";

                    var parameters = new { Id };

                    var rowsAffected = connection.Execute(sql, parameters);
                    Console.WriteLine($"Session with ID {Id} successfully deleted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }

        public static void updateDB(CodingSession session)
        {
            try
            {
                using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["Pubs"].ConnectionString))
                {

                    var sql = "UPDATE CodingSessions SET StartTime=@StartTime, EndTime=@EndTime, Duration=@Duration WHERE Id=@Id";


                    var rowsAffected = connection.Execute(sql, session);
                    AnsiConsole.Markup($"Session with ID [green]{session.Id}[/] successfully updated.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static CodingSession? getSession(int Id)
        {
            try
            {
                using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["Pubs"].ConnectionString))
                {

                    var sql = "SELECT * FROM CodingSessions WHERE Id=@Id";

                    var parameters = new { Id };

                    var session = connection.QuerySingle<CodingSession>(sql, parameters);

                    return session;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]{ex.Message} for session with ID {Id}[/]");
                Console.WriteLine();
            }

            return null;
        }

        public static IEnumerable<CodingSession>? getAllSessions()
        {
            try
            {
                using var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["Pubs"].ConnectionString);

                var sql = "SELECT * FROM CodingSessions";
                var sessions = connection.Query<CodingSession>(sql);


                return sessions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
