using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using NZWalks.API.CustomeActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WlakController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WlakController(IMapper mapper , IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequest addWalkRequest)
        {
            
                //mapping from Dto to domain model 
                var walkdomainmodel = _mapper.Map<Walk>(addWalkRequest);

                await _walkRepository.Create(walkdomainmodel);

                // mapping from domain model to dto
                var walkdto = _mapper.Map<WalkDTO>(walkdomainmodel);

                return Ok(walkdto);
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filteroncolumn, [FromQuery] string? filteronword,
           [FromQuery] string? sortBy , [FromQuery] bool? isAscending ,
           [FromQuery] int pageNumber = 1 , int pageSize = 1000)
        {
            //throw new Exception("exception has occurred!!");

            //filter by word in column 
            var walksdomainmodel = await _walkRepository.GetAll(filteroncolumn, filteronword , sortBy , isAscending ?? true , pageNumber , pageSize);

            //mapping domainmodel to dto
            var walkdto = _mapper.Map<List<WalkDTO>>(walksdomainmodel);

            return Ok(walkdto);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkdomainmodel = await _walkRepository.GetById(id);    
            if(walkdomainmodel == null)
            {
                return NotFound();
            }
            var walkdto = _mapper.Map<WalkDTO>(walkdomainmodel);
            return Ok(walkdto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id , UpdateWalkRequestDto updateWalkRequestDto)
        {
           
                // mapping dto to domain model
                var walkdomainmodel = _mapper.Map<Walk>(updateWalkRequestDto);

                walkdomainmodel = await _walkRepository.Upate(id, walkdomainmodel);
                if (walkdomainmodel == null)
                {
                    return NotFound();
                }
                // mapping domain model to DTO
                var walkdto = _mapper.Map<WalkDTO>(walkdomainmodel);
                return Ok(walkdto);
           
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete (Guid id)
        {
            var walkdomainmodel = await _walkRepository.Delete(id);
            if( walkdomainmodel == null)
            {
                return NotFound();
            }
            var walkdto = _mapper.Map<WalkDTO>(walkdomainmodel);
            return Ok(walkdto);
        }
    }
}
