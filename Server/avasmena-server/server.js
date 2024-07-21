const express = require('express');
const bodyParser = require('body-parser');
const https = require('https');
const fs = require('fs');
const path = require('path');
const socketIo = require('socket.io');
const db = require('./database'); // Импортируем модуль базы данных
const userRoutes = require('./routes/users');
const settingsRoutes = require('./routes/settings');
const banRoutes = require('./routes/ban');

const app = express();
const PORT = 3000;

// Пути к сертификатам и ключам
const keyPath = path.join(__dirname, 'certs', 'server.key');
const certPath = path.join(__dirname, 'certs', 'server.crt');
const caPath = path.join(__dirname, 'certs', 'server.crt');

// Настройки HTTPS
const options = {
    key: fs.readFileSync(keyPath),
    cert: fs.readFileSync(certPath),
    ca: fs.readFileSync(caPath),
    requestCert: true,
    rejectUnauthorized: true,
};

app.use(bodyParser.json());

// Обслуживание статических файлов
app.use(express.static(path.join(__dirname, 'public')));

// Использование маршрутов
app.use('/api/users', userRoutes);
app.use('/api/settings', settingsRoutes);
app.use('/api/ban', banRoutes);

// Создание HTTPS сервера и инициализация socket.io
const server = https.createServer(options, app);
const io = socketIo(server);

// Инициализация socket.io
io.on('connection', (socket) => {
    console.log('a user connected');

    // Отправка данных при подключении
    sendInitialData(socket);

    // Обработчики событий от клиента
    socket.on('addUser', (user) => handleAddUser(socket, user));
    socket.on('updateUser', (update) => handleUpdateUser(socket, update));
    socket.on('deleteUser', (name) => handleDeleteUser(socket, name));
    socket.on('banUser', (name) => handleBanUser(socket, name));
    socket.on('unbanUser', (name) => handleUnbanUser(socket, name));
    socket.on('updateSettings', (settings) => handleUpdateSettings(socket, settings));
    socket.on('getUser', (name, callback) => handleGetUser(socket, name, callback));

    socket.on('disconnect', () => {
        console.log('user disconnected');
    });
});

// Обработчики событий
function sendInitialData(socket) {
    db.all("SELECT * FROM Users", (err, users) => {
        if (err) {
            socket.emit('error', err.message);
        } else {
            console.log('Sending initial users:', users);
            socket.emit('initialUsers', users);
        }
    });

    db.all("SELECT * FROM BAN", (err, bannedUsers) => {
        if (err) {
            socket.emit('error', err.message);
        } else {
            console.log('Sending initial banned users:', bannedUsers);
            socket.emit('initialBannedUsers', bannedUsers);
        }
    });

    db.get("SELECT * FROM Settings", (err, settings) => {
        if (err) {
            socket.emit('error', err.message);
        } else {
            console.log('Sending initial settings:', settings);
            socket.emit('initialSettings', settings);
        }
    });
}

function handleAddUser(socket, user) {
    const { name, id, count } = user;
    db.run("INSERT INTO Users (name, id, count) VALUES (?, ?, ?)", [name, id, count], function(err) {
        if (err) {
            socket.emit('error', err.message);
        } else {
            io.emit('userAdded', user);
        }
    });
}

function handleUpdateUser(socket, update) {
    const { originalName, field, value } = update;
    db.run(`UPDATE Users SET ${field} = ? WHERE name = ?`, [value, originalName], function(err) {
        if (err) {
            socket.emit('error', err.message);
        } else {
            io.emit('userUpdated', { originalName, field, value });
        }
    });
}

function handleDeleteUser(socket, name) {
    db.run("DELETE FROM Users WHERE name = ?", name, function(err) {
        if (err) {
            socket.emit('error', err.message);
        } else {
            io.emit('userDeleted', name);
        }
    });
}

function handleBanUser(socket, name) {
    db.get("SELECT * FROM Users WHERE name = ?", [name], (err, row) => {
        if (err) {
            socket.emit('error', err.message);
        } else if (row) {
            db.run("INSERT INTO BAN (name, id, count) VALUES (?, ?, ?)", [row.name, row.id, row.count], function(err) {
                if (err) {
                    socket.emit('error', err.message);
                } else {
                    db.run("DELETE FROM Users WHERE name = ?", [name], function(err) {
                        if (err) {
                            socket.emit('error', err.message);
                        } else {
                            io.emit('userBanned', row);
                        }
                    });
                }
            });
        } else {
            socket.emit('error', 'User not found');
        }
    });
}

function handleUnbanUser(socket, name) {
    db.get("SELECT * FROM BAN WHERE name = ?", [name], (err, row) => {
        if (err) {
            socket.emit('error', err.message);
        } else if (row) {
            db.run("INSERT INTO Users (name, id, count) VALUES (?, ?, ?)", [row.name, row.id, row.count], function(err) {
                if (err) {
                    socket.emit('error', err.message);
                } else {
                    db.run("DELETE FROM BAN WHERE name = ?", [name], function(err) {
                        if (err) {
                            socket.emit('error', err.message);
                        } else {
                            io.emit('userUnbanned', row);
                        }
                    });
                }
            });
        } else {
            socket.emit('error', 'User not found');
        }
    });
}

function handleUpdateSettings(socket, settings) {
    const { tokenBot, forwardChat, chatId } = settings;
    db.run("UPDATE Settings SET tokenBot = ?, forwardChat = ?, chatId = ?", [tokenBot, forwardChat, chatId], function(err) {
        if (err) {
            socket.emit('error', err.message);
        } else {
            io.emit('settingsUpdated', settings);
        }
    });
}

function handleGetUser(socket, name, callback) {
    db.get("SELECT * FROM Users WHERE name = ?", [name], (err, row) => {
        if (err) {
            callback(null);
        } else {
            callback(row);
        }
    });
}

// Обслуживание главной страницы
app.get('/', (req, res) => {
    if (!req.client.authorized) {
        return res.status(401).send('Client certificate required');
    }
    res.sendFile(path.join(__dirname, 'public', 'index.html'));
});

// Запуск HTTPS сервера
server.listen(PORT, () => {
    console.log(`HTTPS сервер запущен на порту ${PORT}`);
});