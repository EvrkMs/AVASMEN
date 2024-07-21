const express = require('express');
const bodyParser = require('body-parser');
const fs = require('fs');
const path = require('path');
const dotenv = require('dotenv');

dotenv.config();

const app = express();
const PORT = 3001; // Используйте другой порт для API сервера

// API ключи для доступа к version.json
const API_KEY_READ = process.env.API_KEY_READ;
const API_KEY_WRITE = process.env.API_KEY_WRITE;

app.use(bodyParser.json());

// Маршрут для получения данных из version.json
app.get('/api/version', (req, res) => {
    const apiKey = req.query.apiKey;
    if (apiKey !== API_KEY_READ) {
        return res.status(401).json({ error: 'Unauthorized' });
    }

    const versionFilePath = path.join(__dirname, 'Update', 'version.json');
    fs.access(versionFilePath, fs.constants.F_OK, (err) => {
        if (err) {
            return res.status(404).json({ error: 'version.json not found' });
        }

        const versionData = fs.readFileSync(versionFilePath, 'utf8');
        return res.json(JSON.parse(versionData));
    });
});

// Корневой API для изменения данных в массиве version.json
app.put('/api/version', (req, res) => {
    const apiKey = req.query.apiKey;
    if (apiKey !== API_KEY_WRITE) {
        return res.status(401).json({ error: 'Unauthorized' });
    }

    const versionFilePath = path.join(__dirname, 'Update', 'version.json');
    const newVersionData = req.body;

    if (!newVersionData.version || !newVersionData.filename) {
        return res.status(400).json({ error: 'Invalid data format' });
    }

    fs.writeFileSync(versionFilePath, JSON.stringify(newVersionData, null, 2));
    return res.json({ message: 'Version updated successfully' });
});

// Маршрут для скачивания setup.exe из папки Update/Output
app.get('/api/download', (req, res) => {
    const apiKey = req.query.apiKey;
    if (apiKey !== API_KEY_READ) {
        return res.status(401).json({ error: 'Unauthorized' });
    }

    const setupFilePath = path.join(__dirname, 'Update', 'Output', 'setup.exe');
    if (fs.existsSync(setupFilePath)) {
        res.download(setupFilePath, 'setup.exe', (err) => {
            if (err) {
                console.error('Error downloading file:', err);
                res.status(500).json({ error: 'Error downloading file' });
            }
        });
    } else {
        return res.status(404).json({ error: 'setup.exe not found' });
    }
});

app.listen(PORT, () => {
    console.log(`API сервер запущен на порту ${PORT}`);
});