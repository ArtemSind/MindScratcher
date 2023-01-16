namespace MindScratcher.Models.Options;

public class MongoOptions
{
    public string ConnectionString { get; set; }

    public string MindScratcherDbName { get; set; }
    public string CardsCollectionName { get; set; }
    public string FoldersCollectionName { get; set; }
}