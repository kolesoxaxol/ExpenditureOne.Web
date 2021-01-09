using Newtonsoft.Json;

namespace ExpenditureOne.Web.Models.Category
{
    public class CategoryCreateRequest
    {
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
    }
}
