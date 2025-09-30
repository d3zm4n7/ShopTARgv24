using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARgv24.Core.Dto
{
    public class RealEstateDto
    {
        public Guid? Id { get; set; }
        public double? Area { get; set; }
        public string? Location { get; set; }
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
