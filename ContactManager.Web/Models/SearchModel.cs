namespace ContactManager.Web.Models
{
    public class Search
    {
        public string Value { get; set; }
    }

    public class SearchModel
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public Search Search { get; set; }
    }
}
