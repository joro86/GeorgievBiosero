using AutoMapper;
using Biosero.Data.Models;
using Biosero.Service.Models;
using Biosero.Service.Models.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biosero.Service.Utilities
{
    public static class MapperHelper
    {
        public static Mapper Mapper { get; set; }

        static MapperHelper()
        {
            Mapper = new Mapper(new MapperConfiguration(config => {
                config.CreateMap<Book, BookDto>();
                config.CreateMap<BookDto, Book>();

                config.CreateMap<BookRequest, Book>()
                .ForMember(x => x.Author, opt => opt.Ignore() );

                config.CreateMap<AuthorDto, Author>();
                config.CreateMap<Author, AuthorDto>();


                config.CreateMap<UserDto, User>();
                config.CreateMap<User, UserDto>();
            }));
        }
    }
}
