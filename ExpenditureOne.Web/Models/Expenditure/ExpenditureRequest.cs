﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenditureOne.Web.Models.Expenditure
{
    public class ExpenditureRequest
    {
        //[JsonProperty(PropertyName = "id")]
        //public int Id { get; set; }

        [JsonProperty(PropertyName = "dateOfExpenditure")]
        public DateTime? DateOfExpenditure { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

       // public  ICollection<ExpenditureCategory> Categories { get; set; }
    }
}
