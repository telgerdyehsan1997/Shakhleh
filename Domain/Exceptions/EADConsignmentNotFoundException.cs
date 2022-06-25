using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class EADConsignmentNotFoundException : ValidationException
    {
        public EADConsignmentNotFoundException(string message)
            : base(message)
        {

        }
    }
}
