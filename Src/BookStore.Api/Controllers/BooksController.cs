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

            var qBooks = _storeContext.Books.Include(b=>b.Authors).AsQueryable();
                
            
            if(string.IsNullOrEmpty(orderby))
            {
                qBooks = qBooks.OrderBy(a => a.Id);
            }
            else
            {
                //when ordering the first order returns a IOrderedQueryable that additional ordering 
                // should be appended to.
                // EF.Property is used to get the key selector of the named column
                var sortArray = orderby.Split(',');
                IOrderedQueryable<Domain.Model.Book>? sortOrder=null;
                foreach(var sort in sortArray)
                {
                    var ss = sort.Split('-');
                    if (sortOrder is null)
                    {
                        if (ss[0] == "Ascending")
                            sortOrder = qBooks.OrderBy(p => EF.Property<Domain.Model.Author>(p, ss[1]));
                        else
                            sortOrder = qBooks.OrderByDescending(p => EF.Property<Domain.Model.Author>(p, ss[1]));
                    }
                    else
                    {
                        if (ss[0] == "Ascending")
                            sortOrder = sortOrder.ThenBy(p => EF.Property<Domain.Model.Author>(p, ss[1]));
                        else
                            sortOrder = sortOrder.ThenByDescending(p => EF.Property<Domain.Model.Author>(p, ss[1]));
                    }
                }
                if (sortOrder is null)
                    qBooks = qBooks.OrderBy(a => a.Id);
                else
                    qBooks = sortOrder.ThenBy(a => a.Id);
            }

            // Skip and take to be after any ordering
            if (skip > 0)
            {
                qBooks = qBooks.Skip(skip);
            }
            if(take>0)
            {
                qBooks = qBooks.Take(take);
            }

            return Ok( qBooks.Select(b=>b.MapToDto()).AsAsyncEnumerable());
        }

        [HttpPut]
        public IActionResult PutAuthor(Author author)
        {
            return Ok();
        }
    }
}
