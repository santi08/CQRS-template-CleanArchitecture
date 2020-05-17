using Application.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid BookId { get; set; }
        }


        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.BookId == request.BookId);

                if (book == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { error = "Book not found" });
                }

                _context.Books.Remove(book);
                var success = await _context.SaveChangesAsync() > 0;


                if (success) return Unit.Value;

                throw new Exception("Can't save changes");
            }
        }
    }
}
