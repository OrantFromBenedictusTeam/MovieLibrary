using AutoMapper;
using MovieLibrary.Api.Common;

namespace MovieLibrary.Api.Controllers.Category
{
    public class CategoryManagementController : CrudApiController<Data.Entities.Category, Dto.CreateCategoryDto, Dto.UpdateCategoryDto, Dto.GetCategoryDto>
    {
        public CategoryManagementController(IRepository<Data.Entities.Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
