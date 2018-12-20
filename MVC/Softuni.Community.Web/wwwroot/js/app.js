(()=> {
    let clicked = false;
    $("#addAnswerBtn").click(function() {
        if (!clicked) {
            $("#replyDiv").show();
            clicked = true;
        } else {
            $("#replyDiv").hide();
            clicked = false;
        }
    })
})();