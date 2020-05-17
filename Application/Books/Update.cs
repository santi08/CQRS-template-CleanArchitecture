using Application.Errors;
using FluentValidation;
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
    public class Update
    {
        public class Command : IRequest
        {
            public Guid BookId { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string AuthorId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Category).NotEmpty();
                RuleFor(x => x.AuthorId).NotEmpty();

            }
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

                var author = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.AuthorId);

                if (author == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { error = "Author not found" });
                }

                book.Name = request.Name;
                book.Category = request.Category;
                book.Author = author;

             

                _context.Books.Update(book);
                var success = await _context.SaveChangesAsync() > 0;


                if (success) return Unit.Value;

                throw new Exception("Can't save changes");
            }
        }
    }
}
