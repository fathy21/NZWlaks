using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostenvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHostenvironment , IHttpContextAccessor httpContextAccessor ,
            NZWalksDbContext dbContext)
        {
            this.webHostenvironment = webHostenvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> UploadImage(Image image)
        {
            
            var imagesDirectory = Path.Combine(webHostenvironment.ContentRootPath, "Images");

            // Ensure the directory exists.
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            // Create a unique file name
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.File.FileName);
            var localFilePath = Path.Combine(imagesDirectory, uniqueFileName);

            using (var stream = new FileStream(localFilePath, FileMode.Create))
            {
                await image.File.CopyToAsync(stream);
            }
           
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{uniqueFileName}";
           
            image.FilePath = urlFilePath;

            await dbContext.images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
