
namespace API.Entities
{
    public class Product : BaseEntity
    {
        public string PartNumber { get; set; }
        public string Description { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public Category Category { get; set; }
        public SetType SetType { get; set; }

        public double Volume { get; set; }
        public double Weight { get; set; }
        public int ItemPerSet { get; set; }
        public int Popularity { get; set; }

    }

    

    public class SetType : BaseEntity { }


}