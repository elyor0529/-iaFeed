
namespace KiaFeed.Parser.Helpers
{

    using KiaFeed.Parser.Models;
    using KiaFeed.Parser.Properties;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;

    public static class FeedParser
    {
        public static readonly Dictionary<FeedType, string> FeedUrls = new Dictionary<FeedType, string>
        {
            {FeedType.New,Settings.Default.NEW_VEHICLE_URL },
            {FeedType.Used,Settings.Default.USED_VEHICLE_URL }
        };

        public static readonly FeedType[] Keys = new[]
        {
            FeedType.New,
            FeedType.Used
        };

        public static IList<KiaFeedModel> GetModels()
        {
            var feeds = new List<KiaFeedModel>();

            foreach (var type in new[] { FeedType.New, FeedType.Used })
            {
                var url = FeedUrls[type];
                var xml = new XmlSerializer(typeof(Rss));
                var reader = XmlReader.Create(url);
                var rss = xml.CanDeserialize(reader) ? (Rss)xml.Deserialize(reader) : new Rss();

                foreach (var item in rss.Channel.Items)
                {
                    var feed = new KiaFeedModel
                    {
                        Id = item.Id,
                        KiaModel = item.Model.ExtractFeedString(),
                        Year = item.Year,
                        CarCategory = item.Body.ExtractFeedString().SplittingWhiteSpaceLastElement(),
                        EquipmentType = item.Trim.ExtractFeedString().SplittingWhiteSpaceElementAt(0).Replace("+", "").Replace("!", ""),
                        DrivetrainType = item.Trim.ExtractFeedString().SplittingWhiteSpaceElementAt(1),
                        BodyType = item.Body.ExtractFeedString(),
                        Engine = item.Engine.ExtractFeedString(),
                        FuelType = "",
                        MPG = "",
                        Transmission = item.Transmission.ExtractFeedString(),
                        ExteriorColor = item.Color.ExtractFeedString(),
                        InteriorColor = item.InteriorColor.ExtractFeedString(),
                        SalePrice = item.SalePrice.Replace(".00 USD", ""),
                        Price = item.Price.Replace(".00 USD", ""),
                        ModelUrl = item.Link.ExtractFeedString(),
                        ImageUrl = item.ImageLink,
                        Condition = item.Condition
                    };

                    feeds.Add(feed);
                }
            }

            return feeds;
        }

    }
}