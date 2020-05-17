using Application.Errors;
using AutoMapper;
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
    public class Details
    {
        public class Query : IRequest<BookDto>
        {

            public Guid BookId { get; set; }

        }

        public class Handler : IRequestHandler<Query, BookDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BookDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var book = await _context.Books.Include(b => b.Author)
                                               .FirstOrDefaultAsync(b => b.BookId == request.BookId);

                if (book == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { error = "Book not found" });

                }

                var bookDto = _mapper.Map<BookDto>(book);

                return bookDto;
            }
        }
    }
}
