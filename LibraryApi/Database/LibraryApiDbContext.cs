using LibraryApi.Database.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Database
{
    public class LibraryApiDbContext : IdentityDbContext<IdentityUser>
    {
        public LibraryApiDbContext(DbContextOptions<LibraryApiDbContext> options) : base(options)
        {

        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<FavBookModel> FavoriteBooks { get; set; }

    }
}
