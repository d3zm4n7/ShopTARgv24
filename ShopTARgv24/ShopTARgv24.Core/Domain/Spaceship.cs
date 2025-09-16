namespace ShopTARgv24.Core.Domain
{
    public class Spaceship
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? TypeName { get; set; }
        public DateTime? BuiltDate { get; set; }
        public int? Crew { get; set; }
        public int? EnginePower { get; set; }
        public int? Passengers { get; set; }
        public int? InnerVolume { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
