using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Books;
using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController
    {

        [HttpGet]
        public async Task<PagedList<BookDto>> List([FromQuery] int? pageSize, [FromQuery] int? pageNumber)
        {
            return await Mediator.Send(new List.Query(pageSize, pageNumber));
        }

        [HttpPost]
        public async Task<Unit> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{bookId}")]
        public async Task<BookDto> Details(Guid bookId)
        {
            return await Mediator.Send(new Details.Query { BookId = bookId });
        }

        [HttpPut("{bookId}")]
        public async Task<ActionResult<Unit>> Update(Guid bookId, Update.Command command)
        {
            command.BookId = bookId;
            return await Mediator.Send(command);
        }

        [HttpDelete("{bookId}")]
        public async Task<ActionResult<Unit>> Delete(Guid bookId)
        {

            return await Mediator.Send(new Delete.Command { BookId = bookId });
        }


    }
}