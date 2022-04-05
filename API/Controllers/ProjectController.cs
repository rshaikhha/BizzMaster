using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class ProjectController : BaseApiController
    {
        private readonly BMContext _context;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(BMContext context, ILogger<ProjectController> logger)
        {
            this._context = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> Get()
        {
            return await _context.Projects
            .Include(x => x.Order)
            .ThenInclude(x => x.SupplyLine)
            .Include(x => x.Activities)
            .Select(x => ToDto(x))
            .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> Get(int id)
        {
            var ent = await _context.Projects.FindAsync(id);
            if (ent == null) return BadRequest();
            _context.Entry(ent).Reference(x => x.Order).Load();
            _context.Entry(ent).Reference(x => x.Order.SupplyLine).Load();
            _context.Entry(ent).Collection(x => x.Activities).Load();

            return ToDto(ent);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(ProjectUploadDto dto)
        {

            //if(!dto.Items.Any()) return BadRequest(new ProblemDetails{Title = "No Items!!!"});

            var supplyLine = _context.SupplyLines.Find(dto.SupplyLineId);
            var Order = _context.Orders.Find(dto.OrderId);
            if (supplyLine == null || Order == null)
            {
                return BadRequest();
            }
            var sd = dto.StartDate;
            var fd = dto.FinishDate;
            DateTime startDate, finishDate;
            var hasStart = DateTime.TryParseExact(sd, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out startDate);
            var hasFinish = DateTime.TryParseExact(fd, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out finishDate);

            if (!hasStart && !hasFinish) return BadRequest();
            if (!hasStart) startDate = finishDate.AddDays(-dto.Items.Sum(x=>x.Duration));
            

            //string title = string.Format("Project-{0}-{1}", dto.SupplyLineId, dto.OrderId);
            var activities = new List<Activity>();
            for (int i = 0; i < dto.Items.Count; i++)
            {   
                string itemTitle = dto.Items[i].Title;
                DateTime itemPlannedStart = startDate.AddDays(dto.Items.Take(i).Sum(x=>x.Duration));
                DateTime itemPlannedFinish = itemPlannedStart.AddDays(dto.Items[i].Duration);
                
                activities.Add(new Activity{
                    Title = itemTitle,
                    PlannedStart = itemPlannedStart,
                    PlannedFinish = itemPlannedFinish,
                    EstimatedStart = itemPlannedStart,
                    EstimatedFinish = itemPlannedFinish,
                    ActivityStatus = ActivityStatus.NotStarted,
                    Order = i
                });
            }

            var project = new Project
            {
                Title = dto.Title,
                OrderId = dto.OrderId,
                Activities = activities,
            };
            _context.Projects.Add(project);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return project.Id;
            }
            return BadRequest();

        }


        private static ProjectDto ToDto(Project ent)
        {
            return new ProjectDto
            {
                Id = ent.Id,
                Title = ent.Title,
                OrderId = ent.OrderId ?? -1,
                OrderTitle = ent.Order?.Title ?? "",
                SupplyLine = ent.Order.SupplyLine.Title,
                Activities = ent.Activities.OrderBy(x => x.Order).Select(x => ToDto(x)).ToList(),

            };
        }
        private static ActivityDto ToDto(Activity ent)
        {
            return new ActivityDto
            {
                Id = ent.Id,
                Title = ent.Title,
                Description = ent.Description,
                PlannedStart = ent.PlannedStart,
                PlannedFinish = ent.PlannedFinish,
                EstimatedStart = ent.EstimatedStart,
                EstimatedFinish = ent.EstimatedFinish,
                ActualStart = ent.ActualStart,
                ActualFinish = ent.ActualFinish,
                ActivityStatus = ent.ActivityStatus.ToString(),
                Order = ent.Order
            };

        }
    }
}