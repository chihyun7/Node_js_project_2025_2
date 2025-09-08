//http 모듈 로딩

let http = require("http");

// http 서버를 listen 상태로 8000 포트를 사용하여 만듬
http.createServer(function (requset, response)
{
    // response HTTP 타입 헤더를 정의
    response.writeHead(200, {'Content-Type' : 'text/plain'})

    response.end("Hello World")

}).listen(8000);

console.log("Server running at http://127.0.0.1:8000");