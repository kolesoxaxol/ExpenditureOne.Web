using Newtonsoft.Json;

namespace ExpenditureOne.Web.Models.Category
{
    public class CategoryEditRequest
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
    }
}
