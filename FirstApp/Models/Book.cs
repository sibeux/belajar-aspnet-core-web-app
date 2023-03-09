using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Models
{
    public class Book
    {
        [FromRoute]
        public int? BookId { get; set; }
        public string? Author { get; set; }

        public override string ToString()
        {
            return $"book object - bookid : {BookId}, Author : {Author}";
        }
    }
}
