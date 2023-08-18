using Dapper;
using Npgsql;
using OnlineQuiz.Application.Repositories;
using OnlineQuiz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Infrastucture.Persistence
{
    public class DbQustion : DbContext, IQuestionRepository
    {
        public async Task AddAsync(Question question)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string commandText = @"INSERT INTO questions(
                question, option1, option2, option3, option4, true_option, dificulty, inner_category_id)
                VALUES (@QuestionText, @Option1, @Option2, @Option3, @Option4, @TrueOption, @Difficulty::dificulty, @InnerCategoryId)";

                await connection.ExecuteAsync(commandText, new
                {
                    question.QuestionText,
                    question.Option1,
                    question.Option2,
                    question.Option3,
                    question.Option4,
                    question.TrueOption,
                    Difficulty = question.Difficulty.ToString().ToLower(), // convert the enum value to a string
                   question.InnerCategory.InnerCategoryId
                });
            }
        }

        public async Task AddRangeAsync(List<Question> questions)
        {
            DbQustion dbQuestion = new();
            foreach (var item in questions)
            {
                await dbQuestion.AddAsync(item);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int result = 0;
            using (NpgsqlConnection connection = new(_connectionString))
            {
                var sqlQuery = $"DELETE FROM questions WHERE question_id ={id}";
                result = await connection.ExecuteAsync(sqlQuery);
            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Question>("SELECT * FROM questions");
            }
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                var question = await connection.QueryAsync<Question>($"SELECT * FROM questions WHERE question_id = {id}");
                return question.FirstOrDefault();
            }
        }

        public async Task<bool> UpdateAsync(Question entity)
        {
            using (NpgsqlConnection db = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = $@"UPDATE questions SET 
                question='{entity.QuestionText}', 
                option1='{entity.Option1}', 
                option2='{entity.Option2}', 
                option3='{entity.Option3}', 
                option4='{entity.Option4}', 
                true_option={entity.TrueOption}, 
                dificulty='{entity.Difficulty}' 
                WHERE question_id = {entity.QuestionId}";
                if (db.ExecuteAsync(sqlQuery).Result > 0) return true;
                return false;
            }
        }
    }
}