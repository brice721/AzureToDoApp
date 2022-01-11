namespace Domain
{
    public class ToDo
    {
        /// <summary>
        /// Guid assigned when document was created, in string format.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Task to be done.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Sentiment value from Azure TextAnalyzer.
        /// </summary>
        public string CompletionSentiment { get; set; }
        /// <summary>
        /// Date the task was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Flag for if the task is already completed.
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
