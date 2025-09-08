let express = require('express');           // eexpress 모듈 가져오가
let app = express();                        // express를 app 이름으 로 정의후 사용

app.get('/' , function(req, res){       // 기본 라우터에서 hello world 변환
    res.send('Hello world');
});


app.get('/about' , function(req, res){       // about에서 about.data를 반환한다
    res.send('Player data 1111');
});

app.listen(3000, function(){

    console.log('listening on port 3000');      // 3000포트에서 입력 대기
});