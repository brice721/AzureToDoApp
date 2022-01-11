using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public static class DataMocks
    {
        public static IQueryable<ToDo> GetMockToDos() => new List<ToDo>
            {
                new ToDo{
                    Id = "79a34314-818a-4c40-aa20-f0cc1e5ee3ad",
                    Title = "Test ToDo One",
                    CompletionSentiment = "Positive",
                    CreatedOn = DateTime.Parse("1/08/2022 1:26:30 PM"),
                    IsComplete = false
                },
                new ToDo{
                    Id = "4c532f14-3b1b-46d9-94d0-eb5fb38278eb",
                    Title = "Test ToDo Two",
                    CompletionSentiment = "Positive",
                    CreatedOn = DateTime.Parse("1/09/2022 1:26:30 PM"),
                    IsComplete = false
                },
                new ToDo{
                    Id = "ac97a789-a56e-4926-b32d-6cb1fb802442",
                    Title = "Test ToDo Three",
                    CompletionSentiment = "Positive",
                    CreatedOn = DateTime.Parse("1/06/2022 1:26:30 PM"),
                    IsComplete = false
                }
            }.AsQueryable();

        public static ToDoDto ToDoDto() => new ToDoDto 
        {
            Id = "ac97a789-a56e-4926-b32d-6cb1fb802442",
            Title = "Test ToDo Three",
            CreatedOn = DateTime.Parse("1/06/2022 1:26:30 PM"),
            IsComplete = true
        };
    }
}
