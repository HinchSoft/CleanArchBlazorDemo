using BookStore.Api.Dtos;
using BookStore.Application.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreContext _storeContext;

        public BooksController(BookStoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        /// <summary>
        /// Returns a collection of Books or the count of Books if Count = true
        /// </summary>
        /// <param name="skip">Number of Books to skip</param>
        /// <param name="take">Number of Books to return</param>
        /// <param name="orderby">comma separated list of order by fields "Ascending|Descending-column"</param>
        /// <param name="count">Return only the count of Books</param>
        /// <returns></returns>
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        [ProducesResponseType<IAsyncEnumerable<Book>>(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetBooks([FromQuery]int skip=0, [FromQuery]int take = 0, [FromQuery]string? orderby = null, [FromQuery]bool count = false)
        {
            if (count)
                return Ok(_storeContext.Books.Count());

            var qBooks = _storeContext.Books
                .Include(b=>b.Authors)
                .Include(b=>b.Publisher)
                .AsNoTracking()
                .AsSplitQuery()
                .AsQueryable();

            qBooks = qBooks.Page(skip,take,b=>b.Id,orderby);
            
            return Ok( qBooks.Select(b=>b.MapToDto()).AsAsyncEnumerable());
        }

        [HttpPut]
        public IActionResult PutBook(Book author)
        {

            return Ok();
        }
    }
}
