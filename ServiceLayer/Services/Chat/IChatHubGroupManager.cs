using ServiceLayer.API;
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
        /// <param name="userIdentifier">User Identifier In ChatHub</param>
        /// <param name="groupName">Group Identifier For Users</param>
        void AddToGroup(string userIdentifier, string groupName);
        
        /// <summary>
        /// Gets Active Groups
        /// </summary>
        /// <returns></returns>
        void RemoveFromGroup(string userIdentifier, string groupName);

        /// <summary>
        /// Gets Active Groups
        /// </summary>
        /// <returns></returns>
        List<string> GetActiveGroups();

        /// <summary>
        /// Gets Specified User's Active Groups
        /// </summary>
        /// <param name="userIdentifier">User's Idenfier in ChatHub</param>
        /// <returns></returns>
        ServiceResult<List<string>> GetUserGroups(string userIdentifier);

        /// <summary>
        /// Gets Specified Group UserIdentifiers
        /// </summary>
        /// <param name="groupName">Specified Group Name</param>
        /// <returns></returns>
        ServiceResult<List<string>> GetGroupUsers(string groupName);


        public void ChangeUserGroup(string userIdentifier, string perviousGroupName, string newGroupName);
    }
    public class ChatHubGroupManager : IChatHubGroupManager
    {
        private static Dictionary<string, List<string>> _UserGroups;
        private static Dictionary<string, List<string>> _GroupUsers;

        public ChatHubGroupManager()
        {
            _UserGroups = _UserGroups ?? new Dictionary<string, List<string>>();
            _GroupUsers = _GroupUsers ?? new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Adds User To Specified Group & Adds Group To User's GroupsList
        /// </summary>
        /// <param name="userIdentifier">User Identifier In ChatHub</param>
        /// <param name="groupName">Group Identifier For Users</param>
        public void AddToGroup(string userIdentifier, string groupName)
        {
            if (!_UserGroups.ContainsKey(userIdentifier))
                _UserGroups[userIdentifier] = new List<string>() { groupName };
            else
                _UserGroups[userIdentifier].Add(groupName);

            if (!_GroupUsers.ContainsKey(groupName))
                _GroupUsers[groupName] = new List<string>() { userIdentifier };
            else
                _GroupUsers[groupName].Add(userIdentifier);

        }

        public void ChangeUserGroup(string userIdentifier, string perviousGroupName, string newGroupName)
        {
            RemoveFromGroup(userIdentifier, perviousGroupName);
            AddToGroup(userIdentifier, newGroupName);
        }

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
        /// <param name="userIdentifier">User's Idenfier in ChatHub</param>
        /// <returns></returns>
        public ServiceResult<List<string>> GetUserGroups(string userIdentifier)
        {
            if (!_UserGroups.ContainsKey(userIdentifier))
                return new ServiceResult<List<string>>("UserIdentifier Not Found");

            return new ServiceResult<List<string>>(_UserGroups[userIdentifier]);
        }

        /// <summary>
        /// Removes User To Specified Group & Removes Group To User's GroupsList
        /// </summary>
        /// <param name="userIdentifier">User Identifier In Hub</param>
        /// <param name="groupName">Group Identifier For Users</param>
        public void RemoveFromGroup(string userIdentifier, string groupName)
        {
            if (_UserGroups.ContainsKey(userIdentifier))
            {
                _UserGroups[userIdentifier].Remove(groupName);

                if (!_UserGroups[userIdentifier].Any())
                    _UserGroups.Remove(userIdentifier);
            }

            if (_GroupUsers.ContainsKey(groupName))
            {
                _GroupUsers[groupName].Remove(userIdentifier);

                if (!_GroupUsers[groupName].Any())
                    _GroupUsers.Remove(groupName);
            }
        }
    }
}
