document.addEventListener('DOMContentLoaded', () => {
    const socket = io();

    const usersTable = document.getElementById('usersTable').querySelector('tbody');
    const addUserForm = document.getElementById('addUserForm');
    const updateSettingsForm = document.getElementById('updateSettingsForm');
    const banTable = document.getElementById('banTable').querySelector('tbody');

    // Fetch and display users
    fetch('/api/users')
        .then(response => response.json())
        .then(users => {
            users.forEach(user => {
                addUserRow(user);
            });
        });

    // Fetch and display banned users
    fetch('/api/ban')
        .then(response => response.json())
        .then(users => {
            users.forEach(user => {
                addBannedUserRow(user);
            });
        });

    // Fetch and display settings
    fetch('/api/settings')
        .then(response => response.json())
        .then(settings => {
            document.getElementById('tokenBot').value = settings.tokenBot;
            document.getElementById('forwardChat').value = settings.forwardChat;
            document.getElementById('chatId').value = settings.chatId;
        });

    // Add user
    addUserForm.addEventListener('submit', (e) => {
        e.preventDefault();
        const formData = new FormData(addUserForm);
        const name = formData.get('name');
        const id = formData.get('id');
        const count = formData.get('count');

        socket.emit('addUser', { name, id, count });
        addUserForm.reset();
    });

    // Update settings
    updateSettingsForm.addEventListener('submit', (e) => {
        e.preventDefault();
        const formData = new FormData(updateSettingsForm);
        const tokenBot = formData.get('tokenBot');
        const forwardChat = formData.get('forwardChat');
        const chatId = formData.get('chatId');

        socket.emit('updateSettings', { tokenBot, forwardChat, chatId });
    });

    socket.on('userAdded', (user) => {
        addUserRow(user);
    });

    socket.on('userUpdated', ({ originalName, field, value }) => {
        updateUserRow(originalName, field, value);
    });

    socket.on('userDeleted', (name) => {
        removeUserRow(name);
    });

    socket.on('userBanned', (user) => {
        addBannedUserRow(user);
        removeUserRow(user.name);
    });

    socket.on('userUnbanned', (user) => {
        addUserRow(user);
        removeBannedUserRow(user.name);
    });

    socket.on('settingsUpdated', (settings) => {
        alert('Settings updated');
    });

    socket.on('error', (message) => {
        alert('Error: ' + message);
    });

    window.updateUser = (originalName, field, value) => {
        socket.emit('updateUser', { originalName, field, value });
    };

    window.deleteUser = (name) => {
        socket.emit('deleteUser', name);
    };

    window.banUser = (name) => {
        socket.emit('banUser', name);
    };

    window.unbanUser = (name) => {
        socket.emit('unbanUser', name);
    };

    function addUserRow(user) {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td class="editable" contenteditable="true" onblur="updateUser('${user.name}', 'name', this.innerText)">${user.name}</td>
            <td class="editable" contenteditable="true" onblur="updateUser('${user.name}', 'id', this.innerText)">${user.id}</td>
            <td class="editable" contenteditable="true" onblur="updateUser('${user.name}', 'count', this.innerText)">${user.count}</td>
            <td>
                <button onclick="deleteUser('${user.name}')">Delete</button>
                <button onclick="banUser('${user.name}')">BAN</button>
            </td>
        `;
        usersTable.appendChild(row);
    }

    function addBannedUserRow(user) {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${user.name}</td>
            <td>${user.id}</td>
            <td>${user.count}</td>
            <td>
                <button onclick="unbanUser('${user.name}')">unBAN</button>
            </td>
        `;
        banTable.appendChild(row);
    }

    function updateUserRow(originalName, field, value) {
        const row = document.querySelector(`tr:has(td:contains(${originalName}))`);
        if (row) {
            const cell = row.querySelector(`td:contains(${value})`);
            if (cell) {
                cell.innerText = value;
            }
        }
    }

    function removeUserRow(name) {
        const row = document.querySelector(`tr:has(td:contains(${name}))`);
        if (row) {
            row.remove();
        }
    }

    function removeBannedUserRow(name) {
        const row = document.querySelector(`tr:has(td:contains(${name}))`);
        if (row) {
            row.remove();
        }
    }
});