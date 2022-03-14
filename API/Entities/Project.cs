using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace API.Entities
{

    public class Project : BaseEntity
    {
        public int? OrderId {get; set;}
        public virtual Order Order {get; set;}
        public IList<Activity> Activities {get; set;}
    }

    public class Activity : BaseEntity
    {
        public string Description {get; set;}
        public DateTime PlannedStart {get; set;}
        public DateTime PlannedFinish {get; set;}

        public DateTime EstimatedStart {get; set;}
        public DateTime EstimatedFinish {get; set;}

        public DateTime ActualStart {get; set;}
        public DateTime ActualFinish {get; set;}



        public ActivityStatus ActivityStatus {get; set;}

        public int Order { get; set; }
    }


    public enum ActivityStatus 
    {
        NotStarted = 1,
        InProcess = 2,
        Delayed = 3,
        Extended = 4,
        Completed = 5,
        Closed = 6,
    }
}