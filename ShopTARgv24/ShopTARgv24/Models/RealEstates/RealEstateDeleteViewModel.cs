namespace ShopTARgv24.Models.RealEstates
{
    public class RealEstateDeleteViewModel
    {
        public Guid? Id { get; set; }
        public double? Area { get; set; }
        public string? Location { get; set; }
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }
        public List<RealEstateImageViewModel>? Image { get; set; }
            = new List<RealEstateImageViewModel>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
