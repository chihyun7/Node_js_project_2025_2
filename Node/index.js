const express = require('express');
const fs = require('fs');       // 파일 시스템 헤더에 추가
const playerRoutes = require('./Routes/playerRoutes');
const app = express();
const port = 4000;

app.use(express.json());
app.use('/api', playerRoutes);
const resourceFilePath = 'resources.json';


loadResource();

function loadResource()
{
    if(fs.existsSync(resourceFilePath))
    {
        const data = fs.readFileSync(resourceFilePath);
        global.players = JSON.parse(data);
    }
    else
    {
        global.players = {};
    }
}


app.listen(port, ()=>
{
    console.log(`서버거 http://localhost:${port}에서 실행 중 입니다.`);
})