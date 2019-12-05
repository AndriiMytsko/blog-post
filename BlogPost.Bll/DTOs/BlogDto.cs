namespace BlogPost.Bll.DTOs
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
