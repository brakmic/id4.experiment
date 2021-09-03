using Application.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Entities.CustomFields
{
	public class CustomFieldValue : BaseEntity<int>, IValidatableObject
	{
        [Required]
        public long CustomFieldID { get; set; }
#nullable enable
        public string? TextValue { get; set; }
        public double? NumericValue { get; set; }
#nullable disable
        public int PatentFamilyID { get; set; }

        public PatentFamily PatentFamily { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (Id <= 0)
            {
                results.Add(new ValidationResult("ID must be greater than 0"));
            }
            if (CustomFieldID <= 0)
            {
                results.Add(new ValidationResult("CustomFieldID must be greater than 0"));
            }
            if (TextValue == null && NumericValue == null)
            {
                results.Add(new ValidationResult("Either TextValue or NumbericValue must be set"));
            }
            return results;
        }
    }
}
