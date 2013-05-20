using System.ComponentModel.DataAnnotations;

namespace Blog.DAL.Model
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }
        
        public long PostId { get; set; }

        public string Content { get; set; }
    }
}
