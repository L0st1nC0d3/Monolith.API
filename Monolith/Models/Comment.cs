namespace Monolith.Models
{
    public class Comment
    {
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}
