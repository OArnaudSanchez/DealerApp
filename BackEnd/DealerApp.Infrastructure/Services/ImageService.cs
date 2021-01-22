using System.IO;
using System.Threading.Tasks;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using DealerApp.Core.Enumerations;
using System;
using DealerApp.Infrastructure.Helpers;
using System.Collections.Generic;

namespace DealerApp.Infrastructure.Services
{
    public class ImageService : IHelperImage
    {
        public async Task<string> Upload(List<IFormFile> file, string directory, string folder)
        {
            if (file.Count == 0 || file == null)
            {
                throw new BussinessException("No se ha seleccionado ningun archivo", 400);
            }

            if (CheckImageFile(file))
            {
                return await WriteFile(file, directory, folder);
            }

            throw new BussinessException("La Foto no Tiene un Formato Valido", 400);
        }

        private bool CheckImageFile(List<IFormFile> file)
        {
            foreach (var image in file)
            {
                byte[] fileBytes;
                var memoryStream = new MemoryStream();
                image.CopyTo(memoryStream);
                fileBytes = memoryStream.ToArray();
                return ImageHelper.GetImageFormat(fileBytes) != ImageFormat.unkown;
            }

            return false;
        }

        public async Task<string> WriteFile(List<IFormFile> file, string directory, string folder)
        {
            string fileName = "";
            try
            {
                foreach (var image in file)
                {
                    var extension = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
                    fileName = Guid.NewGuid().ToString() + extension;

                    var path = Path.Combine(directory, $"Resources\\Images\\{folder}", fileName);

                    var bits = new FileStream(path, FileMode.Create);

                    await image.CopyToAsync(bits);
                    bits.Close();
                }

            }
            catch (BussinessException)
            {
                throw new BussinessException("Ha ocurrido un error al subir la foto", 500);
            }

            return fileName;
        }
        public void DeleteImage(string fileName, string folder, string directory)
        {
            var imagePath = Path.Combine(directory, $"Resources\\Images\\{folder}", fileName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}