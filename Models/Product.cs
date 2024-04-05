
namespace CrudUsers0.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? UserId { get; set; }
    }
}

