using Application.Errors;
using AutoMapper;
using Domain;
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
    public class Create
    {
        public class Command : IRequest
        {
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
                var author = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.AuthorId);

                if (author == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { error = "Author not found" });
                }

                var book = new Book
                {
                   Name = request.Name,
                   Author = author,
                   Category = request.Category
                };

                _context.Books.Add(book);
                var success = await _context.SaveChangesAsync() > 0;


                if (success) return Unit.Value;

                throw new Exception("Can't save changes");
            }
        }
    }
}
