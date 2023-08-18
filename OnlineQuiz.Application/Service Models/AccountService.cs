using OnlineQuiz.Application.Repositories;
using OnlineQuiz.Application.Service_Interfaces;
using OnlineQuiz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Application.Service_Models
{
    internal class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository) =>
            this.accountRepository = accountRepository;
        public async Task AddAccountAsync(Account account)
        {
            await this.accountRepository.AddAsync(account);
        }

        public async Task AddRangeAccountsAsync(List<Account> accounts)
        {
            await this.accountRepository.AddRangeAsync(accounts);
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            return await this.accountRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await this.accountRepository.GetAllAsync();
        }

        public async Task<Account> GetByIdAccountAsync(int id)
        {
            return await accountRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAccountAsync(Account entity)
        {
            return await this.accountRepository.UpdateAsync(entity);
        }
    }
}
