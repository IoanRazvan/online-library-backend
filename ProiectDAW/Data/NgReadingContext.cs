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
        public DbSet<LibraryBook> LibraryBooks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        
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

            builder.Entity<Library>()
                .HasOne(library => library.Owner)
                .WithMany(owner => owner.Libraries);

            builder.Entity<LibraryBook>().HasKey(libraryBook => new { libraryBook.BookId, libraryBook.LibraryId });

            builder.Entity<LibraryBook>()
                .HasOne(libraryBook => libraryBook.Book)
                .WithMany(book => book.LibraryBooks);

            builder.Entity<LibraryBook>()
                .HasOne(libraryBook => libraryBook.Library)
                .WithMany(library => library.LibraryBooks);

            builder.Entity<BookGenre>().HasKey(bookGenre => new { bookGenre.BookId, bookGenre.GenreId });

            builder.Entity<BookGenre>()
                .HasOne(bookGenre => bookGenre.Book)
                .WithMany(book => book.BookGenres);

            builder.Entity<BookGenre>()
                .HasOne(bookGenre => bookGenre.Genre)
                .WithMany(genre => genre.BookGenres);

            base.OnModelCreating(builder);
        }
    }
}
