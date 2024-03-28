let user = {
    id: "",
    name: "",
    number: "",
    pic: ""
};
let ChatRoomsList = [
    //Sample
    //{
    //    id: "",
    //    name: "Anish",
    //    number: "+91 91231 40293",
    //lastMessage:{
    //    body: "",
    //    time: "",
    //    status: 0,
    //    SenderUserName:""
    // },
    //    pic: "images/asdsd12f34ASd231.png",
    //    lastSeen: "Apr 29 2018 17:58:02"
    //},
];
let groupList = [
    //Sample
    //{
    //    id: "",
    //    name: "Programmers",
    //    members: [0, 1, 3],
    //lastMessage:{
    //    body: "",
    //    time: "",
    //    status: 0,
    //    SenderUserName:"" 
    // },
    //    pic: "images/0923102932_aPRkoW.jpg"
    //},
];
let messages = [
    //Sample
    //{
    //    id: 0,
    //    sender: 2,
    //    body: "where are you, buddy?",
    //    time: "April 25, 2018 13:21:03",
    //    status: 2,
    //    recvId: 0,
    //    recvIsGroup: false
    //},
];


function SetUserInfo(userDto) {
    user.id = userDto.id
    user.name = userDto.name
    user.pic = userDto.pic
    user.number = userDto.tell
    ChatRoomsList = userDto.chatRooms
    //groupList = userDto.groupChatRooms
    init();
}


// message status - 0:sent, 1:delivered, 2:read



let MessageUtils = {
    getByGroupId: (groupId) => {
        return messages.filter(msg => msg.recvIsGroup && msg.recvId === groupId);
    },
    getByContactId: (contactId) => {
        return messages.filter(msg => {
            return !msg.recvIsGroup && (msg.recvId === contactId)
            /*(msg.sender === user.id && msg.recvId === contactId) || (msg.re === contactId && msg.recvId === user.id)*/
        });
    },
    getMessages: () => {
        return messages;
    },
    getGroupChatrooms: () => {
        return groupList;
    },
    getPrivateChatrooms: () => {
        return ChatRoomsList;
    },
    changeStatusById: (options) => {
        messages = messages.map((msg) => {
            if (options.isGroup) {
                if (msg.recvIsGroup && msg.recvId === options.id) msg.status = 2;
            } else {
                if (!msg.recvIsGroup && msg.sender === options.id && msg.recvId === user.id) msg.status = 2;
            }
            return msg;
        });
    },
    addMessage: (msg) => {
        msg.id = msg.id;
        messages.push(msg);
    }
};