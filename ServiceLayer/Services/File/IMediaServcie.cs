using Domain.API;
using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
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
        ServiceResult<Guid> Add(CreateUpdateMediaDto dto, IFormFile formFile);
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

        public ServiceResult<Guid> Add(CreateUpdateMediaDto dto, IFormFile formFile)
        {
            _core.BeginTransaction();
            try
            {
                TblMedia tblMedia = dto.Adapt<TblMedia>();
                tblMedia.FileServerId = _fileServerService.GetActiveFileServer().Id;
                tblMedia.FileName = formFile.FileName;
                tblMedia.FileMimType = formFile.ContentType;

                _core.TblMedia.Add(tblMedia);
                _core.Save();

                var addFileResult = _fileService.Add(formFile, tblMedia);
                if (addFileResult.Failure)
                    return new ServiceResult<Guid>("Error Occured On Saving File!");

                _core.CommitTransaction();
                return new ServiceResult<Guid>(tblMedia.Id);
            }
            catch (Exception ex)
            {
                ElmahCore.ElmahExtensions.RaiseError(ex);
                return new ServiceResult<Guid>("Error Occured On Media Saving : " + ex.Message);
            }
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
    }
}
