document.addEventListener('DOMContentLoaded', () => {
    const socket = io();

    const usersTable = document.getElementById('usersTable').querySelector('tbody');
    const addUserForm = document.getElementById('addUserForm');
    const banTable = document.getElementById('banTable').querySelector('tbody');

    const tokenBotInput = document.getElementById('tokenBot');
    const forwardChatInput = document.getElementById('forwardChat');
    const chatIdInput = document.getElementById('chatId');
    const editTokenBotButton = document.getElementById('editTokenBotButton');
    const saveTokenBotButton = document.getElementById('saveTokenBotButton');
    const cancelTokenBotButton = document.getElementById('cancelTokenBotButton');
    const editForwardChatButton = document.getElementById('editForwardChatButton');
    const saveForwardChatButton = document.getElementById('saveForwardChatButton');
    const cancelForwardChatButton = document.getElementById('cancelForwardChatButton');
    const editChatIdButton = document.getElementById('editChatIdButton');
    const saveChatIdButton = document.getElementById('saveChatIdButton');
    const cancelChatIdButton = document.getElementById('cancelChatIdButton');

    let originalSettings = {};

    // Инициализация данных при подключении
    socket.on('initialUsers', (users) => {
        console.log('Received initial users:', users);
        usersTable.innerHTML = ''; // Очистка таблицы перед заполнением
        users.forEach(user => {
            addUserRow(user);
        });
    });

    socket.on('initialBannedUsers', (users) => {
        console.log('Received initial banned users:', users);
        banTable.innerHTML = ''; // Очистка таблицы перед заполнением
        users.forEach(user => {
            addBannedUserRow(user);
        });
    });

    socket.on('initialSettings', (settings) => {
        console.log('Received initial settings:', settings);
        tokenBotInput.value = settings.tokenBot;
        forwardChatInput.value = settings.forwardChat;
        chatIdInput.value = settings.chatId;
        originalSettings = { ...settings };
    });

    // Обработчики событий от сервера
    socket.on('userAdded', (user) => {
        console.log('User added:', user);
        addUserRow(user);
    });

    socket.on('userUpdated', ({ originalName, field, value }) => {
        console.log('User updated:', { originalName, field, value });
        updateUserRow(originalName, field, value);
    });

    socket.on('userDeleted', (name) => {
        console.log('User deleted:', name);
        removeUserRow(name);
    });

    socket.on('userBanned', (user) => {
        console.log('User banned:', user);
        removeUserRow(user.name);
        addBannedUserRow(user);
    });

    socket.on('userUnbanned', (user) => {
        console.log('User unbanned:', user);
        removeBannedUserRow(user.name);
        addUserRow(user);
    });

    socket.on('settingsUpdated', (settings) => {
        alert('Settings updated');
        tokenBotInput.value = settings.tokenBot;
        forwardChatInput.value = settings.forwardChat;
        chatIdInput.value = settings.chatId;
        originalSettings = { ...settings };
        toggleSettingsEditMode(false, 'all');
    });

    socket.on('error', (message) => {
        alert('Error: ' + message);
    });

    // Добавление пользователя
    addUserForm.addEventListener('submit', (e) => {
        e.preventDefault();
        const formData = new FormData(addUserForm);
        const name = formData.get('name');
        const id = formData.get('id');
        const count = formData.get('count');

        socket.emit('addUser', { name, id, count });
        addUserForm.reset();
    });

    // Обновление настроек
    editTokenBotButton.addEventListener('click', () => {
        toggleSettingsEditMode(true, 'tokenBot');
    });

    saveTokenBotButton.addEventListener('click', () => {
        const tokenBot = tokenBotInput.value;
        socket.emit('updateSettings', { ...originalSettings, tokenBot });
    });

    cancelTokenBotButton.addEventListener('click', () => {
        tokenBotInput.value = originalSettings.tokenBot;
        toggleSettingsEditMode(false, 'tokenBot');
    });

    editForwardChatButton.addEventListener('click', () => {
        toggleSettingsEditMode(true, 'forwardChat');
    });

    saveForwardChatButton.addEventListener('click', () => {
        const forwardChat = forwardChatInput.value;
        socket.emit('updateSettings', { ...originalSettings, forwardChat });
    });

    cancelForwardChatButton.addEventListener('click', () => {
        forwardChatInput.value = originalSettings.forwardChat;
        toggleSettingsEditMode(false, 'forwardChat');
    });

    editChatIdButton.addEventListener('click', () => {
        toggleSettingsEditMode(true, 'chatId');
    });

    saveChatIdButton.addEventListener('click', () => {
        const chatId = chatIdInput.value;
        socket.emit('updateSettings', { ...originalSettings, chatId });
    });

    cancelChatIdButton.addEventListener('click', () => {
        chatIdInput.value = originalSettings.chatId;
        toggleSettingsEditMode(false, 'chatId');
    });

    function toggleSettingsEditMode(isEditMode, field) {
        if (field === 'tokenBot' || field === 'all') {
            tokenBotInput.disabled = !isEditMode;
            editTokenBotButton.style.display = isEditMode ? 'none' : 'inline';
            saveTokenBotButton.style.display = isEditMode ? 'inline' : 'none';
            cancelTokenBotButton.style.display = isEditMode ? 'inline' : 'none';
        }
        if (field === 'forwardChat' || field === 'all') {
            forwardChatInput.disabled = !isEditMode;
            editForwardChatButton.style.display = isEditMode ? 'none' : 'inline';
            saveForwardChatButton.style.display = isEditMode ? 'inline' : 'none';
            cancelForwardChatButton.style.display = isEditMode ? 'inline' : 'none';
        }
        if (field === 'chatId' || field === 'all') {
            chatIdInput.disabled = !isEditMode;
            editChatIdButton.style.display = isEditMode ? 'none' : 'inline';
            saveChatIdButton.style.display = isEditMode ? 'inline' : 'none';
            cancelChatIdButton.style.display = isEditMode ? 'inline' : 'none';
        }
    }

    window.updateUser = (originalName, field, value) => {
        socket.emit('updateUser', { originalName, field, value });
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
            <td class="editable">${user.name}</td>
            <td class="editable">${user.id}</td>
            <td class="editable">${user.count}</td>
            <td>
                <button class="edit-btn" onclick="editUser('${user.name}')">✏️</button>
                <button class="ban-btn" onclick="banUser('${user.name}')">BAN</button>
                <button class="save-btn" style="display:none;" onclick="saveUser('${user.name}')">✔️</button>
                <button class="cancel-btn" style="display:none;" onclick="cancelEditUser('${user.name}')">❌</button>
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
        const rows = document.querySelectorAll(`tr`);
        rows.forEach(row => {
            const cells = row.querySelectorAll('td');
            cells.forEach(cell => {
                if (cell.innerText === originalName && cell.classList.contains('editable')) {
                    const index = Array.from(cells).indexOf(cell);
                    if (field === 'name' && index === 0) cell.innerText = value;
                    if (field === 'id' && index === 1) cell.innerText = value;
                    if (field === 'count' && index === 2) cell.innerText = value;
                }
            });
        });
    }

    function removeUserRow(name) {
        const rows = document.querySelectorAll(`tr`);
        rows.forEach(row => {
            const cells = row.querySelectorAll('td');
            cells.forEach(cell => {
                if (cell.innerText === name) {
                    row.remove();
                }
            });
        });
    }

    function removeBannedUserRow(name) {
        const rows = document.querySelectorAll(`tr`);
        rows.forEach(row => {
            const cells = row.querySelectorAll('td');
            cells.forEach(cell => {
                if (cell.innerText === name) {
                    row.remove();
                }
            });
        });
    }

    window.editUser = (name) => {
        const row = findRowByName(name);
        if (row) {
            const cells = row.querySelectorAll('td.editable');
            cells.forEach(cell => {
                cell.setAttribute('contenteditable', 'true');
                cell.focus();
            });
            const actionCell = row.querySelector('td:last-child');
            actionCell.querySelector('.edit-btn').style.display = 'none';
            actionCell.querySelector('.ban-btn').style.display = 'none';
            actionCell.querySelector('.save-btn').style.display = 'inline';
            actionCell.querySelector('.cancel-btn').style.display = 'inline';
        }
    };

    window.saveUser = (originalName) => {
        const row = findRowByName(originalName);
        const cells = row.querySelectorAll('td.editable');
        const name = cells[0].innerText;
        const id = cells[1].innerText;
        const count = cells[2].innerText;
        socket.emit('updateUser', { originalName, field: 'name', value: name });
        socket.emit('updateUser', { originalName, field: 'id', value: id });
        socket.emit('updateUser', { originalName, field: 'count', value: count });
        cells.forEach(cell => cell.removeAttribute('contenteditable'));
        const actionCell = row.querySelector('td:last-child');
        actionCell.querySelector('.edit-btn').style.display = 'inline';
        actionCell.querySelector('.ban-btn').style.display = 'inline';
        actionCell.querySelector('.save-btn').style.display = 'none';
        actionCell.querySelector('.cancel-btn').style.display = 'none';
    };

    window.cancelEditUser = (originalName) => {
        const row = findRowByName(originalName);
        const cells = row.querySelectorAll('td.editable');
        socket.emit('getUser', originalName, (user) => {
            cells[0].innerText = user.name;
            cells[1].innerText = user.id;
            cells[2].innerText = user.count;
            cells.forEach(cell => cell.removeAttribute('contenteditable'));
            const actionCell = row.querySelector('td:last-child');
            actionCell.querySelector('.edit-btn').style.display = 'inline';
            actionCell.querySelector('.ban-btn').style.display = 'inline';
            actionCell.querySelector('.save-btn').style.display = 'none';
            actionCell.querySelector('.cancel-btn').style.display = 'none';
        });
    };

    function findRowByName(name) {
        const rows = document.querySelectorAll('tr');
        let foundRow = null;
        rows.forEach(row => {
            const cells = row.querySelectorAll('td');
            if (cells.length > 0 && cells[0].innerText === name) {
                foundRow = row;
            }
        });
        return foundRow;
    }
});