using MyBookAPI.Application.Common.Interfaces;
using System;

namespace MyBookAPI.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
