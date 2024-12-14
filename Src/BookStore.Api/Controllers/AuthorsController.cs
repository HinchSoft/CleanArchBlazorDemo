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
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreContext _storeContext;

        public AuthorsController(BookStoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        /// <summary>
        /// Returns a collection of Authors or the count of Authors if Count = true
        /// </summary>
        /// <param name="skip">Number of Authors to skip</param>
        /// <param name="take">Number of Authors to return</param>
        /// <param name="orderby">comma separated list of order by fields "Ascending|Descending-column"</param>
        /// <param name="count">Return only the count of Authors</param>
        /// <returns></returns>
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        [ProducesResponseType<IAsyncEnumerable<Author>>(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetAuthors([FromQuery]int skip=0, [FromQuery]int take = 0, [FromQuery]string? orderby = null, [FromQuery]bool count = false)
        {
            if (count)
                return Ok(_storeContext.Authors.Count());

            var qAuthors = _storeContext.Authors.AsQueryable();
            qAuthors = qAuthors.Page(skip, take, a=>a.Id, orderby);    
            
            return Ok(qAuthors.Select(a=>a.MapToDto()).AsAsyncEnumerable());
        }

        [HttpPut]
        public IActionResult PutAuthor(Author author)
        {
            return Ok();
        }
    }
}
