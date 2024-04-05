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

async function RecieveMessage(message) {

    var messageDto = message.result;

    if (chat != null) {
        if (chat.id == messageDto.recieverChatRoomId) {
            addMessageToMessageArea(messageDto)
            UpdateCurrentChatListItem(messageDto)
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


async function SendMessageToHub(messagebody) {
    var messageDto = await chatConnection.invoke("SendMessage", messagebody).then(function (response) {
        return response.result
    });

    UpdateCurrentChatListItem(messageDto)
    return messageDto;
}
function UpdateCurrentChatListItem(messageDto, isNotif = false) {

    if (isNotif) {
        var readCountElement = document.getElementById(`ChRead-count${notifDto.recieverChatRoomId}`)

        var readCount = new Number(readCountElement.innerText) + 1;

        readCountElement.innerText = readCount

        document.getElementById(`ChReadDiv${notifDto.recieverChatRoomId}`).removeAttribute("hidden")
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