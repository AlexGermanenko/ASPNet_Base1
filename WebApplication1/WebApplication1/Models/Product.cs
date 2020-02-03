namespace WebApplication1.Models
{
    public class Product
    {
        public uint Id { get; set; }

        public float Rate { get; set; }

        public uint Price { get; set; }

        public string Name { get; set; }
        public string ImgURL { get; set; }
        public string Description { get; set; }

        public Product(string name, uint price, float rate, string description, string imgUrl)
        {
            //Id = id;
            Price = price;
            Rate = rate;
            Name = name;
            Description = description;
            ImgURL = imgUrl;
        }
    }
}