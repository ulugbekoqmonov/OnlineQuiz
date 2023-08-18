using Dapper;
using Npgsql;
using OnlineQuiz.Application.Repositories;
using OnlineQuiz.Domain.Enums;
using OnlineQuiz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Infrastucture.Persistence
{
    public class DbInnerCategory : DbContext, IInnerCategoryRepository
    {
        public async Task AddAsync(InnerCategory innerCategory)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string commandText = @"insert into inner_categories(inner_category_name, question_count, time, category_name)
                                        values (@InnerCategoryName, @QuestionCount, @Time, @CategoryName::category)";

                await connection.ExecuteAsync(commandText, new
                {
                    innerCategory.InnerCategoryName,                    
                    innerCategory.Time,
                    CategoryName = innerCategory.CategoryName.ToString().ToLower(),
                });
            }
        }

        public async Task AddRangeAsync(List<InnerCategory> innerCategories)
        {
            DbInnerCategory dbInnerCategory = new();
            foreach (var item in innerCategories)
            {
                await dbInnerCategory.AddAsync(item);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int result = 0;
            using (NpgsqlConnection connection = new(_connectionString))
            {
                var sqlQuery = $"delete from inner_categories where inner_category_id = {id}";
                result = await connection.ExecuteAsync(sqlQuery);
            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<InnerCategory>> GetAllAsync()
        {
            using (NpgsqlConnection connection = new (_connectionString))
            {
                return await connection.QueryAsync<InnerCategory>("select * from inner_categories");
            }
        }

        public async Task<InnerCategory> GetByIdAsync(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                var innerCategory = await connection.QueryAsync<InnerCategory>($"select * from inner_categories where inner_category_id = {id}");
                return innerCategory.FirstOrDefault();
            }
        }

        public async Task<bool> UpdateAsync(InnerCategory entity)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                var sqlQuery = $@"update inner_categories set 
                inner_category_id='{entity.InnerCategoryId}', 
                inner_category_name='{entity.InnerCategoryName}',                 
                time='{entity.Time}', 
                category_name='{entity.CategoryName}',                 
                WHERE inner_category_id = {entity.InnerCategoryId}";
                if (connection.ExecuteAsync(sqlQuery).Result > 0) return true;
                return false;
            }
        }
    }
}
