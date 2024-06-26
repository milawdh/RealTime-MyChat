let getById = (id, parent) =>
    parent ? parent.getElementById(id) : getById(id, document);
let getByClass = (className, parent) =>
    parent
        ? parent.getElementsByClassName(className)
        : getByClass(className, document);

const DOM = {
    chatListArea: getById("chat-list-area"),
    messageArea: getById("message-area"),
    inputArea: getById("input-area"),
    chatList: getById("chat-list"),
    messages: getById("messages"),
    chatListItem: getByClass("chat-list-item"),
    messageAreaName: getById("name", this.messageArea),
    messageAreaPic: getById("pic", this.messageArea),
    messageAreaNavbar: getById("navbar", this.messageArea),
    messageAreaDetails: getById("details", this.messageAreaNavbar),
    messageAreaOverlay: getByClass("overlay", this.messageArea)[0],
    messageInput: getById("input"),
    profileSettings: getById("profile-settings"),
    profilePic: getById("profile-pic"),
    profilePicInput: getById("profile-pic-input"),
    inputName: getById("input-name"),
    username: getById("username"),
    displayPic: getById("display-pic"),
};

let mClassList = (element) => {
    return {
        add: (className) => {
            element.classList.add(className);
            return mClassList(element);
        },
        remove: (className) => {
            element.classList.remove(className);
            return mClassList(element);
        },
        contains: (className, callback) => {
            if (element.classList.contains(className)) callback(mClassList(element));
        },
    };
};

// 'areaSwapped' is used to keep track of the swapping
// of the main area between chatListArea and messageArea
// in mobile-view
let areaSwapped = false;

// 'chat' is used to store the current chat
// which is being opened in the message area
let chat = null;

// this will contain all the chats that is to be viewed
// in the chatListArea
let chatList = [];

// this will be used to store the date of the last message
// in the message area
let lastDate = "";

// 'populateChatList' will generate the chat list
// based on the 'messages' in the datastore
let populateChatList = () => {
    chatList = [];

    // 'present' will keep track of the chats
    // that are already included in chatList
    // in short, 'present' is a Map DS
    let present = {};

    MessageUtils.getMessages()
        .forEach((msg) => {
            let chat = {};
            chat.isGroup = msg.recvIsGroup;
            chat.msg = msg;

            if (msg.recvIsGroup) {
                chat.group = groupList.find((group) => group.id === msg.recvId);
                chat.name = chat.group.name;
            } else {
                chat.contact = ChatRoomsList.find((contact) =>
                    //msg.sender !== user.id
                    //    ? contact.id === msg.sender
                    //    : 
                    contact.id === msg.recvId
                );
                chat.name = chat.contact.name;
            }

            chat.unread = msg.sender !== user.id && msg.status < 2 ? 1 : 0;

            if (present[chat.name] !== undefined) {
                chatList[present[chat.name]].msg = msg;
                chatList[present[chat.name]].unread += chat.unread;
            } else {
                present[chat.name] = chatList.length;
                chatList.push(chat);
            }
        });
};
function CreateChatListItem(elem) {

    let statusClass = elem.lastMessage != null ? elem.lastMessage.status < 2 ? "far" : "fas" : "";
    let unreadClass = elem.lastMessage != null ? elem.lastMessage.status < 2 ? "unread" : "" : "";

    return `
		<div class="chat-list-item d-flex flex-row w-100 p-2 border-bottom ${unreadClass}"
        id="Ch${elem.id}" onclick="generateMessageArea(this, '${elem.id}')">
			<img src="${elem.pic}" alt="Profile Photo" class="img-fluid rounded-circle mr-2" style="height:50px;">
			<div class="w-50">
				<div class="name" id="ChLastMessageName${elem.id}">${elem.name}</div>
				<div class="small last-message" id="ChLastMessage${elem.id}">
                ${elem.lastMessage != null ? elem.lastMessage.SenderUserName === user.name
            ? '<i class="' + statusClass + ' fa-check-circle mr-1"></i>'
            : "" : ""
        }
                ${elem.lastMessage != null ? elem.lastMessage.body : ""}
                </div>
			</div>
			<div class="flex-grow-1 text-right">
				<div class="small time" id="ChTime${elem.id}" >${elem.lastMessage != null ? elem.lastMessage.time : ""}</div>

    <div id="ChReadDiv${elem.id}" >
        <div class="badge badge-success badge-pill small"
        id="ChRead-count${elem.id}" ${elem.notSeenMessagesCount <= 0 ? "hidden" : ""}>
            ${elem.notSeenMessagesCount}
            </div>
            
            ${elem.isNew ? `<span class="badge badge-success badge-pill small">New</span>` : ""}
            
                </div>
			</div>
		</div>
		`;
}
let viewChatList = () => {
    debugger
    DOM.chatList.innerHTML = "";

    ChatRoomsList
        .forEach((elem, index) => {
            DOM.chatList.innerHTML += CreateChatListItem(elem)
        });
}

