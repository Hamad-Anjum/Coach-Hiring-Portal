using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using MatBlazor;

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace CHS.Services.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async ValueTask<string> UploadAsync(IMatFileUploadEntry file, string path)
        {
            try
            {
                using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                //var ms = new MemoryStream();
                await file.WriteToStreamAsync(stream);
                //ms.WriteTo(stream);
                stream.Close();
                //ms.Close();

                string fileExtension = Path.GetExtension(path);
                string newFile = Path.Combine(GetFolderPath(path), Guid.NewGuid().ToString() + fileExtension);
                File.Move(path, newFile);
                Image image = await Image.LoadAsync(newFile);

                image.Mutate(x => x.Resize(196, 196));

                await image.SaveAsJpegAsync(newFile);
                return newFile;
            }
            catch (Exception)
            {
                path = GetFolderPath(path);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                return null;
            }
        }

        public MultipartFormDataContent MultipartFormDataContent(IBrowserFile file, string type)
        {
            var content = new MultipartFormDataContent();
            using (var ms = file.OpenReadStream(file.Size))
            {
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(file.Size)), "image", file.Name);
            }
            return content;
        }

        public async ValueTask<string> UploadAsync(IBrowserFile file, string path)
        {
            try
            {
                using FileStream stream = new(path, FileMode.Create, FileAccess.Write);
                var ms = new MemoryStream();
                await file.OpenReadStream(9012600).CopyToAsync(ms);

                ms.WriteTo(stream);
                stream.Close();
                ms.Close();

                string fileExtension = Path.GetExtension(path);
                string newFile = Path.Combine(GetFolderPath(path), Guid.NewGuid().ToString() + fileExtension);
                File.Move(path, newFile);
                Image image = await Image.LoadAsync(newFile);

                if (path.Contains("Posts") && file.Size > 9012600)
                {
                    image.Mutate(x => x.Resize(1900, 1440));
                }
                else if (path.Contains("CoverPictures"))
                {
                    image.Mutate(x => x.Resize(1600, 451));
                }
                else if (path.Contains("ProfilePictures"))
                {
                    image.Mutate(x => x.Resize(196, 196));
                }


                await image.SaveAsJpegAsync(newFile);
                return newFile;
            }
            catch (Exception)
            {
                path = GetFolderPath(path);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                return null;
            }
        }

        public async ValueTask<string> SaveLogoImage(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            if (path.Contains("ProfilePictures"))
            {
                bool dirExist = true;
                string newPath = path.Replace("ProfilePictures", "Logos");
                if (!Directory.Exists(Path.GetDirectoryName(newPath)))
                {
                    dirExist = Directory.CreateDirectory(Path.GetDirectoryName(newPath)).Exists;
                }

                if (dirExist)
                {
                    File.Copy(path, newPath);
                    Image image = await Image.LoadAsync(newPath);
                    image.Mutate(x => x.Resize(60, 60));
                    await image.SaveAsJpegAsync(newPath);
                    return newPath.Split(Constant.WwwRoot)[1].Replace('\\', '/');
                }
            }
            return null;
        }

        public async ValueTask<bool> SaveMainSiteImage(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            if (path.Contains("ProfilePictures"))
            {
                bool dirExist = true;
                string newPath = path.Replace("ProfilePictures", "MainSiteImage");
                if (!Directory.Exists(Path.GetDirectoryName(newPath)))
                {
                    dirExist = Directory.CreateDirectory(Path.GetDirectoryName(newPath)).Exists;
                }

                if (dirExist)
                {
                    File.Copy(path, newPath);
                    Image image = await Image.LoadAsync(newPath);
                    image.Mutate(x => x.Resize(350, 60));
                    await image.SaveAsJpegAsync(newPath);
                    return true;
                }
            }
            return false;
        }

        public async ValueTask<string> UploadPostAsync(IBrowserFile file, string path)
        {
            try
            {
                if (file.Size > 4194304)
                {
                    file = await file.RequestImageFileAsync("image/*", 1200, 1200);
                }
                using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                var ms = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(ms);

                ms.WriteTo(stream);
                return path;
            }
            catch (Exception)
            {
                path = Path.GetFullPath(path);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                return null;
            }
        }

        public bool DeleteImage(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            else if (File.Exists(path))
            {
                File.Delete(path);
                path = GetFolderPath(path);
                Directory.Delete(path.TrimEnd('\\'), true);

                return true;
            }
            return false;
        }

        private static string GetFolderPath(string path)
        {
            var temp = path.Split("\\");
            path = string.Empty;
            for (int i = 0; i < temp.Length - 1; i++)
            {
                path += temp[i] + "\\";
            }

            return path;
        }
    }
}
