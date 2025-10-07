

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Threading.Channels;

namespace DBA_WebAPI.Models;

public partial class Publisher
{
    //An unhandled exception occurred while processing the request.
    //SqlException: Cannot insert the value NULL into column 'type', table 'BookStoresDB.dbo.Book'; column does not allow nulls.UPDATE fails.
    //Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction)
    //DbUpdateException: An error occurred while saving the entity changes.See the inner exception for details.
    //Microsoft.EntityFrameworkCore.Update.AffectedCountModificationCommandBatch.ConsumeResultSetAsync(int startCommandIndex, RelationalDataReader reader, CancellationToken cancellationToken)
    public Publisher()
    {
        Books = new HashSet<Book>();
        Users = new HashSet<User>();
    }

    public int PubId { get; set; }

    public string? PublisherName { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Book> Books { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; }
}
