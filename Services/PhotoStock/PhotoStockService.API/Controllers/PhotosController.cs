using Microsoft.AspNetCore.Mvc;
using PhotoStockService.API.Dtos;
using WatchStore.Shared.ControllerBase;
using WatchStore.Shared.Dtos;

namespace PhotoStockService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);
                var returnPath = photo.FileName;
                PhotoDto photoDto = new() { Url = returnPath };
                return CreateActionResultInstance(TResponse<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(TResponse<NoContent>.Fail("photo is empty", 400));
        }

        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(TResponse<NoContent>.Fail("photo not found", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(TResponse<PhotoDto>.Success(204));
        }
    }
}
