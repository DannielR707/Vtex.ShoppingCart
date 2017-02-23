using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betsson.Wallet.Service.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message)
            : base(message)
        { }

        public ResourceNotFoundException()
            : base("The requested resource was not found.")
        { }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        { }

    }
}