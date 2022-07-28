using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NordKlan.Validation
{
    public class DurationEventValidation: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            TimeSpan dateTime = (TimeSpan)value;
            return dateTime >= new TimeSpan(0, 30, 0) && dateTime <= new TimeSpan(24, 0, 0);
        }
    }
}
