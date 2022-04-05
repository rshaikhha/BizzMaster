using System;


namespace API.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.Active = true;
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime CreatedOn {get; set;} = DateTime.Now;


        
        public bool Active { get; set; }

    }


    

}