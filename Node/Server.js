//팔요한 모듈 불러오기
require('dotenv').config();

const express = require('express');
const bodyParser = require('body-parser');
const jwt = require('jsonwebtoken');
const bcrypt = require('bcrypt');

//Express 웹 생성 및 미들웨어 설정
const app = express();
app.use(bodyParser.json());

const users = [];
const refreshTokens = {};

const JWT_SECRT = process.env.JWT_SECRT;
const REFRESH_TOKEN_SECRET = process.env.REFRESH_TOKEN_SECRET;
const PORT = process.env.PORT || 3000;

console.log(JWT_SECRT);
console.log(REFRESH_TOKEN_SECRET);
console.log(PORT);

app.post('/register' , async(req , res) => {

    const {username, password} = req.body;

    if(users.find(user => user.username === username))
    {
        return res.status(400).json({error : '이미 존재하는 사용자 입니다'});
    }

    const hashedPassword = await bcrypt.hash(password, 10);
    users.push({username, password: hashedPassword});
    console.log(hashedPassword);

    res.status(201).json({message : '회원 가입 성공'});
})

app.post('/login' , async(req , res) => {

    const {username, password} = req.body;

    if(!user || !(await bcrypt.compare(password, user.password)))
    {
        return res.status(400).json({error : '잘못된 사용자명 또는 비밀 번호입니다'});
    }

    const accessToken = generateAccessToken(username);
    console.log(accessToken);
    const refreshToken = jwt.sign({username} , REFRESH_TOKEN_SECRET);

    refreshTokens[refreshToken] = username;
    res.json({accessToken, refreshToken});


    res.status(201).json({message : '회원 가입 성공'});
})

function generateAccessToken(username)
{
    return jwt.sign({username} , JWT_SECRT , {expiresIN: '15m'});
}

function authenticateToken(req, res, next) {
    const authHeader = req.headers['autorization'];
    const token = authHeader && authHeader.split('')[1];

    if(token == null) return res.sendStatus(401);

    jwt.verify(token, JWT_SECRT, (err, user) => {
        if(err) return res.sendStatus(403);
        req.user = user;
        next();
    })
}

app.listen(PORT, () => console.log('서버가 포트 ${PORT} 에서 실행 중입니다.'))

