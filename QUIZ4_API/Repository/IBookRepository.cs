using QUIZ4_API.Models;

namespace QUIZ4_API.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookModel>> GetBooksAsync();
        Task<BookModel> GetBookByIdAsync(int bookId);
        Task<BookModel> AddBookAsync(BookModel book);
        Task<IEnumerable<BookModel>> AddBooksAsync(IEnumerable<BookModel> books);
        Task AddToFavoriteAsync(string userId, int bookId);
        Task<IEnumerable<BookModel>> GetFavoriteBooksAsync(string userId);
    }
}
