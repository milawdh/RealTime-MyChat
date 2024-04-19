var chatConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .withStatefulReconnect({ bufferSize: 1000 })
    .build();

StyleDisconnected()
StartChatConnection()

let startRow = 0;
let isMessagesEnd = false;

function StartChatConnection() {
    chatConnection.start().then(function () {
        StyleConnected()
    }).catch(function (err) {
        return console.error(err.toString());
    });
}

async function setCurrentChatRoom() {
    await chatConnection.invoke("SetCurrentChatRoom", chat != null ? chat.id : null);
}

function StyleDisconnected() {
    document.getElementById('Dimmer').innerHTML = `<div style="position: absolute; left: 50%; top:50%;">
            <div uk-spinner style="position:relative;z-index:9999;left:-50%;top:-50%;">
                Connecting ...
            </div>
        </div>`
    document.body.style.filter = 'brightness(50%)'
    document.body.style.pointerEvents = 'none'
}
function StyleConnected() {
    document.getElementById('Dimmer').innerHTML = ``
    document.body.style.filter = ''
    document.body.style.pointerEvents = 'auto'
}

async function GetChatRoomDetails(charoomId) {
    return await chatConnection.invoke("GetChatRoomDetails", charoomId).then(function (res) { return res.result; });
}

//async function GetChatRoomMessages() {
//    return await chatConnection.invoke("GetCurrentChatRoomMessages").then(function (res) { return res.result; });
//}

function LoadOtherMessages() {
var LoadOtherMessagesHtml = ""
    debugger
    if (!isMessagesEnd) {

        var element = document.getElementById('lazy-div')
        chatConnection.stream("GetCurrentChatRoomMessages", startRow)
            .subscribe({
                next: (msg) => {
                    let sendStatus = `<i class="${msg.result.status < 2 ? "far" : "fas"} fa-check-circle"></i>`;

                    LoadOtherMessagesHtml = `
  <div class="d-flex ${msg.result.sender == user.id ? `justify-content-end` : `justify-content-start`}">
	<div class="${msg.result.sender == user.id ? `self` : ``
                        } p-1 my-1 mx-3 rounded bg-white shadow-sm message-item" id="MessageId${msg.result.Id}">
		<div class="options">
		<a class="text-decoration-none" uk-toggle href="#msgDrId${msg.result.id}">
		<i class="fas fa-angle-down text-muted px-2"></i>
		</a>
		<div uk-dropdown="mode: click" class="uk-border-rounded" id="msgDrId${msg.result.id}">
			<ul class="uk-nav uk-dropdown-nav">
				${msg.result.sender == user.id ?
                            `<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowEditMessage(${msg.result.Id})">Edit</a></li>
				<hr class="m-0">
				<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowDeleteMessage(${msg.result.Id})">Delete</a></li>
				<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowDeleteMessage(${msg.result.Id})">Forward</a></li>
                `
                            :
                            `<li class="uk-active"><a style="font-weight: bold;" class="text-decoration-none" onclick="ShowDeleteMessage(${msg.result.Id})">Forward</a></li>`
                        }
			</ul>
		</div>
		</div>
		<a href="#">
        ${chat.type == 9 ? msg.result.senderUserName : ""}
        </a>
		<div class="d-flex flex-row">
        ${msg.result.fileApi != null ?
                            '<a download href="' + msg.result.fileApi + '">DownloadFile</a>' : ""
                        }
			<div class="body m-1 mr-2">${msg.result.body}</div>
			<div class="time ml-auto small text-right flex-shrink-0 align-self-end text-muted" style="width:75px;">
				${msg.result.time.split(' ')[0]}
				${msg.result.sender === user.id ? sendStatus : ""}
			</div>
		</div>
	</div>
    <div>
    </div>
        </div>
	` + LoadOtherMessagesHtml;

                    let msgDate = mDate(msg.result.time).getDate();
                    if (lastDate != msgDate) {
                        LoadOtherMessagesHtml = `
	                     <div class="mx-auto my-2 bg-primary text-white small py-1 px-2 rounded">
		                   ${msgDate}
	                    </div>
	                    ` + LoadOtherMessagesHtml;
                        lastDate = msgDate;
                    }
                    if (!isMessagesEnd)
                        isMessagesEnd = msg.result.isEnd
                },
                complete: () => {
                    element.id = null
                    element.innerHTML = LoadOtherMessagesHtml
                    LoadOtherMessagesHtml = ""
                    if (!isMessagesEnd) {
                        DOM.messages.innerHTML = `<div id="lazy-div"></div>` + DOM.messages.innerHTML
                        startRow += 12;

                    }
                },
                error: (err) => {
                    LoadOtherMessagesHtml = ""
                },
            });
        LoadOtherMessagesHtml = ""
    }
}
function LoadCurrentMessages() {
    chatConnection.stream("GetCurrentChatRoomMessages", startRow)
        .subscribe({
            next: (item) => {
                LoadMessageToMessageArea(item.result)
                isMessagesEnd = item.resul.isEnd
            },
            complete: () => {
                DOM.messages.scrollTop = 15;
                DOM.messages.innerHTML = `<div id="lazy-div"></div>` + DOM.messages.innerHTML
                if (!isMessagesEnd)
                    startRow += 12;
            },
            error: (err) => {
            },
        });

}
async function RecieveMessage(message) {

    var messageDto = message.result;

    if (chat != null) {
        if (chat.id == messageDto.recieverChatRoomId) {
            addMessageToMessageArea(messageDto)
            UpdateCurrentChatListItem(messageDto)
            startRow++;
            await chatConnection.invoke("SetMessageRead", chat.id);
        }
    }
}

async function RecieveNotification(notif) {
    var notifDto = notif.result;

    //ChatList Item Proccess
    UpdateCurrentChatListItem(notifDto, true)

    //Show Notif
    ShowNotification(notifDto)
}

function SetAllMessagesRead() {
    $('.far').addClass('fas')
    $('.far').removeClass('far')
}



async function SendMessageToHub(messagebody) {
    var messageDto = await chatConnection.invoke("SendMessage", messagebody).then(function (response) {
        return response.result
    });

    UpdateCurrentChatListItem(messageDto)
    return messageDto;
}
function UpdateCurrentChatListItem(messageDto, isNotif = false) {

    if (isNotif) {
        var readCountElement = document.getElementById(`ChRead-count${messageDto.recieverChatRoomId}`)

        var readCount = new Number(readCountElement.innerText) + 1;

        readCountElement.innerText = readCount

        document.getElementById(`ChRead-count${messageDto.recieverChatRoomId}`).removeAttribute("hidden")
    }

    document.getElementById(`ChLastMessage${messageDto.recieverChatRoomId}`).innerHTML = formatLength(messageDto.body, 10);
    document.getElementById(`ChTime${messageDto.recieverChatRoomId}`).innerText = messageDto.time;
    var item = document.getElementById(`Ch${messageDto.recieverChatRoomId}`).outerHTML

    document.getElementById(`Ch${messageDto.recieverChatRoomId}`).remove()

    var AllChatRooms = DOM.chatList.innerHTML
    DOM.chatList.innerHTML = item + AllChatRooms
}
function chatOnClosed() {
    StyleDisconnected()
    setTimeout(StartChatConnection, 5000)
}

chatConnection.onclose(chatOnClosed)

chatConnection.on("GetCurrentChatRoom", setCurrentChatRoom)
chatConnection.on("SetUserInfo", SetUserInfo)
chatConnection.on("RecieveMessage", RecieveMessage)
chatConnection.on("RecieveNotification", RecieveNotification)
chatConnection.on("SetAllMessagesRead", SetAllMessagesRead)