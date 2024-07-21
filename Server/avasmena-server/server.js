const express = require('express');
const bodyParser = require('body-parser');
const db = require('./database');
const https = require('https');
const fs = require('fs');
const path = require('path');
const socketIo = require('socket.io');

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

// Создание HTTPS сервера и инициализация socket.io
const server = https.createServer(options, app);
const io = socketIo(server);

// Получение всех пользователей
app.get('/api/users', (req, res) => {
  db.all("SELECT * FROM Users", (err, rows) => {
    if (err) {
      res.status(500).send(err.message);
    } else {
      res.json(rows);
    }
  });
});

// Получение всех забаненных пользователей
app.get('/api/ban', (req, res) => {
  db.all("SELECT * FROM BAN", (err, rows) => {
    if (err) {
      res.status(500).send(err.message);
    } else {
      res.json(rows);
    }
  });
});

// Получение настроек
app.get('/api/settings', (req, res) => {
  db.get("SELECT * FROM Settings", (err, row) => {
    if (err) {
      res.status(500).send(err.message);
    } else {
      res.json(row);
    }
  });
});

io.on('connection', (socket) => {
  console.log('a user connected');

  socket.on('addUser', ({ name, id, count }) => {
    db.run("INSERT INTO Users (name, id, count) VALUES (?, ?, ?)", 
      [name, id, count], function(err) {
      if (err) {
        socket.emit('error', err.message);
      } else {
        io.emit('userAdded', { name, id, count });
      }
    });
  });

  socket.on('updateUser', ({ originalName, field, value }) => {
    db.run(`UPDATE Users SET ${field} = ? WHERE name = ?`, [value, originalName], function(err) {
      if (err) {
        socket.emit('error', err.message);
      } else {
        io.emit('userUpdated', { originalName, field, value });
      }
    });
  });

  socket.on('deleteUser', (name) => {
    db.run("DELETE FROM Users WHERE name = ?", name, function(err) {
      if (err) {
        socket.emit('error', err.message);
      } else {
        io.emit('userDeleted', name);
      }
    });
  });

  socket.on('banUser', (name) => {
    db.get("SELECT * FROM Users WHERE name = ?", [name], (err, row) => {
      if (err) {
        socket.emit('error', err.message);
      } else if (row) {
        db.run("INSERT INTO BAN (name, id, count) VALUES (?, ?, ?)", 
          [row.name, row.id, row.count], function(err) {
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
  });

  socket.on('unbanUser', (name) => {
    db.get("SELECT * FROM BAN WHERE name = ?", [name], (err, row) => {
      if (err) {
        socket.emit('error', err.message);
      } else if (row) {
        db.run("INSERT INTO Users (name, id, count) VALUES (?, ?, ?)", 
          [row.name, row.id, row.count], function(err) {
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
  });

  socket.on('updateSettings', ({ tokenBot, forwardChat, chatId }) => {
    db.run("UPDATE Settings SET tokenBot = ?, forwardChat = ?, chatId = ?",
      [tokenBot, forwardChat, chatId], function(err) {
        if (err) {
          socket.emit('error', err.message);
        } else {
          io.emit('settingsUpdated', { tokenBot, forwardChat, chatId });
        }
      });
  });

  socket.on('disconnect', () => {
    console.log('user disconnected');
  });
});

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