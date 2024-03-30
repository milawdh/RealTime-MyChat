using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.API;
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

        public void ChangeUserGroup(string userConnectionId, string perviousGroupName, string newGroupName);
    }
    public class ChatHubGroupManager : IChatHubGroupManager
    {
        private static Dictionary<string, List<string>> _UserGroups;
        private static Dictionary<string, List<string>> _GroupUsers;
        private readonly IHubContext<ChatHub, IChatHubApi> _chatHub;


        public ChatHubGroupManager(IHubContext<ChatHub, IChatHubApi> chatHub)
        {
            _UserGroups = _UserGroups ?? new Dictionary<string, List<string>>();
            _GroupUsers = _GroupUsers ?? new Dictionary<string, List<string>>();
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

        public void ChangeUserGroup(string userConnectionId, string perviousGroupName, string newGroupName)
        {
            RemoveFromGroup(userConnectionId, perviousGroupName);
            AddToGroup(userConnectionId, newGroupName);
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

            _chatHub.Groups.RemoveFromGroupAsync(userConnectionId, groupName).Wait();
        }

        #endregion

        #region Get

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

        public void SetDisconnected(string userConnectionId)
        {
            if (_UserGroups.ContainsKey(userConnectionId))
            {
                if (_UserGroups[userConnectionId] is not null)
                {
                    _UserGroups[userConnectionId].ForEach(x => _GroupUsers.Remove(x));
                    _UserGroups.Remove(userConnectionId);
                }
            }
        }

        #endregion

    }
}
