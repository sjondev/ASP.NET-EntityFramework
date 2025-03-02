using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class Category 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<Post> Posts { get; set; }
    }
}