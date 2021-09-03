using System;
using System.Collections.Generic;
using Application.Domain.Entities.CustomFields;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomFieldsController : ControllerBase
    {
        public CustomFieldsController()
		{
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new List<CustomField> { 
                new CustomField
				{
                    Id = 1,
                    Description = "dummy custom field 1",
                    Name = "dummy cf 1",
                    LinkedValues = new List<CustomFieldValue>
					{
                        new CustomFieldValue
						{
                            Id = 1,
                            CustomFieldID = 1,
                            NumericValue = 123.456,
                            TextValue = "123.456"
						}
					},
                    PatentFamilies = new List<PatentFamily>
					{
                        new PatentFamily
						{
                            Id = 2,
                            TitleEN = "patent family 1",
                            AbstractEN = "some abstract here",
                            FirstDateFiling = DateTime.UtcNow,
                            FirstDatePublication = DateTime.UtcNow.AddYears(1)
						}
					}
				}
            });
        }
    }
}
