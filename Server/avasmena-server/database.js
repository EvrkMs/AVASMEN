const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database('database.db'); // Подключаемся к существующей базе данных

db.serialize(() => {
    db.run("CREATE TABLE IF NOT EXISTS Users (name TEXT PRIMARY KEY, id INTEGER, count INTEGER)", (err) => {
        if (err) {
            console.error('Error creating Users table:', err.message);
        } else {
            console.log('Users table created successfully');
        }
    });

    db.run("CREATE TABLE IF NOT EXISTS BAN (name TEXT PRIMARY KEY, id INTEGER, count INTEGER)", (err) => {
        if (err) {
            console.error('Error creating BAN table:', err.message);
        } else {
            console.log('BAN table created successfully');
        }
    });

    db.run("CREATE TABLE IF NOT EXISTS Settings (tokenBot TEXT, forwardChat INTEGER, chatId INTEGER)", (err) => {
        if (err) {
            console.error('Error creating Settings table:', err.message);
        } else {
            console.log('Settings table created successfully');
        }
    });

    // Инициализация таблицы Settings, если она пуста
    db.get("SELECT COUNT(*) AS count FROM Settings", (err, row) => {
        if (err) {
            console.error('Error checking Settings table:', err.message);
        } else if (row.count === 0) {
            db.run("INSERT INTO Settings (tokenBot, forwardChat, chatId) VALUES ('', 0, 0)", (err) => {
                if (err) {
                    console.error('Error inserting into Settings table:', err.message);
                } else {
                    console.log('Settings table initialized successfully');
                }
            });
        }
    });
});

module.exports = db;