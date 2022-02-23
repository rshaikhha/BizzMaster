
namespace API.Entities
{
    public class Product : BaseEntity
    {
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }

        public Category Category { get; set; }
        public MasterSystem MasterSystem { get; set; }
        public SubSystem SubSystem { get; set; }
        public ConsumptionType ConsumptionType { get; set; }
        public SetType SetType { get; set; }

        public double Volume { get; set; }
        public double Weight { get; set; }
        public int ItemPerSet { get; set; }
        public int Popularity { get; set; }

    }

    public class Category : BaseEntity 
    { 
        public int ParentId { get; set; }
        public Category Parent { get; set; }
    }

    public class MasterSystem : BaseEntity { }

    public class SubSystem : BaseEntity { }

    public class ConsumptionType : BaseEntity { }

    public class SetType : BaseEntity { }


}