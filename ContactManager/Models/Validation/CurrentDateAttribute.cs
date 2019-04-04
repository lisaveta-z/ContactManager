using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models.Validation
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute() : base() 
        {
        }

        public override bool IsValid(object value)
        {
            return (DateTime)value <= DateTime.Now;
        }
    }
}
