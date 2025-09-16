namespace ShopTARgv24.Core.Dto
{
    public class FileToApiDto
    {
        public Guid Id { get; set; }
        public string? ExistingFilePath { get; set; }
        public Guid? SpaceshipId { get; set; }
    }
}
