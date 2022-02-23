using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        
        public bool Active { get; set; }

    }
}