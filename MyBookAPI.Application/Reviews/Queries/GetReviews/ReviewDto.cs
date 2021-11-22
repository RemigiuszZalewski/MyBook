using AutoMapper;
using MyBookAPI.Application.Common.Mappings;
using MyBookAPI.Domain.Entities;

namespace MyBookAPI.Application.Reviews.Queries.GetReviews
{
    public class ReviewDto : IMapFrom<Review>
    {
        public int Stars { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }

        public void Mappings(Profile profile)
        {
            profile.CreateMap<Review, ReviewDto>()
                   .ForMember(d => d.UserName, map => map.MapFrom(src => src.User.UserName));
        }
    }
}
