using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DealerApp.Core.Interfaces
{
    public interface IHelperImage
    {
        Task<string> Upload(List<IFormFile> file, string directory, string folder);
        void DeleteImage(string fileName, string folder, string directory);
    }
}