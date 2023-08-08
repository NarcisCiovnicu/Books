namespace DataAccess.Entities
{
    public class Author : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
