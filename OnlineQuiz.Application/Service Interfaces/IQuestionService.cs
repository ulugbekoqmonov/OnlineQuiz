using OnlineQuiz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Application.Service_Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();

        Task<Question> GetByIdQuestionAsync(int id);

        Task AddQuestionAsync(Question obj);
        Task AddRangeQuestionsAsync(List<Question> obj);

        Task<bool> UpdateQuestionAsync(Question entity);
        Task<bool> DeleteQuestionAsync(int id);
    }
}
