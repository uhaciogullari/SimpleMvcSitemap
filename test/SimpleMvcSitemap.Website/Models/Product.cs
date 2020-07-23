namespace SimpleMvcSitemap.Website.Models
{
    public class Product
    {
        public int Id { get; set; }
        public ProductStatus Status { get; set; }
    }

    public enum ProductStatus
    {
        Active, Passive
    }
}