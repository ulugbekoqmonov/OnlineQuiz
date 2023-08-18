using OnlineQuiz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Application.Service_Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();

        Task<Account> GetByIdAccountAsync(int id);

        Task AddAccountAsync(Account obj);
        Task AddRangeAccountsAsync(List<Account> obj);

        Task<bool> UpdateAccountAsync(Account entity);
        Task<bool> DeleteAccountAsync(int id);
    }
}
