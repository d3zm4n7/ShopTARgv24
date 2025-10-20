namespace ShopTARgv24.Models.Kindergardens
{
    public class KindergardenDeleteViewModel
    {
        public Guid? Id { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergardenName { get; set; }
        public string? TeacherName { get; set; }
        public List<KindergardenImageViewModel> Image { get; set; }
            = new List<KindergardenImageViewModel>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
