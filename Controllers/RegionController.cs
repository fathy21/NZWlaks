using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomeActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRepositoryRegion _repositoryRegion;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RegionController(IRepositoryRegion repositoryRegion , IMapper mapper , ILogger<RegionController> logger)
        {
            _repositoryRegion = repositoryRegion;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
                var regionsdomain = await _repositoryRegion.GetAllAsync();
                //mapping model into DTO
                _logger.LogInformation($"finished getallregions request with data :{JsonSerializer.Serialize(regionsdomain)}");
                return Ok(_mapper.Map<List<Region>>(regionsdomain));
        }

        
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regiondomain = await _repositoryRegion.GetById(id);
            if (regiondomain is null)
            {
                return NotFound();
            }
            var res = _mapper.Map<RegionDTO>(regiondomain);
            return Ok(res);
        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create(AddRegionRequestDto addRegionRequestDto)
        {

           
                // mapping DTO to domain model 
                var regiondomainmodel = _mapper.Map<Region>(addRegionRequestDto);

                //use domain model to create region
                regiondomainmodel = await _repositoryRegion.Create(regiondomainmodel);

                //mapping domain model back to DTO
                var regiondto = _mapper.Map<RegionDTO>(regiondomainmodel);

                return CreatedAtAction(nameof(GetById), new { id = regiondto.Id }, regiondto);
           
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute]Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
               var regiondomainmodel = _mapper.Map<Region>(updateRegionRequestDto);

                regiondomainmodel = await _repositoryRegion.Update(id, regiondomainmodel);
                if (regiondomainmodel == null)
                {
                    return NotFound();
                }

                //convert domainmodel to DTO
                var regiondto = _mapper.Map<RegionDTO>(regiondomainmodel);

                return Ok(regiondto);
           
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id )
        {


            var regiondomainmodel = await _repositoryRegion.Delete(id);
            if (regiondomainmodel == null)
            {
                return NotFound();
            }
            var regiondto =_mapper.Map<RegionDTO>(regiondomainmodel);   
            return Ok(regiondto);
        }
    }
}
