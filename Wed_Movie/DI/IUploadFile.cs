using Microsoft.AspNetCore.Mvc;
using Wed_Movie.Result;

namespace Wed_Movie.DI
{
    public interface IUploadFile
    {
        Task<UploadFileResult> UploadsAsync(IFormFile? file, bool thumbnail);

        FileStream StreamMovie(string filePath);
        byte[] GetFile(string filename);

        bool DeleteFile(string? filePath);

        bool CheckFileExists(string? filePath);
    }
}
