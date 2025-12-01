namespace ShopTARgv24.Models.Email
{
    public class EmailViewModel
    {
        public string? To { get; set; } = string.Empty;
        public string? Subject { get; set; } = string.Empty;
        public string? Body { get; set; } = string.Empty;
        public IFormFileCollection Attachment { get; set; }
    }
}
