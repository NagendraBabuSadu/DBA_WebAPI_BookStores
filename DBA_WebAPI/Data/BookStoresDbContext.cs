using DBA_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DBA_WebAPI.Data;

public partial class BookStoresDbContext : DbContext
{
    public BookStoresDbContext()
    {
    }

    public BookStoresDbContext(DbContextOptions<BookStoresDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookAuthor> BookAuthors { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=BookStoresDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__86516BCF38EE6880");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Address)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("state");
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("zip");
        });


    // An unhandled exception occurred while processing the request.
    // SqlException: Cannot insert the value NULL into column 'type', table 'BookStoresDB.dbo.Book'; column does not allow nulls.UPDATE fails.
    //Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, bool breakConnection, Action < Action > wrapCloseInAction)
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__490D1AE1B4346AA0");

            entity.ToTable("Book");

            entity.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PubId)
                .HasPrincipalKey(p => p.PubId);

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Advance)
                .HasColumnType("money")
                .HasColumnName("advance");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("notes");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.PubId).HasColumnName("pub_id");
            entity.Property(e => e.PublishedDate)
                .HasColumnType("datetime")
                .HasColumnName("published_date");
            entity.Property(e => e.Royalty).HasColumnName("royalty");
            entity.Property(e => e.Title)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("type");
            entity.Property(e => e.YtdSales).HasColumnName("ytd_sales");
        });



        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(e => new { e.AuthorId, e.BookId });

            entity.ToTable("BookAuthor");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.AuthorOrder).HasColumnName("author_order");
            entity.Property(e => e.RoyalityPercentage).HasColumnName("royality_percentage");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Job__6E32B6A51621236F");

            entity.ToTable("Job");

            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.JobDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("job_desc");
        });

     

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PubId).HasName("PK__Publishe__2515F22293A5E5A6");

            entity.ToTable("Publisher");

            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.Pub)
            //    .WithMany(p => p.Users)
            //    .HasForeignKey(u => u.PubId)
            //    .HasPrincipalKey(p => p.PubId);



            entity.Property(e => e.PubId).HasColumnName("pub_id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("publisher_name");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("state");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId);

            entity.ToTable("RefreshToken");

            entity.Property(e => e.TokenId).HasColumnName("token_id");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("expiry_date");
            entity.Property(e => e.Token)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC347AAEAF");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_desc");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK_Sale2");

            entity.ToTable("Sale");

            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderNum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("order_num");
            entity.Property(e => e.PayTerms)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("pay_terms");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("store_id");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("UPK_storeid");

            entity.ToTable("Store");

            entity.Property(e => e.StoreId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("store_id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("state");
            entity.Property(e => e.StoreAddress)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("store_address");
            entity.Property(e => e.StoreName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("store_name");
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("zip");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasKey(e => e.UserId);  // ✅ Declare primary key

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");


            entity.HasOne(u => u.Publisher)
                .WithMany(p => p.Users)
                .HasForeignKey(b => b.PubId)
                .HasPrincipalKey(p => p.PubId);

            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasColumnType("datetime")
                .HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PubId).HasColumnName("pub_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("source");
      
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
