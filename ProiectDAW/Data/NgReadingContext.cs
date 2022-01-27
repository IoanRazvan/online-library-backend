using Microsoft.EntityFrameworkCore;
using ProiectDAW.Models;


namespace ProiectDAW.Data
{
    public class NgReadingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UsersSettings { get; set; }
        public DbSet<DirectLoginUser> DirectLoginUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<LibraryBook> LibrariesBooks { get; set; }

        public NgReadingContext(DbContextOptions<NgReadingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(user => user.UserSettings)
                .WithOne(userSettings => userSettings.User);

            builder.Entity<DirectLoginUser>()
                .HasOne(directUser => directUser.User)
                .WithOne(user => user.DirectLoginUser);

            builder.Entity<User>()
                .HasMany(user => user.Uploads)
                .WithOne(book => book.Uploader)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Review>().HasKey(review => new { review.BookId, review.ReviewerId });

            builder.Entity<Review>()
                .HasOne(review => review.Reviewer)
                .WithMany(reviewer => reviewer.Reviews);

            builder.Entity<Review>()
                .HasOne(review => review.Book)
                .WithMany(book => book.Reviews);

            builder.Entity<LibraryBook>()
                .HasKey(libraryBook => new { libraryBook.LibraryId, libraryBook.BookId });

            builder.Entity<LibraryBook>()
                .HasOne(libraryBook => libraryBook.Book)
                .WithMany(book => book.Libraries);

            builder.Entity<LibraryBook>()
                .HasOne(libraryBook => libraryBook.Library)
                .WithMany(library => library.Books);

            base.OnModelCreating(builder);
        }
    }
}
