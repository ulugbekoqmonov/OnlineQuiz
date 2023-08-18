using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Infrastucture.Persistence
{
    public class DbContext
    {
        public static string _connectionString = File.ReadAllText(@"..\..\..\..\..\OnlineQuiz\OnlineQuiz.Infrastucture\Appconfig.txt");

        public static void CreateTables()
        {
            try
            {
                using  NpgsqlConnection connection = new(_connectionString);
                connection.Open();
                string query = File.ReadAllText(@"..\..\..\..\..\OnlineQuiz\OnlineQuiz.Infrastucture\CreateTables.txt");


                NpgsqlCommand command = new(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        public static void CreateDb()
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                connection.Close();
            }
            catch (Npgsql.PostgresException exp)
            {
                if (exp.Message.Contains("does not exist", StringComparison.OrdinalIgnoreCase))
                {
                    string con = _connectionString.Replace("online_quiz", "postgres");
                    using NpgsqlConnection connection = new(con);
                    connection.Open();
                    string query = "create database online_quiz";
                    NpgsqlCommand command = new(query, connection);
                    command.ExecuteNonQuery();
                    CreateTables();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        //public static void AddQuestions()
        //{
        //    try
        //    {
        //        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        //        {
        //            connection.Open();
        //            string? querystring = @"";
        //            using NpgsqlCommand command = new(querystring, connection);
        //            command.ExecuteNonQuery();

        //        }

        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("saassddds");
        //    }

        //}
    }
}
