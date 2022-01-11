using Ai;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class DataMappingExtension
    {
        private static readonly ProbabilityOfCompletion sentiment = new ProbabilityOfCompletion();

        public static async Task<ToDo> AsToDoObjectAsync(this ToDoDto dto)
        {
            return new ToDo
            {
                Id = Guid.NewGuid().ToString(),
                Title = dto.Title,
                CompletionSentiment = await sentiment.GetSentiment(dto.Title),
                CreatedOn = DateTime.Now,
                IsComplete = dto.IsComplete
            };
        }

        public static async Task<ToDo> AsUpdatedToDoAsync(this ToDoDto dto, ToDo entity)
        {
            entity.Title = dto.Title;
            entity.CompletionSentiment = await sentiment.GetSentiment(dto.Title);
            entity.IsComplete = dto.IsComplete;

            return entity;
        }
    }
}
