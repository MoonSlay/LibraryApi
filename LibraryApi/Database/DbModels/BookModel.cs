using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Database.DbModels
{
    public class BookModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? PublisherName { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
