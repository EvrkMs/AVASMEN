const express = require('express');
const router = express.Router();
const db = require('../database');

router.get('/', (req, res) => {
    db.all("SELECT * FROM BAN", (err, bannedUsers) => {
        if (err) {
            res.status(500).send(err.message);
        } else {
            res.json(bannedUsers);
        }
    });
});

router.post('/', (req, res) => {
    const { name, id, count } = req.body;
    db.run("INSERT INTO BAN (name, id, count) VALUES (?, ?, ?)", [name, id, count], function(err) {
        if (err) {
            res.status(500).send(err.message);
        } else {
            res.status(201).send('User banned');
        }
    });
});

router.delete('/:name', (req, res) => {
    const { name } = req.params;
    db.run("DELETE FROM BAN WHERE name = ?", name, function(err) {
        if (err) {
            res.status(500).send(err.message);
        } else {
            res.send('User unbanned');
        }
    });
});

module.exports = router;