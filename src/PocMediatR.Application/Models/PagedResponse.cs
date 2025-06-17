using System.Text.Json.Serialization;

namespace PocMediatR.Application.Models
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; } = [];
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (TotalItems + Size - 1) / Size;
        [JsonIgnore]
        public int Size { get; set; }
    }
}
