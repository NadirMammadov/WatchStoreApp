using WatchStore.Shared.Dtos;

namespace WatchStoreApp.UI.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var TResponse = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
            return TResponse.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return null;
            }
            // örnek dosya ismi= 203802340234.jpg
            var randonFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            using var ms = new MemoryStream();

            await photo.CopyToAsync(ms);

            var multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randonFilename);

            var TResponse = await _httpClient.PostAsync("photos", multipartContent);

            if (!TResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var TResponseSuccess = await TResponse.Content.ReadFromJsonAsync<TResponse<PhotoViewModel>>();

            return TResponseSuccess.Data;
        }
    }
}
