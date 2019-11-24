namespace BlogPost.Dal.Entities
{
    public class CommentEntity: Entity
    {
        public string Text { get; set; }
        public int BlogId { get; set; }
        public virtual BlogEntity BlogEntity { get; set; }
    }
}