using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ExpenditureOne.Web.Models.Expenditure
{
    public class ExpenditureEditRequest
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "dateOfExpenditure")]
        public DateTime? DateOfExpenditure { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "categories")]
        public ICollection<DAL.Entities.Category> Categories { get; set; }
    }
}
