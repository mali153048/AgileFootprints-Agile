using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileFootPrints.API.Data;
using AgileFootPrints.API.Dtos;
using AgileFootPrints.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgileFootPrints.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoryController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public StoryController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpGet("getProjetcStories/{id}")]
        public async Task<IActionResult> GetProjetcStories(string id)
        {
            int Id = Convert.ToInt32(id);
            var stories = await _context.Stories.Where(x => x.PriorityId == Id).Include(e => e.Epic).ToListAsync();
            return Ok(stories);
        }


        [HttpDelete("deleteStory/{id}")]
        public async Task<ActionResult> DeleteStory(string id)
        {
            int storyId = Convert.ToInt32(id);
            var storyToDelete = await _context.Stories.FindAsync(storyId);
            if (storyToDelete == null)
                return NotFound();
            _context.Stories.Remove(storyToDelete);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost("createNewStory")]
        public async Task<IActionResult> CreateNewStory(StoryDto storyDto)
        {
            storyDto.StatusId = 1;
            var storyToSave = _mapper.Map<Story>(storyDto);
            _context.Stories.Add(storyToSave);
            await _context.SaveChangesAsync();
            int sid = storyToSave.Id;
            return CreatedAtRoute("GetNewlyCreatedStory",
            new { id = storyToSave.Id }, storyToSave);

        }

        [HttpGet("{id}", Name = "GetNewlyCreatedStory")]
        public async Task<ActionResult<List<Story>>> GetNewlyCreatedStory(int id)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story != null)
            {
                var storyToReturn = await _context.Stories.Where(x => x.Id == id).Include(x => x.Epic).ToListAsync();
                return storyToReturn;
            }

            return NotFound();
        }

        [HttpGet("getStory/{id}")]
        public async Task<IActionResult> GetStory(string id)
        {
            int storyId = Convert.ToInt32(id);
            var story = await _context.Stories
            .Include(x => x.Epic)
            .Include(x => x.Sprint)
            .Include(x => x.User)
            .Where(x => x.Id == storyId).FirstOrDefaultAsync();
            if (story == null)
            {
                return NotFound();
            }
            //var storyToReturn = _mapper.Map<StoryDto>(story);
            return Ok(story);

        }

        [HttpPatch("editStory/{id}")]
        public async Task<IActionResult> EditStory(string id, StoryDto storyPatch)
        {
            int storyId = Convert.ToInt32(id);
            Story story = await _context.Stories.FindAsync(storyId);
            if (story == null)
            {
                return NotFound();
            }
            story.StoryName = storyPatch.StoryName;
            story.StoryDescription = storyPatch.StoryDescription;
            story.AcceptanceCriteria = storyPatch.AcceptanceCriteria;
            story.EpicId = storyPatch.EpicId;
            story.PriorityId = storyPatch.PriorityId;
            story.SprintId = storyPatch.SprintId;
            story.UserId = storyPatch.UserId;
            await _context.SaveChangesAsync();

            return Ok(storyPatch);
        }

        [HttpPost("updateStoryStatus/{statusId}")]
        public async Task<IActionResult> UpdateStoryStatus(int statusId, [FromBody]Story[] stories)
        {
            if (stories.Length == 0)
            {
                return BadRequest("No story to move");
            }
            for (int i = 0; i < stories.Length; i++)
            {
                if (stories[i].StatusId != statusId)
                {
                    var story = await _context.Stories.FindAsync(stories[i].Id);
                    story.StatusId = statusId;
                    await _context.SaveChangesAsync();
                }
            }

            return Ok();

        }

        [HttpPatch("updateStorySprintStatus/{storyId}/{sprintId}")]
        public async Task<IActionResult> UpdateStorySprintStatus(string storyId, string sprintId)
        {
            var story = await _context.Stories.FindAsync(Convert.ToInt32(storyId));
            var sprint = _context.Sprints.FindAsync(Convert.ToInt32(sprintId));
            if (story == null || sprint == null)
                return BadRequest("Sprint or story not found");
            story.SprintId = Convert.ToInt32(sprintId);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("userTasks/{username}")]
        public async Task<IActionResult> GetUserTasks(string username)
        {
            var user = _context.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (user == null)
                return BadRequest();

            var data = await (from story in _context.Stories

                              where story.UserId == user.Id
                              select new
                              {
                                  storyid = story.Id,
                                  story.StoryName,
                                  story.StoryDescription,
                                  story.Status.status,
                                  statusId = story.Status.Id,
                                  projectId = story.Project.Id,
                                  story.Project.ProjectName,
                                  story.Project.ProjectDescription,
                                  story.Project.ProjectKey,
                                  epicId = story.Epic.Id,
                                  story.Epic.EpicName,
                                  story.Epic.EpicDescription,
                                  userId = story.Project.User.Id,
                                  sprintId = story.Sprint.Id,
                                  story.Sprint.SprintName,
                                  story.Project.User.UserName
                              }).ToArrayAsync();

            return Ok(data);
        }

    }
}