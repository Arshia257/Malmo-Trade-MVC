namespace MalmöTradera.web.Models
{
    public class ItemsModel
    {
         public int id { get; set; }
        public string Name { get; set; }
        public int price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool ISSold { get; set; }
    }
}