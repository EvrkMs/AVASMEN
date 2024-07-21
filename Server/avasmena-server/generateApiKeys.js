const crypto = require('crypto');

function generateApiKey() {
    return crypto.randomBytes(32).toString('hex');
}

const readApiKey = generateApiKey();
const writeApiKey = generateApiKey();

console.log('API_KEY_READ:', readApiKey);
console.log('API_KEY_WRITE:', writeApiKey);