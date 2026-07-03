namespace Library.Domain.QueryParameters
{
    public class BookQueryParametersDto
    {
        // Search
        public string? SearchTerm { get; set; }

        // Filters
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int? PublicationYear { get; set; }

        // Sorting
        public string? SortBy { get; set; } = "title";
        public bool Descending { get; set; } = false;

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}