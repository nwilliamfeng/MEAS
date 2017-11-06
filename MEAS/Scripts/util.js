//显示倒计时     
function showCountDown(seconds, output, finish) {
    setTimeout(function () {
        var s = seconds;
        if (s == 0)
            finish();
        else {
            s--;
            if (s >= 0) {
                output(s);
                showCountDown(s, output, finish);
            }
        }
    }, 1000);
}

 