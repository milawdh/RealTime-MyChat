using Domain.API;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Hubs;
using ServiceLayer.Hubs.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Chat
{
    public interface IChatHubGroupManager
    {
        /// <summary>
        /// Adds User To Specified Group & Adds Group To User's GroupsList
        /// </summary>
        /// <param name="userConnectionId">User Identifier In ChatHub</param>
        /// <param name="groupName">Group Identifier For Users</param>
        void AddToGroup(string userConnectionId, string groupName);
        void SetCurrentChatRoom(string userConnectionId, string groupName);

        /// <summary>
        /// Gets Active Groups
        /// </summary>
        /// <returns></returns>
        void RemoveFromGroup(string userConnectionId, string groupName);

        /// <summary>
        /// Gets Active Groups
        /// </summary>
        /// <returns></returns>
        List<string> GetActiveGroups();

        /// <summary>
        /// Gets Specified User's Active Groups
        /// </summary>
        /// <param name="userConnectionId">User's Idenfier in ChatHub</param>
        /// <returns></returns>
        ServiceResult<List<string>> GetUserGroups(string userConnectionId);

        /// <summary>
        /// Gets Specified Group UserIdentifiers
        /// </summary>
        /// <param name="groupName">Specified Group Name</param>
        /// <returns></returns>
        ServiceResult<List<string>> GetGroupUsers(string groupName);

        void SetDisconnected(string userConnectionId);

        public void ChangeUserGroups(string userConnectionId, string perviousGroupName, string newGroupName);

        ServiceResult<string> GetCurrentChatRoom(string userConnectionId);

        Dictionary<string, string> GetUsersCurrentChatRoomDict();
    }
    public class ChatHubGroupManager : IChatHubGroupManager
    {
        private static Dictionary<string, List<string>> _UserGroups;
        private static Dictionary<string, string> _UserCurrentChatRoom;
        private static Dictionary<string, List<string>> _GroupUsers;

        private readonly IHubContext<ChatHub, IChatHubApi> _chatHub;

        public ChatHubGroupManager(IHubContext<ChatHub, IChatHubApi> chatHub)
        {
            _UserGroups = _UserGroups ?? new Dictionary<string, List<string>>();
            _GroupUsers = _GroupUsers ?? new Dictionary<string, List<string>>();
            _UserCurrentChatRoom = _UserCurrentChatRoom ?? new Dictionary<string, string>();
            _chatHub = chatHub;
        }

        #region SetValues

        /// <summary>
        /// Adds User To Specified Group & Adds Group To User's GroupsList
        /// </summary>
        /// <param name="userConnectionId">User Identifier In ChatHub</param>
        /// <param name="groupName">Group Identifier For Users</param>
        public void AddToGroup(string userConnectionId, string groupName)
        {
            if (!_UserGroups.ContainsKey(userConnectionId))
                _UserGroups[userConnectionId] = new List<string>() { groupName };
            else
                _UserGroups[userConnectionId].Add(groupName);

            if (!_GroupUsers.ContainsKey(groupName))
                _GroupUsers[groupName] = new List<string>() { userConnectionId };
            else
                _GroupUsers[groupName].Add(userConnectionId);

            _chatHub.Groups.AddToGroupAsync(userConnectionId, groupName).Wait();
        }

        /// <summary>
        /// Changes User's CurrentChatRoom
        /// </summary>
        /// <param name="userConnectionId"></param>
        /// <param name="perviousGroupName"></param>
        /// <param name="newGroupName"></param>
        public void ChangeUserGroups(string userConnectionId, string perviousGroupName, string newGroupName)
        {
            RemoveFromGroup(userConnectionId, perviousGroupName);
            AddToGroup(userConnectionId, newGroupName);
        }

        /// <summary>
        /// Sets UserCurrent ChatRoom That is in that!
        /// </summary>
        /// <param name="userConnectionId">User Identifier In Hub</param>
        /// <param name="groupName">Group Identifier For Users</param>
        public void SetCurrentChatRoom(string userConnectionId, string groupName)
        {
            if (GetCurrentChatRoom(userConnectionId).Result != groupName)
            {
                if (_UserCurrentChatRoom.ContainsKey(userConnectionId))
                {
                    ChangeUserGroups(userConnectionId, _UserCurrentChatRoom[userConnectionId], groupName);
                    _UserCurrentChatRoom[userConnectionId] = groupName;
                }
                else
                {
                    _UserCurrentChatRoom.Add(userConnectionId, groupName);
                    AddToGroup(userConnectionId, groupName);
                }
            }
        }

        /// <summary>
        /// Removes User To Specified Group & Removes Group To User's GroupsList
        /// </summary>
        /// <param name="userConnectionId">User Identifier In Hub</param>
        /// <param name="groupName">Group Identifier For Users</param>
        public void RemoveFromGroup(string userConnectionId, string groupName)
        {
            if (_UserGroups.ContainsKey(userConnectionId))
            {
                _UserGroups[userConnectionId].Remove(groupName);

                if (!_UserGroups[userConnectionId].Any())
                    _UserGroups.Remove(userConnectionId);
            }

            if (_GroupUsers.ContainsKey(groupName))
            {
                _GroupUsers[groupName].Remove(userConnectionId);

                if (!_GroupUsers[groupName].Any())
                    _GroupUsers.Remove(groupName);
            }

            if (_UserCurrentChatRoom.ContainsKey(userConnectionId))
            {
                _UserCurrentChatRoom.Remove(userConnectionId);
            }

            _chatHub.Groups.RemoveFromGroupAsync(userConnectionId, groupName).Wait();
        }

        /// <summary>
        /// Removes All Groups Data When User Is Disconnected
        /// </summary>
        /// <param name="userConnectionId">User Identifier In Hub</param>
        public void SetDisconnected(string userConnectionId)
        {
            if (_UserGroups.ContainsKey(userConnectionId))
            {
                if (_UserGroups[userConnectionId] is not null)
                {
                    //Remove User From All Groups
                    _UserGroups[userConnectionId].ForEach(x =>
                    {
                        if (_GroupUsers.ContainsKey(x))
                            if (_GroupUsers[x].Any(v => v == userConnectionId))
                                _GroupUsers[x].Remove(userConnectionId);
                    });

                    //Remove User's Groups
                    _UserGroups.Remove(userConnectionId);
                }
            }

            //Remove User Current Room
            if (_UserCurrentChatRoom.ContainsKey(userConnectionId))
                _UserCurrentChatRoom.Remove(userConnectionId);
        }

        #endregion

        #region Get Value

        /// <summary>
        /// Gets Active Groups
        /// </summary>
        /// <returns></returns>
        public List<string> GetActiveGroups() => _GroupUsers.Select(x => x.Key).ToList();

        /// <summary>
        /// Gets Specified Group UserIdentifiers
        /// </summary>
        /// <param name="groupName">Specified Group Name</param>
        /// <returns></returns>
        public ServiceResult<List<string>> GetGroupUsers(string groupName)
        {
            if (!_GroupUsers.ContainsKey(groupName))
                return new ServiceResult<List<string>>("Group Not Found");

            return new ServiceResult<List<string>>(_GroupUsers[groupName]);
        }

        /// <summary>
        /// Gets Specified User's Active Groups
        /// </summary>
        /// <param name="userConnectionId">User's Idenfier in ChatHub</param>
        /// <returns></returns>
        public ServiceResult<List<string>> GetUserGroups(string userConnectionId)
        {
            if (!_UserGroups.ContainsKey(userConnectionId))
                return new ServiceResult<List<string>>("UserIdentifier Not Found");

            return new ServiceResult<List<string>>(_UserGroups[userConnectionId]);
        }

        /// <summary>
        /// Gets User's Current ChatRoom
        /// </summary>
        /// <param name="userConnectionId">User Identifier In Hub</param>
        /// <returns></returns>
        public ServiceResult<string> GetCurrentChatRoom(string userConnectionId)
        {
            if (!_UserCurrentChatRoom.ContainsKey(userConnectionId))
                return new ServiceResult<string>("There is No ChatRoom You're In!");

            return new ServiceResult<string>
            {
                Messages = new(),
                Success = true,
                Failure = false,
                Result = _UserCurrentChatRoom[userConnectionId]
            };
        }

        public Dictionary<string, string> GetUsersCurrentChatRoomDict() => _UserCurrentChatRoom;

        #endregion

    }
}
