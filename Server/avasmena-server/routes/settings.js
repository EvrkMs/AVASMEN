const express = require('express');
const router = express.Router();
const db = require('../database');

router.get('/', (req, res) => {
    db.get("SELECT * FROM Settings", (err, settings) => {
        if (err) {
            res.status(500).send(err.message);
        } else {
            res.json(settings);
        }
    });
});

router.put('/', (req, res) => {
    const { tokenBot, forwardChat, chatId } = req.body;
    db.run("UPDATE Settings SET tokenBot = ?, forwardChat = ?, chatId = ?", [tokenBot, forwardChat, chatId], function(err) {
        if (err) {
            res.status(500).send(err.message);
        } else {
            res.send('Settings updated');
        }
    });
});

module.exports = router;