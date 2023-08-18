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
    public class QuestionService:IQuestionService
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionService(IQuestionRepository questionRepository) =>
            this.questionRepository = questionRepository;        

        public async Task AddQuestionAsync(Question question)
        {
            await this.questionRepository.AddAsync(question);
        }

        public async Task AddRangeQuestionsAsync(List<Question> questions)
        {
            await this.questionRepository.AddRangeAsync(questions);
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
           return await this.questionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await this.questionRepository.GetAllAsync();
        }

        public async Task<Question> GetByIdQuestionAsync(int id)
        {
            return await this.questionRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateQuestionAsync(Question entity)
        {
            return await this.questionRepository.UpdateAsync(entity);
        }
    }
}
