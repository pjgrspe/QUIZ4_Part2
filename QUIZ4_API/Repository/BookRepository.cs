using QUIZ4_API.Models;
using Microsoft.EntityFrameworkCore;
using QUIZ4_API.Database;

namespace QUIZ4_API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookModel>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<BookModel> AddBookAsync(BookModel book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<BookModel>> AddBooksAsync(IEnumerable<BookModel> books)
        {
            _context.Books.AddRange(books);
            await _context.SaveChangesAsync();
            return books;
        }

        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }

        public async Task AddToFavoriteAsync(string userId, int bookId)
        {
            var favoriteBook = new FavoriteBookModel
            {
                UserId = userId,
                BookId = bookId,
                DateAdded = DateTime.UtcNow
            };
            _context.FavoriteBooks.Add(favoriteBook);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookModel>> GetFavoriteBooksAsync(string userId)
        {
            var favoriteBooks = await _context.FavoriteBooks
                .Where(fb => fb.UserId == userId)
                .Include(fb => fb.Book)
                .Select(fb => fb.Book)
                .ToListAsync();

            return favoriteBooks;
        }
    }
}
