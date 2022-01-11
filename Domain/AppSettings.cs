namespace Domain
{
    public class AppSettings
    {
        public string Database { get; set; }
        public string Container { get; set; }
        public Uri Url { get; set; }
        public string PrimaryKey { get; set; }
        public string ConnectionString { get; set; }
        public string LanguageKey { get; set; }
        public Uri LanguageEndpoint { get; set; }

        public AppSettings()
        {
            var accountEndpoint = "https://cosmosorm.documents.azure.com:443/";
            var accountKey = "<account_key_from_azure>";

            Database = "CosmosPlayground";
            Container = "ToDos";
            Url = new Uri(accountEndpoint);
            PrimaryKey = accountKey;
            ConnectionString =
                $"AccountEndpoint={accountEndpoint};AccountKey={accountKey};";
            LanguageKey = "<azure_cognitive_services_key>";
            LanguageEndpoint = new Uri("https://testtextanalyticsbsr.cognitiveservices.azure.com/");
        }
    }
}