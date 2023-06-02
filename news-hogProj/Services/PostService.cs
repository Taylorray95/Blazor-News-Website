using Microsoft.EntityFrameworkCore;
using news_hogProj.Data;
using news_hogProj.Objects;

namespace news_hogProj.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetRecentPosts(int amountToReturn)
        {
            return await _context.Posts.OrderByDescending(p => p.PostSysDate).Take(amountToReturn).ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts.Where(p => p.PostId == id).FirstOrDefaultAsync()!;
        }

        public async Task<List<Comment>> GetCommentsByPostId(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId && c.ParentCommentId == null) // Only get top-level comments
                .Include(c => c.Replies) // Include replies
                .ToListAsync();
        }

        public async Task AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task IncrementPageViews(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post != null)
            {
                post.PageViews++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task IncrementTotalComments(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post != null)
            {
                post.TotalComments++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DecrementTotalComments(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post != null)
            {
                post.TotalComments--;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Post>> GetTrendingPosts(int amountToReturn, int daysToGoBack)
        {
            var dateLimit = DateTime.Now.AddDays(-daysToGoBack); // posts from the last 45 days

            return await _context.Posts
                .Where(p => p.PostSysDate >= dateLimit)
                .OrderByDescending(p => p.PageViews + (p.TotalComments * 2))
                .Take(amountToReturn)
                .ToListAsync();
        }



    }
}
