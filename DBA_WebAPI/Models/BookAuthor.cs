namespace DBA_WebAPI.Models;

public partial class BookAuthor
{
    public int AuthorId { get; set; }

    public int BookId { get; set; }

    public byte? AuthorOrder { get; set; }

    public int? RoyalityPercentage { get; set; }
}
