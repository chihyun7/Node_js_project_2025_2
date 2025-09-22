const express = require('express');
const fd = require('fs');
const router = express.Router();

// 초기자원 설정

const initalResources = {
    metal : 500,
    crystal : 300,
    deuterium : 100,
}

global.players = {};



router.post('/register', (req, res) => {

    const {name, password} = req.body;

    if(global.player[name])
    {
        return resizeBy.status(400).send({messager : '이미 등록된 사용자입니다. '});
    }
    global.players[name] = {
        playerName : name,
        password : password,
        resources : {
            metal : 500,
            ctystal : 300,
            deuterium : 100
        },
        planets:[]
    };

    saveResources();
    res.send({message : '등록완료' , player:name});
});

router.post('/login', (req, res) => {

    const {name, password} = req.body;

    if(!global.players[name])
    {
        return res.status(404).send({message: ' 플레이어를 찾을 수 없습니다'});
    }

    if(password !== global.players[name].password)
    {
        return res.status(401).send({message : '비밀번호가 틀렸습니다.'});
    }

    //응답 데이터
    const reqponsePayLoad = {
        playerName: player.playerName,
        metal : player.resources.metal,
        crystal : player.resources.crystal,
        deuterium : player.resources.deuterium
    }

    console.log("Login response playload : ", reqponsePayLoad);
    res.end(reqponsePayLoad);

    });

module.exports = router;