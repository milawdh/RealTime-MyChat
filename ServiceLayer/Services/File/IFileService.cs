using Domain.API;
using Domain.DataLayer.Contexts.FileDbContext;
using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
using Domain.Entities.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.File
{
    public interface IFileService
    {
        ServiceResult<byte[]> Get(TblMedia media);
        ServiceResult Delete(TblMedia tblMedia, TblFileServer tblFileServer = null);
        ServiceResult Add(IFormFile file, TblMedia tblMedia, TblFileServer tblFileServer = null);
    }
    public class FileService : IFileService
    {
        private readonly Core _core;
        private readonly IFileServerService _fileServerService;
        public FileService(Core core, IFileServerService fileServerService)
        {
            this._core = core;
            _fileServerService = fileServerService;
        }

        public ServiceResult Add(IFormFile file, TblMedia tblMedia, TblFileServer tblFileServer = null)
        {
            var fileServer = tblFileServer ?? _fileServerService.GetActiveFileServer();

            using (FileDbContext fileDbContext = new FileDbContext(fileServer.ConnectionString))
            {
                if (fileDbContext.Set<TblFile>().Any(x => x.Id == tblMedia.Id))
                    return new ServiceResult("The File Id Is Already Exist");

                MemoryStream memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);

                TblFile tblFile = new TblFile();
                tblFile.Id = tblMedia.Id;
                tblFile.Data = memoryStream.ToArray();

                fileDbContext.TblFile.Add(tblFile);
                fileDbContext.SaveChanges();
            }

            return new ServiceResult();
        }

        public ServiceResult<byte[]> Get(TblMedia media)
        {
            var fileServer = _fileServerService.GetActiveFileServer();

            using (FileDbContext fileDbContext = new FileDbContext(fileServer.ConnectionString))
            {
                if (!fileDbContext.TblFile.Any(x => x.Id == media.Id))
                    return new ServiceResult<byte[]>("The File Id Doesn't Exist");

                return new ServiceResult<byte[]>(fileDbContext.TblFile.FirstOrDefault(x => x.Id == media.Id).Data);
            }
        }

        public ServiceResult Delete(TblMedia tblMedia, TblFileServer tblFileServer = null)
        {
            var fileServer = tblFileServer ?? _fileServerService.GetActiveFileServer();

            using (FileDbContext fileDbContext = new FileDbContext(fileServer.ConnectionString))
            {
                if (fileDbContext.TblFile.Any(f => f.Id == tblMedia.Id))
                {
                    fileDbContext.TblFile.Remove(fileDbContext.TblFile.FirstOrDefault(x => x.Id == tblMedia.Id));
                    fileDbContext.SaveChanges();
                }
            }
            return new ServiceResult();
        }
    }
}
