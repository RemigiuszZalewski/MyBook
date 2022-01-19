using AgileObjects.ReadableExpressions;
using AutoMapper;
using Moq;
using MyBookAPI.Application.Common.Mappings;
using MyBookAPI.Application.Reviews.Queries.GetReviews;
using MyBookAPI.Domain.Entities;
using MyBookAPI.Persistance;
using System;

namespace MyBookAPI.Application.UnitTests.Common
{
    public class QueryBase : IDisposable
    {
        protected readonly MyBookDbContext _dbContext;
        protected readonly Mock<MyBookDbContext> _dbContextMock;
        protected readonly IMapper _mapper;

        public QueryBase()
        {
            _dbContextMock = MyBookDbContextFactory.Create();
            _dbContext = _dbContextMock.Object;

            var configurationProvider = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            MyBookDbContextFactory.DisposeDatabase(_dbContext);
        }
    }
}
