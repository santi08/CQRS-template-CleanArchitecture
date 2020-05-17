using Application.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books
{
    public class List
    {
        public class Query : IRequest<PagedList<BookDto>>
        {

            public int? PageSize { get; set; }
            public int? PageNumber { get; set; }

            public Query(int? pageSize, int? pageNumber)
            {
                PageSize = pageSize;
                PageNumber = pageNumber;
            }

        }

        public class Handler : IRequestHandler<Query, PagedList<BookDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<BookDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Books.AsQueryable();

                var books = await queryable.Skip((request.PageNumber - 1) ?? 0)
                                          .Take(request.PageSize ?? 5)
                                          .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

                return new PagedList<BookDto>
                {
                    Items = books,
                    TotalItems = queryable.Count()
                };
            }
        }
    }
}
