using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContactManager.Models.Validation;

namespace ContactManager.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^(\S|\D)*$", ErrorMessage = "No white spaces or numbers allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^(\S|\D)*$", ErrorMessage = "No white spaces or numbers allowed")]
        public string Name { get; set; }

        [RegularExpression(@"^(\S|\D)*$", ErrorMessage = "No white spaces or numbers allowed")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [CurrentDate(ErrorMessage = "Date must not be greater than current")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Tag is required")]
        [StringLength(64, MinimumLength = 2, ErrorMessage = "Tag length must be between 2 and 64 characters")]
        public string Tag { get; set; }
    }
}
