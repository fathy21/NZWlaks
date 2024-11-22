using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("UploadImage") ]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadFileRequestDto requestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };

            if (!allowedExtensions.Contains(Path.GetExtension(requestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "unsupported file extensions");
            }

            if (requestDto.File.Length > 10485760) // convert from 10mb to bytes
            {
                ModelState.AddModelError("file", "file size more than 10MB , pleasw upload a smaller size of image!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //convert dto to domain model
            var imageDomainModel = new Image
            {
                File = requestDto.File,
                FileExtension = Path.GetExtension(requestDto.File.FileName),
                FileSizeInBytes = requestDto.File.Length,
                FileName = requestDto.FileName,
                FileDescription = requestDto.FileDescription
            };

            await imageRepository.UploadImage(imageDomainModel);

            return Ok(imageDomainModel);
        }

        
    }
}
