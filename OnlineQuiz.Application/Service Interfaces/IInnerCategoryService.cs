using OnlineQuiz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Application.Service_Interfaces
{
    public interface IInnerCategoryService
    {
        Task<IEnumerable<InnerCategory>> GetAllInnerCategoriesAsync();

        Task<InnerCategory> GetByIdInnerCategoryAsync(int id);

        Task AddInnerCategoryAsync(InnerCategory obj);
        Task AddRangeInnerCategoriesAsync(List<InnerCategory> obj);

        Task<bool> UpdateInnerCategoryAsync(InnerCategory entity);
        Task<bool> DeleteInnerCategoryAsync(int id);
    }
}
