using Application.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Entities.CustomFields
{
    public class Tag : BaseEntity<long>, IValidatableObject
    {
        [Required]
        public string TagName { get; set; }
        public ICollection<PatentFamily> PatentFamilies { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Id <= 0)
            {
                results.Add(new ValidationResult("ID must be greater than 0"));
            }

            if (string.IsNullOrWhiteSpace(TagName))
            {
                results.Add(new ValidationResult("Name must be set"));
            }

            return results;
        }
    }
}
