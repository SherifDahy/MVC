namespace Services
{
    public class ImageService
    {
        public async Task<string> UploadFile(IFormFile ufile)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(ufile.FileName)}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return fileName;
            }
            return null;
        }
    }
}
