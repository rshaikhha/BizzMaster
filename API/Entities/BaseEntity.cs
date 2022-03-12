using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

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