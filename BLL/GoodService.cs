using AutoMapper;
using BLL.Utils;
using Contracts.Business;
using Contracts.Repositories;
using DTO;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BLL
{
    public class GoodService : IGoodsService
    {
        private IGoodsRepository _repository;
        private IMapper _mapper;
        //private Bitmap image;

        public GoodService(IGoodsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<GoodDTO> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<GoodDTO> SearchByName(string name)
        {
            var goods = _repository.GetAll();
            return goods.Where(e => e.Name.Contains(name));
        }
        public GoodDTO GoodByID(int id)
        {
            var good = _repository.GetById(id);
            return good;
        }

        public void GenerateThumbnail(string thumbPath, int thumbWidth, int thumbHeight, string thumbNewPath)
        {
            using (MagickImage image = new MagickImage(thumbPath))
            {
                image.Format = image.Format; // Get or Set the format of the image.
                image.Resize(thumbWidth, thumbHeight); // fit the image into the requested width and height. 
                //image.Quality = 10; // This is the Compression level.
                image.Write(thumbNewPath);
            }
         
        }

        public void CreateGood(GoodDTO createGoodDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                string fileName, filePath;
                SaveProductImage(formFile, out fileName, out filePath);
                createGoodDto.Photo = fileName;

                //Thumbnails
                var fileThumbnailsFolder = ContentPathBuilder.GetGoodThumbnailsImageFolderPath();
                var fileThumbnailsPath = Path.Combine(fileThumbnailsFolder, fileName);
                int thumbWidth = 100;
                int thumbHeight = 100;
                GenerateThumbnail(filePath, thumbWidth, thumbHeight, fileThumbnailsPath);
            }
            _repository.Create(createGoodDto);
        }

        private static void SaveProductImage(IFormFile formFile, out string fileName, out string filePath)
        {
            var fileFolder = ContentPathBuilder.GetGoodImageFolderPath();
            fileName = Guid.NewGuid().ToString();
            string extension = System.IO.Path.GetExtension(formFile.FileName);
            fileName = $"{fileName}{extension}";
            filePath = Path.Combine(fileFolder, fileName);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyToAsync(fileStream).Wait();
            }
        }

        public void EditGood(GoodDTO editGoodDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                var fileThumbnailsFolder = ContentPathBuilder.GetGoodThumbnailsImageFolderPath();
                var fileFolder = ContentPathBuilder.GetGoodImageFolderPath();
                if (!string.IsNullOrWhiteSpace(editGoodDto.Photo))
                {
                    var fileDelete = editGoodDto.Photo;
                    var fileThumbnailsDelete = Path.Combine(fileThumbnailsFolder, fileDelete);
                    fileDelete = Path.Combine(fileFolder, fileDelete);
                    if (File.Exists(fileDelete))
                        File.Delete(fileDelete);

                    //Thumbnails                
                    if (File.Exists(fileThumbnailsDelete))
                        File.Delete(fileThumbnailsDelete);
                }

                string fileName, filePath;
                SaveProductImage(formFile, out fileName, out filePath);
                editGoodDto.Photo = fileName;

                //Thumbnails                
                var NewfileThumbnailsPath = Path.Combine(fileThumbnailsFolder, fileName);
                int thumbWidth = 100;
                int thumbHeight = 100;
                GenerateThumbnail(filePath, thumbWidth, thumbHeight, NewfileThumbnailsPath);
            }

            _repository.Edit(editGoodDto);
        }

        public void DeleteGood(int id)
        {
            var good = GoodByID(id);
            var fileDelete = good.Photo;
            var fileFolder = ContentPathBuilder.GetGoodImageFolderPath();
            var fileThumbnailsFolder = ContentPathBuilder.GetGoodThumbnailsImageFolderPath();
            if (!string.IsNullOrWhiteSpace(good.Photo))
            {
                var fileThumbnailsDelete = Path.Combine(fileThumbnailsFolder, fileDelete);
                fileDelete = Path.Combine(fileFolder, fileDelete);
                if (File.Exists(fileDelete))
                    File.Delete(fileDelete);
                //Thumbnails                
                if (File.Exists(fileThumbnailsDelete))
                    File.Delete(fileThumbnailsDelete);
            }
            _repository.Delete(id);
        }
    }
}
