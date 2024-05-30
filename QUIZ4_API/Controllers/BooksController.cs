using QUIZ4_API.Models;
using Microsoft.AspNetCore.Mvc;
using QUIZ4_API.Repository;
using QUIZ4_API.Utils;

namespace QUIZ4_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [ApiKeyAuthorize]
        [HttpGet("get-books")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetBooksAsync();
            return Ok(books);
        }

        [ApiKeyAuthorize]
        [HttpPost("add-single-book")]
        public async Task<IActionResult> AddBook([FromBody] BookModel book)
        {
            if (book == null)
            {
                return BadRequest("Book is null.");
            }

            var createdBook = await _bookRepository.AddBookAsync(book);
            return Ok(new ResponseModel { Status = "Success", Message = "Book added successfully" });
        }

        [ApiKeyAuthorize]
        [HttpPost("add-multiple-books")]
        public async Task<IActionResult> AddBooks([FromBody] IEnumerable<BookModel> books)
        {
            if (books == null)
            {
                return BadRequest("Books are null.");
            }

            var createdBooks = await _bookRepository.AddBooksAsync(books);
            return Ok(new ResponseModel { Status = "Success", Message = "Multiple books added successfully" });
        }
    }
}