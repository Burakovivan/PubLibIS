using AutoMapper;
using AuthorDLL = PubLibIS_BLL.Model.Author;
using AuthorDAL = PubLibIS_DAL.Model.Author;
using System.Collections.Generic;

namespace PubLibIS_BLL.Mappers
{
    public static class AuthorMapper
    {
        static AuthorMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AuthorDAL, AuthorDLL>();
                cfg.CreateMap<AuthorDLL, AuthorDAL>();
            });

        }
        public static AuthorDLL MapOneUp(AuthorDAL author)
        {
            return Mapper.Map<AuthorDAL, AuthorDLL>(author);
        }

        public static IEnumerable<AuthorDLL> MapManyUp(IEnumerable<AuthorDAL> authors)
        {
            return Mapper.Map<IEnumerable<AuthorDAL>, IEnumerable<AuthorDLL>>(authors);
        }

        public static AuthorDAL MapOneDown(AuthorDLL author)
        {
            return Mapper.Map<AuthorDLL, AuthorDAL>(author);
        }

        public static IEnumerable<AuthorDAL> MapManyUp(IEnumerable<AuthorDLL> authors)
        {
            return Mapper.Map<IEnumerable<AuthorDLL>, IEnumerable<AuthorDAL>>(authors);
        }

    }
}
