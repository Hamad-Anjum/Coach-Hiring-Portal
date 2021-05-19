using System.Net.Http;
using System.Threading.Tasks;

using MatBlazor;

using Microsoft.AspNetCore.Components.Forms;

namespace CHS.Web.Services
{
    public interface IFileUpload
    {
        /// <summary>
        /// For Blazor Simple InputFile
        /// Use for Profile Picture
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        ValueTask<string> UploadAsync(IBrowserFile file, string path);

        /// <summary>
        /// For Blazor Simple InputFile
        /// Use for Post Upload
        /// </summary>
        /// <param name="file"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        ValueTask<string> UploadPostAsync(IBrowserFile file, string path);

        /// <summary>
        /// For Saving logo image on top right corner
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        ValueTask<string> SaveLogoImage(string path);

        /// <summary>
        /// For MatBlazor InputFile
        /// </summary>
        /// <param name="e"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        ValueTask<string> UploadAsync(IMatFileUploadEntry e, string type);

        /// <summary>
        /// For MultiPartForm,
        /// Returns MultipartFormDataContent for sending to API
        /// </summary>
        /// <param name="file"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        MultipartFormDataContent MultipartFormDataContent(IBrowserFile file, string type);

        /// <summary>
        /// Delete Selected Image from file System
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        bool DeleteImage(string img);
        ValueTask<bool> SaveMainSiteImage(string imgSrc);
    }
}
