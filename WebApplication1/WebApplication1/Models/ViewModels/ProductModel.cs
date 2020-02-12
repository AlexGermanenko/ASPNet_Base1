namespace WebApplication1.Models.ViewModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public uint Price { get; set; }

        public string Name { get; set; }
        public string ImgURL { get; set; }
        public string Description { get; set; }

        public RateModel Rate { get; set; }

        public ProductModel(string name, uint price, string description, string imgUrl)
        {
            Price = price;
            Name = name;
            ImgURL = imgUrl;
            Description = description;
        }

        public ProductModel(int id, string name, uint price, string description, string imgUrl)
        {
            Id = id;
            Price = price;
            Name = name;
            ImgURL = imgUrl;
            Description = description;
        }

        public ProductModel()
        {
            
        }
    }
}