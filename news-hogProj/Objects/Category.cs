namespace news_hogProj.Objects
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryDesc { get; set; }
        public string? CategoryHashTag { get; set; }
        public DateTime SysDate { get; set; } = DateTime.Now;
    }

}
