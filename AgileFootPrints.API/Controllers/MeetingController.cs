using System;
using System.Linq;
using System.Threading.Tasks;
using AgileFootPrints.API.Data;
using AgileFootPrints.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgileFootPrints.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeetingController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MeetingController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpPost("createMeeting")]
        [Obsolete]
        public async Task<IActionResult> CreateMeeting(Meeting Meeting)
        {
            Meeting.Date = TimeZone.CurrentTimeZone.ToLocalTime(Meeting.Date);
            await _context.Meetings.AddAsync(Meeting);
            await _context.SaveChangesAsync();
            return Ok(Meeting);
        }

        [HttpGet("getProjectMeetings/{projectId}")]
        public async Task<IActionResult> GetProjectMeetings(string projectId)
        {
            if (projectId == null || projectId == "")
            {
                return BadRequest();
            }
            var Meetings = await _context.Meetings.Where(x => x.ProjectId == Convert.ToInt32(projectId)).ToArrayAsync();
            return Ok(Meetings);
        }

        [HttpDelete("delete/{meetingId}")]
        public async Task<IActionResult> Delete(string meetingId)
        {
            var meetig = _context.Meetings.Find(Convert.ToInt32(meetingId));
            if (meetig == null)
                return NotFound();
            _context.Meetings.Remove(meetig);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("edit/{meetingId}")]
        [Obsolete]
        public async Task<IActionResult> Edit(string meetingId, Meeting Meeting)
        {
            var meeting = await _context.Meetings.FindAsync(Convert.ToInt32(meetingId));
            if (meeting == null)
                return NotFound();
            meeting.Subject = Meeting.Subject;
            meeting.Description = Meeting.Description;
            meeting.Date = TimeZone.CurrentTimeZone.ToLocalTime(Meeting.Date); ;
            meeting.StartTime = Meeting.StartTime;
            meeting.EndTime = Meeting.EndTime;
            meeting.Venue = Meeting.Venue;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}