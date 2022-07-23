
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Common;
using MovieLibrary.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Api.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public abstract class CrudApiController<TEntity, TCreateDto, TUpdateDto, TGetDto> : ControllerBase where TEntity : Entity
    {
        protected IRepository<TEntity> Repository { get; }
        protected IMapper Mapper { get; }
        public CrudApiController(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public virtual IEnumerable<TGetDto> Get()
        {
            return Repository.GetAll().Select(item => Mapper.Map<TGetDto>(item));
        }

        [HttpGet("{id}")]
        public virtual TGetDto Get(int id) => Mapper.Map<TGetDto>(Repository.GetById(id));

        [HttpPost]
        public virtual int Create([FromBody] TCreateDto dto) => Repository.Insert(Mapper.Map<TEntity>(dto));

        [HttpDelete("{id}")]
        public virtual void Delete(int id) => Repository.Delete(id);

        [HttpPut]
        public virtual TGetDto Update([FromBody] TUpdateDto dto)
        {
            var updatingEntity = Mapper.Map<TEntity>(dto);
            Repository.Update(updatingEntity);
            return Mapper.Map<TGetDto>(Repository.GetById(updatingEntity.Id));
        }
    }
}
