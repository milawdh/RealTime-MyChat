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
        ServiceResult<byte[]> Get(Guid Id);
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
                if (fileDbContext.TblFiles.Any(x => x.Id == tblMedia.Id))
                    return new ServiceResult("The File Id Is Already Exist");

                MemoryStream memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);

                TblFile tblFile = new TblFile();
                tblFile.Id = tblMedia.Id;
                tblFile.Data = memoryStream.ToArray();

                fileDbContext.TblFiles.Add(tblFile);
                fileDbContext.SaveChanges();
            }

            return new ServiceResult();
        }
        
        public ServiceResult<byte[]> Get(Guid Id)
        {
            var fileServer = _fileServerService.GetActiveFileServer();

            using (FileDbContext fileDbContext = new FileDbContext(fileServer.ConnectionString))
            {
                if (fileDbContext.TblFiles.Any(x => x.Id == Id))
                    return new ServiceResult<byte[]>("The File Id Is Already Exist");

                return new ServiceResult<byte[]>(fileDbContext.TblFiles.FirstOrDefault(x => x.Id == Id).Data);
            }
        }

        public ServiceResult Delete(TblMedia tblMedia, TblFileServer tblFileServer = null)
        {
            var fileServer = tblFileServer ?? _fileServerService.GetActiveFileServer();

            using (FileDbContext fileDbContext = new FileDbContext(fileServer.ConnectionString))
            {
                if (fileDbContext.TblFiles.Any(f => f.Id == tblMedia.Id))
                {
                    fileDbContext.TblFiles.Remove(fileDbContext.TblFiles.FirstOrDefault(x => x.Id == tblMedia.Id));
                    fileDbContext.SaveChanges();
                }
            }
            return new ServiceResult();
        }
    }
}
