using AutoMapper;
using MyBookAPI.Application.Common.Mappings;
using MyBookAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail
{
    public class AuthorDetailVm : IMapFrom<Author>
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public ICollection<string> Books { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Author, AuthorDetailVm>()
                   .ForMember(d => d.FullName, map => map.MapFrom(src => src.AuthorName.ToString()))
                   .ForMember(d => d.Description, map => map.MapFrom<DescriptionResolver>())
                   .ForMember(d => d.Books, map => map.MapFrom(src => src.Books.Select(x => x.Name).ToList()));
        }

        private class DescriptionResolver : IValueResolver<Author, object, string>
        {
            public string Resolve(Author source, object destination, string destMember, ResolutionContext context)
            {
                if (source.Description.Text is not null)
                {
                    return source.Description.Text;
                }
                return string.Empty;
            }
        }
    }
}
