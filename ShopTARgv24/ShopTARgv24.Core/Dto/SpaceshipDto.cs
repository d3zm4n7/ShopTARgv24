using Microsoft.AspNetCore.Http;

namespace ShopTARgv24.Core.Dto
{
    public class SpaceshipDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? TypeName { get; set; }
        public DateTime? BuiltDate { get; set; }
        public int? Crew { get; set; }
        public int? EnginePower { get; set; }
        public int? Passengers { get; set; }
        public int? InnerVolume { get; set; }

        //Tuleb teha muutuja Files ja see peab olema listis
        public List<IFormFile> Files { get; set; }

        public IEnumerable<FileToApiDto> FileToApiDtos { get; set; }
            = new List<FileToApiDto>();

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
