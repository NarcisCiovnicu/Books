namespace DataAccess.Entities
{
    public class Book : BaseEntity<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[] CoverPhoto { get; set; } = Array.Empty<byte>();
        public IList<Author> Authors { get; set; } = new List<Author>();
    }
}
