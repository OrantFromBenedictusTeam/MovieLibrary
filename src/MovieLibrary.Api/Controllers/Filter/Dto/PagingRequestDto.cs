namespace MovieLibrary.Api.Controllers.Filter.Dto
{
    public class PagingRequestDto
    {
        public int? PageNumber { get; set; }
        public int? ItemsPerPage { get; set; }
    }
}
