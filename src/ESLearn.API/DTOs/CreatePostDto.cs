namespace ESLearn.API.DTOs
{
    public class CreatePostDto
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}