using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Mapping
{
    public class MappingTest : IClassFixture<MappingFixture>
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;
        public MappingTest(MappingFixture mappingFixture)
        {
            _configurationProvider = mappingFixture.ConfigurationProvider;
            _mapper = mappingFixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }
    }
}
