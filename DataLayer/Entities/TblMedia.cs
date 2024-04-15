using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Domain.Audited.Models;
using Domain.DataLayer.Repository;
using Domain.DataLayer.UnitOfWorks;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblMedia : FullAuditedEntity<TblMedia, Guid>
{
    public MediaType MediaType { get; set; }

    public string FileName { get; set; }
    public string FileMimType { get; set; }

    [ForeignKey(nameof(FileServerId))]
    [InverseProperty(nameof(TblFileServer.TblMedias))]
    public TblFileServer FileServer { get; set; }
    public Guid FileServerId { get; set; }


    public Guid MessageId { get; set; }

    [ForeignKey(nameof(MessageId))]
    [InverseProperty(nameof(TblMessage.TblMedias))]
    public virtual TblMessage Message { get; set; } = null!;


    public override IQueryable<TblMedia> ValidateGetPermission(Core core, IQueryable<TblMedia> entities, IUserInfoContext userInfoContext)
    {
        var medias = core.TblMessage.Get().SelectMany(x => x.TblMedias.Select(x => x.Id)).ToList();
        return base.ValidateGetPermission(core, entities.Where(x => medias.Contains(x.Id)), userInfoContext);
    }
}
