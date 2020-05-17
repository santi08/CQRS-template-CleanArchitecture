using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Books
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(b => b.Author, o => o.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}"));
        }
    }

}
