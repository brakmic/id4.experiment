using Application.Core.Entities;
using System;

namespace Application.Domain.Entities.CustomFields
{
    public class PatentDocument : BaseEntity<long>
    {
        public string DocumentNumber { get; set; }

        public DateTime? DateFiling { get; set; }

        public DateTime? DatePublication { get; set; }
    }
}
