﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Application.Core
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(int id);

        public Task AddAsync(T obj);
        public Task AddRangeAsync(List<T> obj);

        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(int id);
    }
}
