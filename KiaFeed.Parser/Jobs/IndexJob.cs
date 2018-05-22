
namespace KiaFeed.Parser.Jobs
{
    using KiaFeed.Parser.Helpers;
    using Quartz;
    using System;

    public class IndexJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            LuceneIndexer.ClearLuceneIndex();
            Console.WriteLine("Search index was cleared successfully!");

            var items = FeedParser.GetModels();
            LuceneIndexer.AddUpdateLuceneIndex(items);
            Console.WriteLine("Search index was created successfully!");

            LuceneIndexer.Optimize();
            Console.WriteLine("Search index was optimized successfully!");
        }
    }
}