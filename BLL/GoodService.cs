using AutoMapper;
using BLL.Utils;
using Contracts.Business;
using Contracts.Repositories;
using DTO;
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
        private Bitmap image;

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
            image = new Bitmap(thumbPath);
            Image imageThumbnail = image.GetThumbnailImage(thumbWidth, thumbHeight, null, new IntPtr());
            imageThumbnail.Save(thumbNewPath);
        }

        public void CreateGood(GoodDTO createGoodDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                var fileFolder = ContentPathBuilder.GetGoodImageFolderPath();
                var fileName = Guid.NewGuid().ToString();// ECA34DB2-C885-4277-86DC-287BCEE00FAD
                string extension = System.IO.Path.GetExtension(formFile.FileName);
                fileName = $"{fileName}{extension}";
                var filePath = Path.Combine(fileFolder, fileName);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyToAsync(fileStream).Wait();
                }
                createGoodDto.Photo = fileName;

                //Thumbnails
                var fileThumbnailsFolder = ContentPathBuilder.GetGoodThumbNailsImageFolderPath();
                var fileThumbnailsPath = Path.Combine(fileThumbnailsFolder, fileName);
                int thumbWidth = 100;
                int thumbHeight = 100;
                GenerateThumbnail(filePath, thumbWidth, thumbHeight, fileThumbnailsPath);
            }
            _repository.Create(createGoodDto);
        }

        public void EditGood(GoodDTO editGoodDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                var fileDelete = editGoodDto.Photo;
                var fileFolder = ContentPathBuilder.GetGoodImageFolderPath();
                var fileThumbnailsFolder = ContentPathBuilder.GetGoodThumbNailsImageFolderPath();
                var fileThumbnailsDelete = Path.Combine(fileThumbnailsFolder, fileDelete);
                fileDelete = Path.Combine(fileFolder, fileDelete);
                if (File.Exists(fileDelete))
                    File.Delete(fileDelete);

                //Thumbnails                
                if (File.Exists(fileThumbnailsDelete))
                    File.Delete(fileThumbnailsDelete);

                var fileName = Guid.NewGuid().ToString();// ECA34DB2-C885-4277-86DC-287BCEE00FAD
                string extension = System.IO.Path.GetExtension(formFile.FileName);
                fileName = $"{fileName}{extension}";
                var filePath = Path.Combine(fileFolder, fileName);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyToAsync(fileStream).Wait();
                }
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
            var fileThumbnailsFolder = ContentPathBuilder.GetGoodThumbNailsImageFolderPath();
            var fileThumbnailsDelete = Path.Combine(fileThumbnailsFolder, fileDelete);
            fileDelete = Path.Combine(fileFolder, fileDelete);
            if (File.Exists(fileDelete))
                File.Delete(fileDelete);
            //Thumbnails                
            if (File.Exists(fileThumbnailsDelete))
                File.Delete(fileThumbnailsDelete);

            _repository.Delete(id);
        }
    }
}
