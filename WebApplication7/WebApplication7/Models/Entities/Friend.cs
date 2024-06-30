using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models.Entities
{
    public class Friend
    {
        [Required, Contains("a", ErrorMessage="Debe contener una 'a'")]
        public string Name { get; set; }

        [Range(18, 120)]
        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Address Address { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        
        [Required, StringLength(5, MinimumLength = 5)]
        public string ZipCode { get; set; }
    } 

    public class ContainsAttribute : ValidationAttribute
    {
        private readonly string _substring;

        public ContainsAttribute(string substring)
        {
            _substring = substring;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is string))
            {
                return new ValidationResult("El valor debe ser una cadena.");
            }

            var stringValue = value as string;
            if (!stringValue.Contains(_substring))
            {
                return new ValidationResult(ErrorMessage ?? $"El campo debe contener '{_substring}'.");
            }

            return ValidationResult.Success;
        }
    }
}
