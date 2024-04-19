using Domain.Audited.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TblUserChatRoomMapPermission : FullAuditedEntity<TblUserChatRoomMapPermission, Guid>
    {
        public Guid UserChatRoomRelId { get; set; }
        public TblUserChatRoomRel UserChatRoomRel { get; set; }

        public Guid PermissionId { get; set; }
        public TblPermission Permission { get; set; }

        public PermissionType PermissionType { get; set; }
    }
}
