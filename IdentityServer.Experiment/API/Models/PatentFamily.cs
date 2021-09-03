using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Entities.CustomFields
{
    public class PatentFamily : BaseEntity<int>, IValidatableObject
    {
        public string DefaultRepresentativeDocumentNumber { get; set; }

        public DateTime? FirstDateFiling { get; set; }

        public DateTime? FirstDatePublication { get; set; }

        public string TitleEN { get; set; }

        public string AbstractEN { get; set; }


        public PatentDocument MatchedBy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Id <= 0)
            {
                results.Add(new ValidationResult("ID must be greater than 0"));
            }

            return results;
        }
    }
}
