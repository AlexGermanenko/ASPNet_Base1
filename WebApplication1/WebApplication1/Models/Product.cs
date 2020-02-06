namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public uint Price { get; set; }

        public string Name { get; set; }
        public string ImgURL { get; set; }
        public string Description { get; set; }

        public RateModel Rate { get; set; }

        public Product(string name, uint price, string description, string imgUrl)
        {
            Price = price;
            Name = name;
            ImgURL = imgUrl;
            Description = description;
        }

        public Product(int id, string name, uint price, string description, string imgUrl)
        {
            Id = id;
            Price = price;
            Name = name;
            ImgURL = imgUrl;
            Description = description;
        }

        public Product()
        {
            
        }
    }
}