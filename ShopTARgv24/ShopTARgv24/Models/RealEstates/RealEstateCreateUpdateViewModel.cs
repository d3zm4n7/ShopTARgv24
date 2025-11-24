

using System.ComponentModel.DataAnnotations;

namespace ShopTARgv24.Models.RealEstates
{
    public class RealEstateCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Area cannot be negative")]
        public double? Area { get; set; }
        public string? Location { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Room Number can't be negative")]
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }
        public List<IFormFile>? Files { get; set; }
        public List<RealEstateImageViewModel>? Image { get; set; }
            = new List<RealEstateImageViewModel>();

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
