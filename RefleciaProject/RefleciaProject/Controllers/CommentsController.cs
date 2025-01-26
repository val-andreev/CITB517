using Microsoft.AspNetCore.Mvc;
using RefleciaProject.Data;
using RefleciaProject.Models;

namespace RefleciaProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase 
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllComments()
        {
            var comments = _context.Comments.ToList();
            return Ok(comments);
        }

        [HttpPost]
        public IActionResult CreateComment([FromBody] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreatedAt = DateTime.UtcNow;
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return Ok(comment);
            }

            return BadRequest(ModelState); 
        }
    }
}
