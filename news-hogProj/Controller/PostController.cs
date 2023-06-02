using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using news_hogProj.Data;

namespace news_hogProj.Controller
{
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ... other methods ...

        [HttpGet("api/posts/getrecentposts")]
        public async Task<IActionResult> GetRecentPosts()
        {
            var recentPosts = await _context.Posts.OrderByDescending(p => p.PostSysDate).Take(10).ToListAsync();
            return Ok(recentPosts);
        }
    }
}
