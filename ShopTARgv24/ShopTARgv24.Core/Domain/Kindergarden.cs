namespace ShopTARgv24.Core.Domain
{
    public class Kindergarden
    {
        public Guid? Id { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergardenName { get; set; }
        public string? TeacherName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Teha uus branch nimega Kindergarden
        //Teha uus CRUD lasamine, vaatamine, uuendamine ja andmete kustutamine.
        //Teemaks on Kindergarden
        //Muutujateks on ID, GroupName, ChildrenCount, KindergardenName, TeacherName, CreatedAt, UpdatedAt
        //Too on hindeline. Too panna githubi ja link saata emailile. Tunnis naitad ette
    }
}
