var chatConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .withStatefulReconnect({ bufferSize: 1000 })
    .build();

StyleDisconnected()
StartChatConnection()


function StartChatConnection() {

    chatConnection.start().then(function () {
        StyleConnected()
    }).catch(function (err) {
        return console.error(err.toString());
    });
}

chatConnection.on("GetCurrentChatRoom", setCurrentChatRoom)

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

async function getChatRoomDetials(charoomId) {

    return await chatConnection.invoke("GetChatRoomDetials", charoomId).then(function (res) { return res.result; });
}

async function RecieveMessage(message) {
    debugger

    var messageDto = message.result;
    document.getElementById("ChTime" + messageDto.recieverChatRoomId).innerText = messageDto.time
    document.getElementById("ChLastMessage" + messageDto.recieverChatRoomId).innerText = formatLength(messageDto.body, 12);

    var chatRoomView = document.getElementById("Ch" + messageDto.recieverChatRoomId)
    document.getElementById("Ch" + messageDto.recieverChatRoomId).remove()

    var AllChatRooms = DOM.chatList.innerHTML

    DOM.chatList.innerHTML = CreateChatListItem(messageDto.senderChatRoom) + AllChatRooms

    if (chat != null) {
        if (chat.id == messageDto.recieverChatRoomId) {
            addMessageToMessageArea(messageDto)
            return;
        }
    }
    ShowNotification(messageDto);
}

async function SendMessageToHub(messagebody) {
    return await chatConnection.invoke("SendMessage", messagebody).then(function (response) {
        return response.result
    });
}


chatConnection.onclose(function () {
    StyleDisconnected()
    setTimeout(StartChatConnection, 5000)
})

chatConnection.on("SetUserInfo", SetUserInfo)
chatConnection.on("RecieveMessage", RecieveMessage)