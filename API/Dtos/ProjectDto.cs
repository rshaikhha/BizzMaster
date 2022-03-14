using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OrderId {get; set;}
        public string OrderTitle {get; set;}
        public string SupplyLine { get; set; }
        public List<ActivityDto> Activities {get; set;} = new List<ActivityDto>();

        public DateTime PlannedStart {get => this.Activities.Min(x=>x.PlannedStart);}
        public DateTime PlannedFinish {get => this.Activities.Max(x=>x.PlannedFinish);}

        public DateTime EstimatedStart {get => this.Activities.Min(x=>x.EstimatedStart);}
        public DateTime EstimatedFinish {get => this.Activities.Max(x=>x.EstimatedFinish);}

        public DateTime ActualStart {get => this.Activities.Min(x=>x.ActualStart);}
        public DateTime ActualFinish {get => this.Activities.Max(x=>x.ActualFinish);}

        public string PStart {get => this.PlannedStart.ToShortDateString();}
        public string PFinish  {get => this.PlannedFinish.ToShortDateString();}
        public string EStart  {get => this.EstimatedStart.ToShortDateString();}
        public string EFinish  {get => this.EstimatedFinish.ToShortDateString();}
        public string AStart  {get => this.ActualStart.ToShortDateString();}
        public string AFinish  {get => this.ActualFinish.ToShortDateString();}


        public string Status {get {
            var status = "";
            if (Activities.Any(x=>x.ActivityStatus != "NotStarted")) status = "In Process";
            if (!Activities.Any(x=>x.ActivityStatus != "Completed")) status = "Completed";
            if (!Activities.Any(x=>x.ActivityStatus != "Closed")) status = "Closed";
            return status;
            }}
        public int CompleteRate { get{
            var EstimatedDuration = (EstimatedFinish - EstimatedStart).Days;
            var Completion = (ActualFinish - EstimatedStart).Days;
            return (int)Math.Max(0,Math.Ceiling((double)Completion / (double)EstimatedDuration));
        } }
    }


    public class ActivityDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description {get; set;}
        public DateTime PlannedStart {get; set;}
        public DateTime PlannedFinish {get; set;}

        public DateTime EstimatedStart {get; set;}
        public DateTime EstimatedFinish {get; set;}

        public DateTime ActualStart {get; set;}
        public DateTime ActualFinish {get; set;}


        public string PStart { get => this.PlannedStart.ToShortDateString(); }
        public string PFinish { get => this.PlannedFinish.ToShortDateString(); }
        public string EStart { get => this.EstimatedStart.ToShortDateString(); }
        public string EFinish { get => this.EstimatedFinish.ToShortDateString(); }
        public string AStart { get => this.ActualStart.ToShortDateString(); }
        public string AFinish { get => this.ActualFinish.ToShortDateString(); }



        public string ActivityStatus {get; set;}

        public int Order { get; set; }


    }


    public class ProjectUploadDto
    {
        public string Title { get; set; }
        public int SupplyLineId { get; set; }
        public int OrderId { get; set; }

        public string StartDate {get; set;}
        public string FinishDate {get; set;}

        public List<ProjectUploadItemDto>  Items { get; set; }
    }

    public class ProjectUploadItemDto
    {
        public string Title { get; set; }
        public int Duration { get; set; }

        public int Order {get; set;}
    }


    
}