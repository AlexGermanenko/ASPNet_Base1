
namespace AspNetBase.Models
{
    public class EditProductModel
    {
        public int Id { get; set; }
        public uint Price { get; set; }
        public string Name { get; set; }
        public string ImgURL { get; set; }
        public string Description { get; set; }
        public int RateId { get; set; }
        public float Rate { get; set; }
    }
}
