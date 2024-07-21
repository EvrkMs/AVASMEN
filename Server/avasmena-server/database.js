const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database('database.db'); // Для реального использования

db.serialize(() => {
  db.run(`CREATE TABLE IF NOT EXISTS Users (
    name TEXT PRIMARY KEY,
    id INTEGER,
    count INTEGER
  )`);

  db.run(`CREATE TABLE IF NOT EXISTS Settings (
    tokenBot TEXT,
    forwardChat INTEGER,
    chatId INTEGER
  )`);

  db.run(`CREATE TABLE IF NOT EXISTS BAN (
    name TEXT PRIMARY KEY,
    id INTEGER,
    count INTEGER
  )`);
});

module.exports = db;