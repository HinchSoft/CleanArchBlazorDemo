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
                
            
            if(string.IsNullOrEmpty(orderby))
            {
                qAuthors = qAuthors.OrderBy(a => a.Id);
            }
            else
            {
                //when ordering the first order returns a IOrderedQueryable that additional ordering 
                // should be appended to.
                // EF.Property is used to get the key selector of the named column
                var sortArray = orderby.Split(',');
                IOrderedQueryable<Domain.Model.Author>? sortOrder=null;
                foreach(var sort in sortArray)
                {
                    var ss = sort.Split('-');
                    if (sortOrder is null)
                    {
                        if (ss[0] == "Ascending")
                            sortOrder = qAuthors.OrderBy(p => EF.Property<Domain.Model.Author>(p, ss[1]));
                        else
                            sortOrder = qAuthors.OrderByDescending(p => EF.Property<Domain.Model.Author>(p, ss[1]));
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
                    qAuthors = qAuthors.OrderBy(a => a.Id);
                else
                    qAuthors = sortOrder.ThenBy(a => a.Id);
            }

            // Skip and take to be after any ordering
            if (skip > 0)
            {
                qAuthors = qAuthors.Skip(skip);
            }
            if(take>0)
            {
                qAuthors = qAuthors.Take(take);
            }

            return Ok(qAuthors.Select(a=>new Author(a.FullName,a.DateOfBirth)).AsAsyncEnumerable());
        }

        [HttpPut]
        public IActionResult PutAuthor(Author author)
        {
            return Ok();
        }
    }
}
