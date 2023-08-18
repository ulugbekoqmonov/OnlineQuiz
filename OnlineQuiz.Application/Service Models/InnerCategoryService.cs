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
    public class InnerCategoryService : IInnerCategoryService
    {
        private readonly IInnerCategoryRepository innerCategoryRepository;
        public InnerCategoryService(IInnerCategoryRepository innerCategoryRepository)
        {
            this.innerCategoryRepository = innerCategoryRepository;
        }

        public async Task AddInnerCategoryAsync(InnerCategory innerCategory)
        {
            await this.innerCategoryRepository.AddAsync(innerCategory);
        }

        public async Task AddRangeInnerCategoriesAsync(List<InnerCategory> innerCategories)
        {
            await this.innerCategoryRepository.AddRangeAsync(innerCategories);
        }

        public async Task<bool> DeleteInnerCategoryAsync(int id)
        {
            return await this.innerCategoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<InnerCategory>> GetAllInnerCategoriesAsync()
        {
            return await innerCategoryRepository.GetAllAsync();
        }

        public async Task<InnerCategory> GetByIdInnerCategoryAsync(int id)
        {
            return await this.innerCategoryRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateInnerCategoryAsync(InnerCategory entity)
        {
            return await this.innerCategoryRepository.UpdateAsync(entity);
        }
    }
}
