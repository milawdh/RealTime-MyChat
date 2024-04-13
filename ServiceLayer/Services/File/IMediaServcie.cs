using Domain.API;
using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
using DomainShared.Dtos.Media;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.File
{
    public interface IMediaServcie
    {

    }
    public class MediaService : IMediaServcie
    {
        private readonly Core _core;
        public MediaService(Core core)
        {
            _core = core;
        }

        public ServiceResult<Guid> Add(CreateUpdateMediaDto dto)
        {
            TblMedia tblMedia = dto.Adapt<TblMedia>();

            _core.TblMedia.Add(tblMedia);
            _core.Save();

            return new ServiceResult<Guid>(tblMedia.Id);
        }

        public ServiceResult Update(CreateUpdateMediaDto dto, TblMedia tblMedia)
        {
            tblMedia.FileName = dto.FileName;
            tblMedia.FileServerId = dto.FileServerId;
            tblMedia.FileMimType = dto.FileMimType;
            tblMedia.MediaType = dto.MediaType;

            _core.TblMedia.Update(tblMedia);
            _core.Save();

            return new ServiceResult();
        }
    }
}
