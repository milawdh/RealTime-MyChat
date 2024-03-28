function ShowProfileInfo(Id, Type) {
    UIkit.modal(document.getElementById("ProfileModal")).show();
}
function ShowMyContacts() {
    UIkit.modal(document.getElementById("MyContactsModal")).show();
}
function ShowAddGroup() {
    UIkit.modal(document.getElementById("AddGroupModal")).show();
}
function SetSelectedContact(parentElement) {
    var image = parentElement.children[0];
    image.style.transition = "0.2s all ease";

    if (image.classList.contains("_selectedContact")) {
        image.classList.remove("_selectedContact");
    } else {
        image.classList.add("_selectedContact");
    }
}
function formatLength(src, length) {
    debugger
    var isLong = false;
    var res = "";
    src = src.split('');

    if (src.length > length) {
        isLong = true
    }
    else {
        length = src.length
    }

    for (var i = 0; i < length; i++) {
        res += src[i]
    }

    if (isLong) {
        res += " ...";
    }

    return res;
}
function ShowNotification(message) {
    UIkit.notification({
        message: `
        <div dir="rtl" onclick="generateMessageArea(this, '${message.id}',${message.type})" class="d-flex justify-content-end">
            <span class="_MyFont mx-1">${message.senderUserName}</span>
            <img class="uk-border-circle" src="${message.senderProfileImage}" width="40"/>
        </div>
        <div>
        ${formatLength(message.body, 10)}
        </div>
        `,
        status: 'primary',
        timeout: 4000,
        pos: 'top-right'
    })
}
function ShowErrors(errors) {
    for (var i = 0; i < errors.length; i++) {
        UIkit.notification({
            message: `<span class="_MyFont">${errors[i]}</span>`,
            status: 'danger',
            timeout: 7000,
            pos: 'top-right'
        })
    }
}
function SetSuccess() {
    UIkit.notification({
        message: `<span class="_MyFont">Succeed!</span>`,
        status: 'success',
        timeout: 2000,
        pos: 'top-right'
    })
    setTimeout(function () {
        window.location.reload()
    }, 2000)
}
function refreshModals() {
    var AddModal = document.getElementById('AddModal')
    var EditModal = document.getElementById('EditModal')
    if (EditModal != null) {
        EditModal.remove()
    }
    if (AddModal != null) {
        AddModal.remove()
    }
}