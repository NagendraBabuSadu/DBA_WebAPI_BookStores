namespace DBA_WebAPI.Models;

public partial class User
{
    public int UserId { get; set; }
    // Just eding 

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public short RoleId { get; set; }

    public int PubId { get; set; }

    public DateTime? HireDate { get; set; }

    public virtual Publisher Publisher { get; set; }
    public virtual Role Role{ get; set; }
}
