using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DealerApp.Core.Common
{
    public class ImageBase
    {
        public List<IFormFile> Image { get; set; }
        public string Foto { get; set; }
    }
}