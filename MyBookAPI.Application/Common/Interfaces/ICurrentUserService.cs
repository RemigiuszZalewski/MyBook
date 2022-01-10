using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string Email { get; set; }
        public bool Authenticated { get; set; }
    }
}
