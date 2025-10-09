namespace ShopTARgv24.Models.Kindergardens

{
    public class KindergardenImageViewModel
    {
        public Guid? Id { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public string? Image { get; set; }
        public Guid? RealEstateId { get; set; }
    }
}