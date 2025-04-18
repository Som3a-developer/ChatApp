// Initialize SignalR Connection
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

// DOM Elements
const messagesList = document.getElementById("messages-list");
const messageInput = document.getElementById("message-input");
const sendButton = document.getElementById("send-button");
const usersList = document.getElementById("users-list");
const recipientName = document.getElementById("recipient-name");

let currentRecipient = null; // For private messages

// Start Connection
async function startConnection() {
    try {
        await connection.start();
        console.log("SignalR Connected.");

        // Register current user
        await connection.invoke("RegisterUser", currentUser);

    } catch (err) {
        console.log("Connection error: ", err);
        setTimeout(startConnection, 5000);
    }
}

// Connection Events
connection.onclose(async () => {
    await startConnection();
});

// SignalR Hub Methods
connection.on("ReceiveMessage", (sender, message) => {
    addMessage(sender, message, false);
});

connection.on("UpdateUsersList", (users) => {
    updateUsersList(users);
});

connection.on("PrivateMessage", (sender, message) => {
    addMessage(sender, message, false, true);
});

// UI Functions
function addMessage(sender, message, isSent, isPrivate = false) {
    const messageElement = document.createElement("div");
    messageElement.classList.add("message");
    messageElement.classList.add(isSent ? "sent" : "received");

    if (isPrivate) {
        messageElement.innerHTML = `<strong>${sender} (private):</strong> ${message}`;
    } else {
        messageElement.innerHTML = `<strong>${sender}:</strong> ${message}`;
    }

    messagesList.appendChild(messageElement);
    messagesList.scrollTop = messagesList.scrollHeight;
}

function updateUsersList(users) {
    usersList.innerHTML = '';
    users.forEach(user => {
        if (user !== currentUser) {
            const li = document.createElement("li");
            li.textContent = user;
            li.onclick = () => selectUser(user);
            usersList.appendChild(li);
        }
    });
}

function selectUser(user) {
    currentRecipient = user;
    recipientName.textContent = user;
    messagesList.innerHTML = '';
    // Load previous messages with this user
    connection.invoke("LoadMessages", user).catch(err => console.error(err));
}

// Event Listeners
sendButton.addEventListener("click", async () => {
    const message = messageInput.value.trim();
    if (message) {
        if (currentRecipient) {
            // Private message
            await connection.invoke("SendPrivateMessage", currentRecipient, message);
            addMessage(currentUser, message, true, true);
        } else {
            // Broadcast message
            await connection.invoke("SendMessageToAll", message);
            addMessage(currentUser, message, true);
        }
        messageInput.value = '';
    }
});

messageInput.addEventListener("keypress", (e) => {
    if (e.key === "Enter") {
        sendButton.click();
    }
});

// Initialize
startConnection();  