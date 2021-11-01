using MyBookAPI.Domain.Common;

namespace MyBookAPI.Domain.Entities
{
    public class Review : AuditableEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Stars { get; set; }
        public int? BookId { get; set; }
        public Book Book { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
