using AutoMapper;
using MovieLibrary.Api.Common;

namespace MovieLibrary.Api.Controllers.Movie
{
    public class MovieManagementController : CrudApiController<Data.Entities.Movie, Dto.CreateMovieDto, Dto.UpdateMovieDto, Dto.GetMovieDto>
    {
        public MovieManagementController(IRepository<Data.Entities.Movie> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
