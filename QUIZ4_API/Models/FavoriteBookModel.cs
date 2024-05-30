using Microsoft.AspNetCore.Identity;

namespace QUIZ4_API.Models
{
    public class FavoriteBookModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DateAdded { get; set; }

        public IdentityUser User { get; set; }
        public BookModel Book { get; set; }
    }
}