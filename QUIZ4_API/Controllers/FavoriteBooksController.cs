using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QUIZ4_API.Models;
using QUIZ4_API.Repository;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteBooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public FavoriteBooksController(IBookRepository bookRepository, UserManager<IdentityUser> userManager)
        {
            _bookRepository = bookRepository;
            _userManager = userManager;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddToFavorite([FromBody] FavoriteBookRequestModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Check if the book exists
            var book = await _bookRepository.GetBookByIdAsync(model.BookId);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }

            await _bookRepository.AddToFavoriteAsync(user.Id, model.BookId);
            return Ok(new { message = "Book added to favorites" });
        }

        [HttpGet("favorites")]
        [Authorize]
        public async Task<IActionResult> GetFavoriteBooks()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var favoriteBooks = await _bookRepository.GetFavoriteBooksAsync(user.Id);
            return Ok(favoriteBooks);
        }
    }
}