
namespace KiaFeed.Parser.Models
{

    using System.Collections.Generic;
    using System.Xml.Serialization;

    public enum FeedType
    {
        New,
        Used
    }

    public class KiaFeedModel
    {
        public string Id { get; set; }

        public string KiaModel { get; set; }

        public string Year { get; set; }

        public string CarCategory { get; set; }

        public string EquipmentType { get; set; }

        public string DrivetrainType { get; set; }

        public string BodyType { get; set; }

        public string Engine { get; set; }

        public string FuelType { get; set; }

        public string MPG { get; set; }

        public string Transmission { get; set; }

        public string SalePrice { get; set; }

        public string ModelUrl { get; set; }

        public string Price { get; set; }

        public string InteriorColor { get; set; }

        public string ExteriorColor { get; set; }

        public string ImageUrl { get; set; }

        public string Condition { get; set; }

    }

    [XmlRoot("rss")]
    public class Rss
    {
        [XmlAttribute(AttributeName = "version")]
        public string Version = "2.0";

        [XmlElement("channel")]
        public Channel Channel { get; set; }

        public Rss()
        {
            Channel = new Channel();
        }

    }

    [XmlRoot("channel")]
    public class Channel
    {

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }

        [XmlElement("lastBuildDate")]
        public string LastBuildDate { get; set; }

        [XmlElement("copyright")]
        public string Copyright { get; set; }

        [XmlElement("item")]
        public List<Item> Items { get; set; }

        public Channel()
        {
            Items = new List<Item>();
        }
    }

    [XmlRoot("image")]
    public class Image
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("width")]
        public string Width { get; set; }

        [XmlElement("height")]
        public string Height { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

    }

    [XmlRoot("item")]
    public class Item
    {

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("image")]
        public Image Image { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement]
        public string VIN { get; set; }

        [XmlElement]
        public string Year { get; set; }

        [XmlElement("brand")]
        public string Brand { get; set; }

        [XmlElement]
        public string Model { get; set; }

        [XmlElement]
        public string Body { get; set; }

        [XmlElement]
        public string Trim { get; set; }

        [XmlElement]
        public string Engine { get; set; }

        [XmlElement]
        public string Transmission { get; set; }

        [XmlElement("color")]
        public string Color { get; set; }

        [XmlElement]
        public string InteriorColor { get; set; }

        [XmlElement("sale_price")]
        public string SalePrice { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement]
        public string Mileage { get; set; }

        [XmlElement("condition")]
        public string Condition { get; set; }

        [XmlElement]
        public string IsCertified { get; set; }

        [XmlElement("options")]
        public string Options { get; set; }

        [XmlElement("image_link")]
        public string ImageLink { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("availability")]
        public string Availability { get; set; }

        [XmlElement("google_product_category")]
        public string GoogleProductCategory { get; set; }

        public Item()
        {
            Image = new Image();
        }

    }

}