using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;
using Wed_Movie.DI;
using Wed_Movie.Result;
using System.Diagnostics;
using FFMpegCore;
using System.Text.RegularExpressions;
using System.Drawing;
using NuGet.Packaging.Signing;

namespace Wed_Movie.Functions
{
    public class Upload : IUploadFile
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IServiceProvider _serviceProvider;
        //private readonly AppSettings _appSettings;
        private readonly List<string> imageExtensions = new List<string> { "jpg", "jpeg", "png", "gif", "bmp", "tiff", "tif", "webp", "svg", "ico" };
        private readonly List<string> videoExtensions = new List<string> { "mp4", "avi", "mov", "mkv", "wmv", "flv", "webm", "3gp" };
        private readonly string _pathImage = "Uploads\\Images\\";
        //private readonly string _pathVideo = "Uploads\\Videos\\";

        public Upload(IWebHostEnvironment hostingEnvironment, IServiceProvider serviceProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _serviceProvider = serviceProvider;
            //_appSettings = appSettings;
        }
        private readonly string _pathVideo = "Uploads\\Videos\\";
        public async Task<UploadFileResult> UploadsAsync(IFormFile file,bool thumbnail) {
            if (file != null && file.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = "";
                string relativePath = "";
                string thumbFilePath = "";
                string isExtensions = IsAllowedExtensions(uniqueFileName);
                if (isExtensions.Equals("Unknown") == true)
                {
                    return new UploadFileResult { IsSuccess = false, ErrorMessage = "Tải file không thành công" };
                }
                else if (isExtensions.Equals("Image") == true)
                {
                    using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
                    {
                        int maxWidth = 2080;
                        int maxHeight = 2080;
                        image.Mutate(x => x.Resize(new ResizeOptions { Size = new SixLabors.ImageSharp.Size(maxWidth, maxHeight), Mode = ResizeMode.Max }));
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, _pathImage);
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var outputStream = new FileStream(filePath, FileMode.Create))
                        {
                            image.Save(outputStream, encoder: new JpegEncoder { Quality = 100 });
                            await outputStream.FlushAsync();
                        }
                    }
                    relativePath = Path.Combine(_pathImage, uniqueFileName);
                }
                else if (isExtensions.Equals("Video") == true)
                {
                    /*// Tạo đường dẫn đầy đủ tới thư mục lưu trữ
                    string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _pathVideo);
                    // Tạo thư mục lưu trữ nếu không tồn tại
                    if (!Directory.Exists(uploadsPath))
                    {
                        Directory.CreateDirectory(uploadsPath);
                    }

                    // Tạo đường dẫn tới file video
                    filePath = Path.Combine(uploadsPath, uniqueFileName);
                    // Lưu file video
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Chuyển đổi định dạng video nếu cần
                    if (!Path.GetExtension(uniqueFileName).Equals(".mp4", StringComparison.OrdinalIgnoreCase))
                    {
                        // Đường dẫn lưu trữ file sau khi chuyển đổi
                        string convertedFilePath = Path.ChangeExtension(filePath, ".mp4");

                        // Khởi tạo đối tượng FFmpegConverter
                        var ffmpeg = new FFMpegConverter();

                        // Cấu hình video settings
                        var videoSettings = new ConvertSettings();
                        videoSettings.VideoCodec = "libx264"; // Sử dụng codec libx264 để nén video
                        videoSettings.CustomOutputArgs = "-crf 23"; // Đặt mức nén (Constant Rate Factor) là 23

                        // Chuyển đổi định dạng video sang MP4
                        ffmpeg.ConvertMedia(filePath, convertedFilePath, NReco.VideoConverter.Format.mp4,null,videoSettings);

                        // Xóa file gốc nếu muốn
                        File.Delete(filePath);
                    }*/

                    var guid = Guid.NewGuid();
                    var videoPath = "video_" + guid;

                    var partialPath = Guid.NewGuid().ToString().Replace("-", "");
                    var videoFname = videoPath + partialPath + Path.GetExtension(file.FileName);


                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, _pathVideo);
                    var videoFilePath = Path.Combine(uploadsFolder, videoFname);

