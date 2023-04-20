using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base()
        {

        }

        public BadRequestException(string message) : base(message)
        {

        }

        public BadRequestException(string message, Exception exp) : base(message, exp)
        {

        }
    }
}
