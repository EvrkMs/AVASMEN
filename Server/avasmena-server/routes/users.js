const express = require('express');
const router = express.Router();
const db = require('../database');

router.get('/', (req, res) => {
    db.all("SELECT * FROM Users", (err, users) => {
        if (err) {
            res.status(500).send(err.message);
        } else {
            res.json(users);
        }
    });
});

router.post('/', (req, res) => {
    const { name, id, count } = req.body;
    db.run("INSERT INTO Users (name, id, count) VALUES (?, ?, ?)", [name, id, count], function(err) {
        if (err) {
            res.status(500).send(err.message);
        } else {
            res.status(201).send('User added');
        }
    });
});

router.put('/:name', (req, res) => {
    const { name } = req.params;
    const { field, value } = req.body;
    db.run(`UPDATE Users SET ${field} = ? WHERE name = ?`, [value, name], function(err) {
        if (err) {
            res.status(500).send(err.message);
        } else {
            res.send('User updated');
        }
    });
});

router.delete('/:name', (req, res) => {
    const { name } = req.params;
    db.run("DELETE FROM Users WHERE name = ?", name, function(err) {
        if (err) {
            res.status(500).send(err.message);
        } else {
            res.send('User deleted');
        }
    });
});

module.exports = router;