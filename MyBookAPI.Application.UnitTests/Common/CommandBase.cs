using Moq;
using MyBookAPI.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookAPI.Application.UnitTests.Common
{
    public class CommandBase : IDisposable
    {
        protected readonly MyBookDbContext _dbContext;
        protected readonly Mock<MyBookDbContext> _dbContextMock;
        public CommandBase()
        {
            _dbContextMock = MyBookDbContextFactory.Create();
            _dbContext = _dbContextMock.Object;
        }
        public void Dispose()
        {
            MyBookDbContextFactory.DisposeDatabase(_dbContext);
        }
    }
}
