namespace Data.Entities
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
