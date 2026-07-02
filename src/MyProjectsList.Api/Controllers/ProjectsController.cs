using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectsList.Api.Data; // Посилання на вашу папку Data
using MyProjectsList.Api.Models; // Посилання на вашу папку Models

namespace MyProjectsList.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")] // Це означає, що маршрут до контролера буде: api/projects
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

// -----------------------------------
        // GET: api/projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

// -----------------------------------
        // POST: api/projects
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, project);
        }
// -----------------------------------
    }
}