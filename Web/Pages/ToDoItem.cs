using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class ToDoItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CompletionSentiment { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsComplete { get; set; }
    }
}
