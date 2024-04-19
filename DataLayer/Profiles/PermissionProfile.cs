using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Profiles
{
    public static class PermissionProfile
    {
        public static class Permission_TblMessage
        {
            public static string Group = nameof(Permission_TblMessage);

            public static class Childs
            {
                public static string Add = Group + "_" + nameof(Add);
                public static string Update = Group + "_" + nameof(Update);
                public static string Delete = Group + "_" + nameof(Delete);
            }
        }
        public static class Permission_TblUser
        {
            public static string Group = nameof(Permission_TblUser);

            public static class Childs
            {
                public static string Add = Group + "_" + nameof(Add);
                public static string Update = Group + "_" + nameof(Update);
                public static string Delete = Group + "_" + nameof(Delete);
            }
        }
        public static class Permission_TblMedia
        {
            public static string Group = nameof(Permission_TblMedia);

            public static class Childs
            {
                public static string Add = Group + "_" + nameof(Add);
                public static string Update = Group + "_" + nameof(Update);
                public static string Delete = Group + "_" + nameof(Delete);
            }
        }
        public static class Permission_TblRole
        {
            public static string Group = nameof(Permission_TblRole);

            public static class Childs
            {
                public static string Add = Group + "_" + nameof(Add);
                public static string Update = Group + "_" + nameof(Update);
                public static string Delete = Group + "_" + nameof(Delete);
            }
        }
        public static class Permission_TblUserContact
        {
            public static string Group = nameof(Permission_TblUserContact);

            public static class Childs
            {
                public static string Add = Group + "_" + nameof(Add);
                public static string Update = Group + "_" + nameof(Update);
                public static string Delete = Group + "_" + nameof(Delete);
            }
        }
        public static class Permission_TblChatRoom
        {
            public static string Group = nameof(Permission_TblChatRoom);

            public static class Childs
            {
                public static string Add = Group + "_" + nameof(Add);
                public static string Update = Group + "_" + nameof(Update);
                public static string Delete = Group + "_" + nameof(Delete);
            }
        }
        public static class Permission_TblFileServer
        {
            public static string Group = nameof(Permission_TblFileServer);

            public static class Childs
            {
                public static string Add = Group + "_" + nameof(Add);
                public static string Update = Group + "_" + nameof(Update);
                public static string Delete = Group + "_" + nameof(Delete);
            }
        }

        public static class Permission_TblUserChatRoomRel
        {
            public static string Group = nameof(Permission_TblUserChatRoomRel);

            public static class Childs
            {
                public static string Add = Group + "_" + nameof(Add);
                public static string Update = Group + "_" + nameof(Update);
                public static string Delete = Group + "_" + nameof(Delete);
            }
        }

        public static class Permission_TblUserChatRoomMapPermission
        {
            public static string Group = nameof(Permission_TblUserChatRoomMapPermission);

            public static class Childs
            {
                public static string Add = Group + "_" + nameof(Add);
                public static string Update = Group + "_" + nameof(Update);
                public static string Delete = Group + "_" + nameof(Delete);
            }
        }
    }
}
