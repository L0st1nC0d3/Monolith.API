namespace Monolith.Models
{
    public class CreatePost : EditPost
    {
        public int AuthorId { get; set; }
    }

    public class EditPost
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
