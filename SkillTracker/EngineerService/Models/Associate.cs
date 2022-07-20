using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace EngineerService.Models
{
    public class Associate
    {
        private const string AssociateIdFormatErrorMessage = "Associate Id should start with CTS";
        private const string AssociateNameLengthErrorMessage = "Name length should be between 5 and 30";
        private const string AssociateIdLengthErrorMessage = "Associate Id length should be between 5 and 30";
        private const string MobileNumberFormatErrorMessage = "Mobile number should have 10 numeric characters";
        private const string AssociateIdRegex = "^(CTS)([0-9]+)";

        [MaxLength(30, ErrorMessage = AssociateNameLengthErrorMessage)]
        [MinLength(5, ErrorMessage = AssociateNameLengthErrorMessage)]
        [Required(AllowEmptyStrings = false, ErrorMessage = AssociateNameLengthErrorMessage)]
        public string Name { get; set; }

        [MaxLength(30, ErrorMessage = AssociateIdLengthErrorMessage)]
        [MinLength(5, ErrorMessage = AssociateIdLengthErrorMessage)]
        [RegularExpression(AssociateIdRegex, ErrorMessage = AssociateIdFormatErrorMessage)]
        [Required(AllowEmptyStrings = false, ErrorMessage = AssociateIdLengthErrorMessage)]
        public string AssociateId { get; set; }

        [Email]
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = MobileNumberFormatErrorMessage)]
        public string Mobile { get; set; }
    }
}
