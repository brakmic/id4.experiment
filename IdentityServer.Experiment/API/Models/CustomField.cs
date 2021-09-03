using Application.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Entities.CustomFields
{
    /// <summary>
    /// Represents a Custom Field that can contain arrays of Patent Families, Documents, or Publications.
    /// At least one of them must contain one or more entries for Custom Field to be accepted as valid.
    /// </summary>
    public class CustomField : BaseEntity<long>, IValidatableObject
    {
        public IList<CustomFieldValue> LinkedValues { get; set; }

        public IList<PatentFamily> PatentFamilies { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (Id == 0)
            {
                results.Add(new ValidationResult("ID cannot be 0"));
            }
            return results;
        }
    }
}