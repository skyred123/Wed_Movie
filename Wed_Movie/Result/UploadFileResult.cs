namespace Wed_Movie.Result
{
    public class UploadFileResult
    {
        public bool IsSuccess { get; set; }

        public string? fileName { get; set; }
        public string? filePath { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ThumbFilePath { get; set; }
    }
}
