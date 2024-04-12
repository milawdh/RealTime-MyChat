using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Index(nameof(Title),Name = "IX_TTblFileServer_Title", IsUnique = true)]
    public class TblFileServer : FullAuditedEntity<TblFileServer, Guid>
    {
        public string Title { get; set; }
        public string ConnectionString { get; set; }

        [InverseProperty(nameof(TblMedia.FileServer))]
        public virtual ICollection<TblMedia> TblMedias { get; set; }
    }

}
