using System;
using System.Linq;
using System.Threading.Tasks;
using AgileFootPrints.API.Data;
using AgileFootPrints.API.Dtos;
using AgileFootPrints.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgileFootPrints.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SprintController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SprintController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpGet("getSprints/{projectId}")]
        public async Task<IActionResult> GetSprints(string projectId)
        {
            int pId = Convert.ToInt32(projectId);
            var sprints = await _context.Sprints.Include(x => x.Stories).Where(x => x.projectId == pId).ToListAsync();
            return Ok(sprints);
        }


        [HttpPost("newSprint/{projectId}")]
        public async Task<IActionResult> NewSprint(string projectId, [FromBody]SprintDto sprint)
        {
            if (projectId == null || projectId == "")
                return BadRequest("Project doesnt exists");
            if (sprint.SprintName == null)
                return BadRequest("Sprint Name is required");
            sprint.projectId = Convert.ToInt32(projectId);
            sprint.StatusId=1;
            var sprintToSave = _mapper.Map<Sprint>(sprint);

            await _context.Sprints.AddAsync(sprintToSave);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("GetSprint", new
            {
                id = sprintToSave.Id
            }, sprintToSave);
        }
        [HttpGet("{id}", Name = "GetSprint")]
        public async Task<IActionResult> GetSprint(int id)
        {
            var sprint = await _context.Sprints.FindAsync(id);
            if (sprint == null)
            {
                return NotFound("Some error occured");
            }
            var sprintToReturn = _mapper.Map<SprintDto>(sprint);
            return Ok(sprintToReturn);
        }

        [HttpPatch("setSprintDates/{sprintId}")]
        public async Task<IActionResult> SetSprintDates(string sprintId, SprintDatesDto sprintDatesDto)
        {

            var sprint = await _context.Sprints.FindAsync(Convert.ToInt32(sprintId));
            if (sprint == null)
                return BadRequest("Sprint doesn't exists");
            DateTime startDate = TimeZone.CurrentTimeZone.ToLocalTime(sprintDatesDto.StartDate);
            DateTime endDate = TimeZone.CurrentTimeZone.ToLocalTime(sprintDatesDto.EndDate);
            int chk = DateTime.Compare(startDate, endDate);
            if (chk > 0)
            {
                return BadRequest("Dates are not valid");
            }
            int currentDateChk = DateTime.Compare(startDate, DateTime.Now);
            if (currentDateChk < 0)
            {
                return BadRequest("Start date for sprint is before the current date");
            }


            sprint.StartDate = startDate;
            sprint.EndDate = endDate;
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }
    }



}