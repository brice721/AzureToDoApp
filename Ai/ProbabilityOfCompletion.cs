using Azure;
using Azure.AI.TextAnalytics;
using Domain;
using Domain.Interfaces;

namespace Ai
{
    /// <summary>
    /// Uses Azure's Natural Language Understanding (NLU) to determine
    /// the sentiment of the to do item title providing a guess as to the
    /// likelihood of the to do being completed.
    /// </summary>
    public class ProbabilityOfCompletion
    {
        private readonly AppSettings _appSettings;

        public ProbabilityOfCompletion()
        {
            _appSettings = new AppSettings();
        }

        /// <summary>
        /// Analyzes the sentiment of the title of a given task. 
        /// Determines the probability of the task being completed.
        /// </summary>
        /// <remarks>
        /// Positive, task will probably get done.
        /// Negative, taks probably won't get done.
        /// Nuetral, task might get done.
        /// </remarks>
        /// <param name="sentence"></param>
        /// <returns>Positive|Negative|Nuetral</returns>
        public async Task<string> GetSentiment(string sentence)
        {
            var client = new TextAnalyticsClient(_appSettings.LanguageEndpoint, new AzureKeyCredential(_appSettings.LanguageKey));

            DocumentSentiment documentSentiment = await client.AnalyzeSentimentAsync(sentence);

            return documentSentiment.Sentiment.ToString();
        }
    }
}