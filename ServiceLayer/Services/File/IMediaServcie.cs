using Domain.API;
using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
using Domain.Enums;
using DomainShared.Dtos.Media;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.File
{
    public interface IMediaServcie
    {
        ServiceResult<Guid> Add(CreateUpdateMediaDto dto);
        ServiceResult Delete(TblMedia tblMedia);
    }
    public class MediaService : IMediaServcie
    {
        private readonly Core _core;
        private readonly IFileService _fileService;
        private readonly IFileServerService _fileServerService;
        public MediaService(Core core, IFileService fileService, IFileServerService fileServerService)
        {
            _core = core;
            this._fileService = fileService;
            _fileServerService = fileServerService;
        }

        public ServiceResult<Guid> Add(CreateUpdateMediaDto dto)
        {
            TblMedia tblMedia = new TblMedia();
            tblMedia.Message = dto.Message;
            tblMedia.FileServerId = _fileServerService.GetActiveFileServer().Id;
            tblMedia.FileName = dto.File.FileName;
            tblMedia.FileMimType = dto.File.ContentType;
            tblMedia.MediaType = GetMediaType(dto.File);

            _core.TblMedia.Add(tblMedia);
            _core.Save();

            var addFileResult = _fileService.Add(dto.File, tblMedia);
            if (addFileResult.Failure)
                return new ServiceResult<Guid>("Error Occured On Saving File!");

            return new ServiceResult<Guid>(tblMedia.Id);
        }

        public ServiceResult Delete(TblMedia tblMedia)
        {
            var removeFileResult = _fileService.Delete(tblMedia);
            if (removeFileResult.Success)
            {
                _core.TblMedia.Reomve(tblMedia);
                _core.Save();
            }
            return new ServiceResult();
        }

        public MediaType GetMediaType(IFormFile file)
        {
            if (file.ContentType.StartsWith("image"))
                return Domain.Enums.MediaType.Image;
            if (file.ContentType.StartsWith("video"))
                return Domain.Enums.MediaType.Video;
            return MediaType.File;
        }
    }
}
