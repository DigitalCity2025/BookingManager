using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManager.Application.Exceptions
{
    public class DuplicateFieldException(string fieldName, string errorMessage): Exception(errorMessage)
    {
        public string FieldName { get; set; } = fieldName!;

    }
}