let generateChatList = () => {
    debugger
    populateChatList();
    viewChatList();
};

let LoadDateToMessageArea = (date) => {
    DOM.messages.innerHTML = `
	<div class="mx-auto my-2 bg-primary text-white small py-1 px-2 rounded">
		${date.split(' ')[2]}
	</div>
	` + DOM.messages.innerHTML;
};

let LoadMessageToMessageArea = (msg) => {
    debugger



    var element = DOM.messages

    let sendStatus = `<i class="${msg.status < 2 ? "far" : "fas"
        } fa-check-circle"></i>`;

    element.innerHTML = `
  <div class="d-flex ${msg.sender == user.id ? `justify-content-end` : `justify-content-start`}">
	<div class="${msg.sender == user.id ? `self` : ``
        } p-1 my-1 mx-3 rounded bg-white shadow-sm message-item" id="MessageId${msg.id}">
		<div class="options">
		<a class="text-decoration-none" uk-toggle href="#msgDrId${msg.id}">
		<i class="fas fa-angle-down text-muted px-2"></i>
		</a>
		<div uk-dropdown="mode: click" class="uk-border-rounded" id="msgDrId${msg.id}">
			<ul class="uk-nav uk-dropdown-nav">
				${msg.sender == user.id ?
            `<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowEditMessage('${msg.id}')">Edit</a></li>
				<hr class="m-0">
				<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowDeleteMessage('${msg.id}')">Delete</a></li>
				<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowForwardMessage('${msg.id}','${formatLength(msg.body, 10)}')">Forward</a></li>
                `
            :
        `<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowForwardMessage('${msg.id}','${formatLength(msg.body, 10)}')">Forward</a></li>`
        }
			</ul>
		</div>
		</div>
		<a href="#">
        ${chat.type == 9 ? msg.senderUserName : ""}
        </a>
		<div class="d-flex flex-row">
        ${msg.fileApi != null ?
            '<a download href="' + msg.fileApi + '">DownloadFile</a>' : ""
        }
			<div class="body m-1 mr-2">${msg.body}</div>
			<div class="time ml-auto small text-right flex-shrink-0 align-self-end text-muted" style="width:75px;">
				${msg.time.split(' ')[0]}
				${msg.sender === user.id ? sendStatus : ""}
			</div>
		</div>
	</div>
    <div>
    </div>
        </div>
	` + element.innerHTML;

    let msgDate = msg.time.split(' ')[2];
    if (lastDate != msgDate) {
        LoadDateToMessageArea(msg.time);
        lastDate = msgDate;
    }

    DOM.messages.scrollTo(0, DOM.messages.scrollHeight);
};



let addDateToMessageArea = (date) => {
    DOM.messages.innerHTML += `
	<div class="mx-auto my-2 bg-primary text-white small py-1 px-2 rounded">
		${date.split(' ')[2]}
	</div>
	`;
};

let addMessageToMessageArea = (msg) => {
    debugger
    let msgDate = msg.time.split(' ')[2];
    if (lastDate != msgDate) {
        addDateToMessageArea(msg.time);
        lastDate = msgDate;
    }
    var element = DOM.messages

    let sendStatus = `<i class="${msg.status < 2 ? "far" : "fas"
        } fa-check-circle"></i>`;

    element.innerHTML += `
  <div class="d-flex ${msg.sender == user.id ? `justify-content-end` : `justify-content-start`}">
	<div class="${msg.sender == user.id ? `self` : ``
        } p-1 my-1 mx-3 rounded bg-white shadow-sm message-item" id="MessageId${msg.id}">
		<div class="options">
		<a class="text-decoration-none" uk-toggle href="#msgDrId${msg.id}">
		<i class="fas fa-angle-down text-muted px-2"></i>
		</a>
		<div uk-dropdown="mode: click" class="uk-border-rounded" id="msgDrId${msg.id}">
			<ul class="uk-nav uk-dropdown-nav">
				${msg.sender == user.id ?
            `<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowEditMessage('${msg.id}','${formatLength(msg.body, 10)}')">Edit</a></li>
				<hr class="m-0">
				<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowDeleteMessage('${msg.id}','${formatLength(msg.body, 10)}')">Delete</a></li>
				<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowForwardMessage('${msg.id}','${formatLength(msg.body, 10)}')">Forward</a></li>
                `
            :
        `<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowForwardMessage('${msg.id}','${formatLength(msg.body, 10)}')">Forward</a></li>`
        }
			</ul>
		</div>
		</div>
		<a href="#">
        ${chat.type == 9 ? msg.senderUserName : ""}
        </a>
		<div class="d-flex flex-row">
        ${msg.fileApi != null ?
            '<a download href="' + msg.fileApi + '">DownloadFile</a>' : ""
        }
			<div class="body m-1 mr-2">${msg.body}</div>
			<div class="time ml-auto small text-right flex-shrink-0 align-self-end text-muted" style="width:75px;">
				${msg.time.split(' ')[0]}
				${msg.sender === user.id ? sendStatus : ""}
			</div>
		</div>
	</div>
    <div>
    </div>
        </div>
	`;


    DOM.messages.scrollTo(0, DOM.messages.scrollHeight);
};


let generateMessageArea = async (elem, chatId) => {
    startRow = 0;
    isMessagesEnd = false;
    lastDate = "";

    chat = await GetChatRoomDetails(chatId);

    document.getElementById(`ChReadDiv${chatId}`).setAttribute("hidden", "")
    document.getElementById(`ChRead-count${chatId}`).innerText = 0;

    mClassList(DOM.inputArea).contains("d-none", (elem) =>
        elem.remove("d-none").add("d-flex")
    );
    mClassList(DOM.messageAreaOverlay).add("d-none");

    [...DOM.chatListItem].forEach((elem) => mClassList(elem).remove("active"));

    mClassList(elem).contains("unread", () => {
        MessageUtils.changeStatusById({
            isGroup: chat.isGroup,
            id: chat.isGroup ? chat.group.id : chat.contact.id,
        });
        mClassList(elem).remove("unread");
        mClassList(elem.querySelector("#unread-count")).add("d-none");
    });

    if (window.innerWidth <= 575) {
        mClassList(DOM.chatListArea).remove("d-flex").add("d-none");
        mClassList(DOM.messageArea).remove("d-none").add("d-flex");
        areaSwapped = true;
    } else {
        mClassList(elem).add("active");
    }

    DOM.messageAreaName.innerHTML = chat.name;
    DOM.messageAreaPic.src = chat.pic;
    DOM.messageAreaDetails.innerHTML = chat.navbarText;

    DOM.messages.innerHTML = "";

    LoadCurrentMessages();
    DOM.messages.onscrollend = function (ev) {
        if (DOM.messages.scrollTop == 0)
            LoadOtherMessages()
    };
    DOM.messages.scrollTo(0, DOM.messages.scrollHeight);
};


let showChatList = () => {
    if (areaSwapped) {
        mClassList(DOM.chatListArea).remove("d-none").add("d-flex");
        mClassList(DOM.messageArea).remove("d-flex").add("d-none");
        areaSwapped = false;
    }
};

let sendMessage = async () => {
    var file = document.getElementById('message-file-input')
    if (DOM.messageInput.value === "" && file.value == null)
        return;

    var sendMessageResult;

    if (file.value == "") {
        sendMessageResult = await SendMessageToHub(DOM.messageInput.value);
        DOM.messageInput.value = "";
        document.getElementById('message-file-input').files = null
        document.getElementById('message-file-input').value = null
    }
    else {
        var dataToSend = new FormData();
        dataToSend.append("messageBody", DOM.messageInput.value)
        dataToSend.append("file", file.files[0])
        document.getElementById('sender-button').onclick = null
        ShowNotificationText("File is uploading Please Wait!")
        $.ajax({
            url: '/Chat/SendMessageWithFile',
            type: 'POST',
            data: dataToSend,
            processData: false,
            contentType: false,
            success: function (data) {
                sendMessageResult = data.result;
                DOM.messageInput.value = "";
                document.getElementById('message-file-input').files = null
                document.getElementById('message-file-input').value = null
                document.getElementById('sender-button').onclick = sendMessage
                ShowNotificationText("Message Sent Successfully!")
            },
            error: function (data) {
            }
        });
    }

    if (sendMessageResult != null) {

    }
}


let showProfileSettings = () => {
    DOM.profileSettings.style.left = 0;
    DOM.profilePic.src = user.pic;
    DOM.inputName.value = user.name;
};

let hideProfileSettings = () => {
    DOM.profileSettings.style.left = "-110%";
    DOM.username.innerHTML = user.name;
};

window.addEventListener("resize", (e) => {
    if (window.innerWidth > 575) showChatList();
});

let init = () => {
    DOM.username.innerHTML = user.name;
    DOM.displayPic.src = user.pic;
    DOM.profilePic.stc = user.pic;
    DOM.profilePic.addEventListener("click", () => DOM.profilePicInput.click());
    DOM.profilePicInput.addEventListener("change", () =>
        console.log(DOM.profilePicInput.files[0])
    );
    DOM.inputName.addEventListener("blur", (e) => (user.name = e.target.value));
    generateChatList();

    console.log("Click the Image at top-left to open settings.");
};

init();
