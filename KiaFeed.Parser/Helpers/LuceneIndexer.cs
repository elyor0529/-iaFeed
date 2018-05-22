
namespace KiaFeed.Parser.Helpers
{
    using KiaFeed.Parser.Models;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Search;
    using Lucene.Net.Store;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// https://github.com/mikhail-tsennykh/Lucene.Net-search-MVC-sample-site/blob/master/LuceneSearch.Library/GoLucene.cs
    /// </summary>
    public class LuceneIndexer
    {
        private static string _luceneDir = Path.Combine(HttpRuntime.AppDomainAppPath, "lucene_index");

        static LuceneIndexer()
        {
            if (!System.IO.Directory.Exists(_luceneDir))
                System.IO.Directory.CreateDirectory(_luceneDir);
        }

        private static FSDirectory _directory;
        private static FSDirectory Directory
        {
            get
            {
                if (_directory == null)
                    _directory = FSDirectory.Open(new DirectoryInfo(_luceneDir));

                if (IndexWriter.IsLocked(_directory))
                    IndexWriter.Unlock(_directory);

                var lockFilePath = Path.Combine(_luceneDir, "write.lock");

                if (File.Exists(lockFilePath))
                    File.Delete(lockFilePath);

                return _directory;
            }
        }

        private static void AddToLuceneIndex(KiaFeedModel item, IndexWriter writer)
        {
            // remove older index entry
            var searchQuery = new TermQuery(new Term("Id", item.Id));

            writer.DeleteDocuments(searchQuery);

            // add new index entry
            var doc = new Document();

            // add lucene fields mapped to db fields
            doc.Add(new Field("Id", item.Id, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("BodyType", item.BodyType, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("CarCategory", item.CarCategory, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("DrivetrainType", item.DrivetrainType, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Engine", item.Engine, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("EquipmentType", item.EquipmentType, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ExteriorColor", item.ExteriorColor, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("FuelType", item.FuelType, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ImageUrl", item.ImageUrl, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("InteriorColor", item.InteriorColor, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("KiaModel", item.KiaModel, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ModelUrl", item.ModelUrl, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("MPG", item.MPG, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Price", item.Price, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("SalePrice", item.SalePrice, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Transmission", item.Transmission, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Year", item.Year, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Condition", item.Condition, Field.Store.YES, Field.Index.ANALYZED));

            // add entry to index
            writer.AddDocument(doc);
        }

        public static IEnumerable<KiaFeedModel> GetAllIndexRecords()
        {
            if (!System.IO.Directory.EnumerateFiles(_luceneDir).Any())
                return new List<KiaFeedModel>();

            using (var searcher = new IndexSearcher(Directory, false))
            {
                using (var reader = IndexReader.Open(Directory, false))
                {
                    var docs = new List<Document>();
                    var term = reader.TermDocs();

                    while (term.Next())
                        docs.Add(searcher.Doc(term.Doc));

                    return docs.Select(doc => new KiaFeedModel
                    {
                        Id = doc.Get("Id"),
                        BodyType = doc.Get("BodyType"),
                        CarCategory = doc.Get("CarCategory"),
                        Condition = doc.Get("Condition"),
                        DrivetrainType = doc.Get("DrivetrainType"),
                        Engine = doc.Get("Engine"),
                        EquipmentType = doc.Get("EquipmentType"),
                        ExteriorColor = doc.Get("ExteriorColor"),
                        FuelType = doc.Get("FuelType"),
                        ImageUrl = doc.Get("ImageUrl"),
                        InteriorColor = doc.Get("InteriorColor"),
                        KiaModel = doc.Get("KiaModel"),
                        ModelUrl = doc.Get("ModelUrl"),
                        MPG = doc.Get("MPG"),
                        Price = doc.Get("Price"),
                        SalePrice = doc.Get("SalePrice"),
                        Transmission = doc.Get("Transmission"),
                        Year = doc.Get("Year")
                    });
                }
            }
        }

        public static void AddUpdateLuceneIndex(IEnumerable<KiaFeedModel> items)
        {
            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
            {
                using (var writer = new IndexWriter(Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    foreach (var item in items)
                        AddToLuceneIndex(item, writer);
                }

                analyzer.Close();
            }
        }

        public static bool ClearLuceneIndex()
        {
            try
            {
                using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
                {
                    using (var writer = new IndexWriter(Directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                    {
                        writer.DeleteAll();
                    }

                    analyzer.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static void Optimize()
        {
            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
            {
                using (var writer = new IndexWriter(Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    writer.Optimize();
                }

                analyzer.Close();
            }
        }

    }
}