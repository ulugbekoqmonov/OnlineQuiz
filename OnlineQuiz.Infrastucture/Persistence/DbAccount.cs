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
    public class DbAccount:DbContext,IAccountRepository
    {
        public async Task AddAsync(Account account)
        {
            using (NpgsqlConnection connection = new(_connectionString))
            {
                connection.Open();
                var sqlQuery = "INSERT INTO accounts(username, password, email, phone_number) VALUES(@Username, @Password, @Email, @PhoneNumber)";
                await connection.ExecuteAsync(sqlQuery, new { Username = account.Username, Password = account.Password, Email = account.Email, PhoneNumber = account.PhoneNumber });

            }
        }

        public async Task AddRangeAsync(List<Account> accounts)
        {
            foreach (Account item in accounts)
            {
                await AddAsync(item);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int result = 0;
            using (NpgsqlConnection conn = new(_connectionString))
            {
                var sqlQuery = "DELETE FROM accounts WHERE account_id = @id";
                result = await conn.ExecuteAsync(sqlQuery, new { id });
            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            using (NpgsqlConnection conn = new(_connectionString))
            {
                return await conn.QueryAsync<Account>("select * from accounts");
            }
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            using (NpgsqlConnection conn = new(_connectionString))
            {
                var account = await conn.QueryAsync<Account>($"SELECT * FROM accounts WHERE account_id = {id}");
                return account.FirstOrDefault();
            }
        }

        public async Task<bool> UpdateAsync(Account entity)
        {
            using (NpgsqlConnection conn = new(_connectionString))
            {
                var sqlQuery = "UPDATE accounts SET username = @Username, password = @Password, email = @Email, phone_number = @PhoneNumber WHERE account_id = @AccountId";
                int n = await conn.ExecuteAsync(sqlQuery, new { entity.Username, entity.Password, entity.Email, entity.PhoneNumber, AccountId = entity.AccountId });
                return n > 0;
            }
        }
    }
}
