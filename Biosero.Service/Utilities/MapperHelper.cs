using AutoMapper;
using Biosero.Data.Models;
using Biosero.Service.Models;
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
            }));
        }
    }
}
