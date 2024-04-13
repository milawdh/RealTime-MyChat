using Domain.API;
using Domain.Audited.Models;
using Domain.DataLayer.Repository;
using Domain.DataLayer.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Index(nameof(Title), Name = "IX_TTblFileServer_Title", IsUnique = true)]
    public class TblFileServer : FullAuditedEntity<TblFileServer, Guid>
    {
        public string Title { get; set; }
        public string ConnectionString { get; set; }

        [InverseProperty(nameof(TblMedia.FileServer))]
        public virtual ICollection<TblMedia> TblMedias { get; set; }

        public bool IsActive { get; set; } = true;


        public override ServiceResult ValidateAdd(TblFileServer entity, Core core)
        {
            if (core.TblFileServer.Any(x => x.Title == entity.Title))
                return new ServiceResult("There is another File Server With This Title Exist!");

            if (entity.IsActive)
                return new ServiceResult("File Server is Active You can't Delete It!");

            return base.ValidateAdd(entity, core);
        }

        public override ServiceResult ValidateUpdate(TblFileServer entity, Core core)
        {
            if (core.TblFileServer.Any(x => x.Title == entity.Title && x.Id != entity.Id))
                return new ServiceResult("There is another File Server With This Title Exist!");

            return base.ValidateUpdate(entity, core);
        }
    }

}