                    using (Stream fileStream = new FileStream(videoFilePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    };
                    

                    //var newVideoFileName = ConvertToMP4(videoFilePath);
                    //DeleteFile(Path.Combine(_pathVideo, videoFname));

                    if (thumbnail == true)
                    {
                        var thumbPath = "thumb_" + guid;
                        var thumbFname = thumbPath + partialPath + ".BMP";
                        var uploadsThumbFolder = Path.Combine(_hostingEnvironment.WebRootPath, _pathImage);
                        thumbFilePath = Path.Combine(uploadsThumbFolder, thumbFname);

                        var length = await GetVideoLength(videoFilePath);
                        var heightWidthStr = await GetVideoHeightWidth(videoFilePath);
                        var heightWidth = heightWidthStr.Split('x');

                        string width = heightWidth[0];
                        string height = heightWidth[1];

                        //get rounded ints for use in ratio (for thumbnail sizes only):
                        int nWidth = Convert.ToInt32(width);
                        int nHeight = Convert.ToInt32(height);
                        int ratioHeight = 0;
                        int ratioWidth = 0;
                        ApplyRatio(nWidth, nHeight, ref ratioWidth, ref ratioHeight);

                        //get thumbnail:
                        var snapshotResult = await FFMpeg
                        .SnapshotAsync(
                            videoFilePath,
                            new System.Drawing.Size(ratioWidth, ratioHeight),
                            null//TimeSpan.FromMinutes(1) //apply this if we want the thumb from a later point in time in the vid
                            );

                        using MemoryStream thumbMemStream = new MemoryStream();
                        snapshotResult.Save(thumbMemStream, System.Drawing.Imaging.ImageFormat.Bmp); //Install nuget: System.Drawing.Common v6
                        thumbMemStream.Position = 0;

                        using Stream thumbFileStream = new FileStream(thumbFilePath, FileMode.Create);
                        await thumbMemStream.CopyToAsync(thumbFileStream);

                        thumbFilePath = Path.Combine(_pathImage, thumbFname);
                    }
                    
                    uniqueFileName = videoFname;
                    relativePath = Path.Combine(_pathVideo, uniqueFileName);
                }
                return new UploadFileResult { IsSuccess = true, fileName = uniqueFileName, filePath = relativePath, ThumbFilePath = thumbFilePath};
            }
            return new UploadFileResult { IsSuccess = false, ErrorMessage = "Tải file không thành công" };
        }

        [HttpPost]
        public byte[] GetFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }

            byte[] imageData;
            string wwwrootPath = _hostingEnvironment.WebRootPath;
            string fullPath = Path.Combine(wwwrootPath, filename);

            // Đọc dữ liệu hình ảnh từ tệp tin
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                imageData = new byte[stream.Length];
                stream.Read(imageData, 0, (int)stream.Length);
            }

            // Tăng độ phân giải của hình ảnh
            using (var image = SixLabors.ImageSharp.Image.Load(imageData))
            {
                int targetWidth = 1280; // Độ rộng mục tiêu sau khi scale
                int targetHeight = 1280; // Độ cao mục tiêu sau khi scale

                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new SixLabors.ImageSharp.Size(targetWidth, targetHeight),
                    Mode = ResizeMode.Max
                }));

                // Tạo một MemoryStream để lưu trữ hình ảnh đã tăng độ phân giải
                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, new JpegEncoder { Quality = 80 });

                    // Trả về kết quả
                    return outputStream.ToArray();
                }
            }

            // Xử lý không thành công
            return null;
        }
        public bool DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) { return false; }
            string wwwrootPath = _hostingEnvironment.WebRootPath;
            string fullPath = Path.Combine(wwwrootPath, filePath);

            try
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                else
                {
                    // File không tồn tại
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public FileStream StreamMovie(string filePath)
        {
            var file = Path.Combine(_hostingEnvironment.WebRootPath, filePath);
            var stream = System.IO.File.OpenRead(file);

            return stream;
        }
        public string IsAllowedExtensions(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(fileExtension))
            {
                return "Unknown";
            }
            string normalizedExtension = fileExtension.TrimStart('.').ToLower();

            if (imageExtensions.Contains(normalizedExtension))
            {
                return "Image";
            }
            else if (videoExtensions.Contains(normalizedExtension))
            {
                return "Video";
            }
            else
            {
                return "Unknown";
            }
        }


        #region private methods

        private string ConvertToMP4(string inputPath)
        {
            //(first, install FFMpegCore nuget package)
            var videoPath = "video_" + Guid.NewGuid();
            var partialPath = Guid.NewGuid().ToString().Replace("-", "");

            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, _pathVideo);
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string newFileName = videoPath + partialPath + Path.GetExtension(fileName); // Đổi tên file ở đây
            string outputPath = Path.Combine(uploadsFolder, Path.ChangeExtension(newFileName, ".mp4"));

            FFMpegArguments
            .FromFileInput(inputPath)
            .OutputToFile(outputPath, false, options => options
                .WithVideoCodec(FFMpegCore.Enums.VideoCodec.LibX264)
                .ForceFormat("mp4")
                .WithConstantRateFactor(21)
                .WithAudioCodec(FFMpegCore.Enums.AudioCodec.Aac)
                .WithVariableBitrate(4)
                .WithVideoFilters(filterOptions => filterOptions
                    .Scale(FFMpegCore.Enums.VideoSize.FullHd))
                .WithFastStart())
            .ProcessSynchronously();
            return Path.ChangeExtension(newFileName, ".mp4");
        }


        private void ApplyRatio(int width, int height, ref int newWidth, ref int newHeight)
        {
            int maxWidth = 150;
            int maxHeight = 150;

            var ratioX = (float)maxWidth / width;
            var ratioY = (float)maxHeight / height;
            var ratio = Math.Min(ratioX, ratioY);

            newWidth = (int)Math.Round(width * ratio);
            newHeight = (int)Math.Round(height * ratio);
        }

        private async Task<string> GetVideoHeightWidth(string filePath)
        {
            string cmd = string.Format("-v error -select_streams v -show_entries stream=width,height -of csv=p=0:s=x {0}", filePath);
            Process theProcess = new Process();
            theProcess.StartInfo.FileName = "./ffmpeg/bin/ffprobe.exe";
            theProcess.StartInfo.Arguments = cmd;
            theProcess.StartInfo.CreateNoWindow = true;
            theProcess.StartInfo.RedirectStandardOutput = true;
            theProcess.StartInfo.RedirectStandardError = true;
            theProcess.StartInfo.UseShellExecute = false;
            theProcess.StartInfo.UseShellExecute = false;
            if (!theProcess.Start())
            {
                Debug.WriteLine("Could not start the FFMpeg process!");
                return "0x0"; //failed
            }
            string result = theProcess.StandardOutput.ReadToEnd();
            theProcess.WaitForExit();
            theProcess.Close();
            return result;
        }

        private async Task<TimeSpan> GetVideoLength(string filepath)
        {
            //make sure you have downloaded from ffmpeg site the binaries and put in this folder!

            string ffMPEG = "./ffmpeg/bin/ffMPEG.exe";
            string outPut = "";
            string param = string.Format("-i \"{0}\"", filepath);
            ProcessStartInfo processStartInfo = null;

            //add using 'System.Text.RegularExpressions' for regex commands!

            Regex regex = null;
            Match m = null;
            TimeSpan duration = TimeSpan.MinValue;

            //Get ready with ProcessStartInfo
            processStartInfo = new ProcessStartInfo(ffMPEG, param);
            processStartInfo.CreateNoWindow = true;

            //ffMPEG uses StandardError for its output.
            processStartInfo.RedirectStandardError = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.UseShellExecute = false;

            Process systemProcess = Process.Start(processStartInfo);
            if (systemProcess != null)
            {
                StreamReader streamReader = systemProcess.StandardError;

                outPut = streamReader.ReadToEnd();
                systemProcess.WaitForExit();
                systemProcess.Close();
                systemProcess.Dispose();
                streamReader.Close();
                streamReader.Dispose();

                //get duration

                regex = new Regex("[D|d]uration:.((\\d|:|\\.)*)");
                m = regex.Match(outPut);

                if (m.Success)
                {
                    string temp = m.Groups[1].Value;
                    string[] timepieces = temp.Split(new char[] { ':', '.' });
                    if (timepieces.Length == 4)
                    {
                        duration = new TimeSpan(0, Convert.ToInt16(timepieces[0]), Convert.ToInt16(timepieces[1]), Convert.ToInt16(timepieces[2]), Convert.ToInt16(timepieces[3]));
                    }
                }
                return duration;

            }
            return new TimeSpan(0);//failed to start process
        }

        #endregion
    }
}
