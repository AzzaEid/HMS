using HMS.Data.Commons;

namespace HMS.Data.Entities
{
    public class Person : GeneralLocalizableEntity
    {
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
    }
}
