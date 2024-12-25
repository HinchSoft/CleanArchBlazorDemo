using BookStore.Api.Dtos;
using BookStore.Core.Repositories;
using CommonCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorsController(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public IAsyncEnumerable<Author> GetAuthors()
        {
            return _authorRepository.GetAllAsync<Author>();
        }

        [HttpPut]
        public IActionResult PutAuthor(Author author)
        {
            return Ok();
        }
    }
}
