let answersBase = "https://localhost:5001/api/answers/";
let questionsBase = "https://localhost:5001/api/questions/";

(() => {
    let clicked = false;
    $("#addAnswerBtn").click(function () {
        if (!clicked) {
            $("#replyDiv").show();
            $(this).text("Hide Reply");
            clicked = true;
        } else {
            $("#replyDiv").hide();
            $(this).text("Add Answer");
            clicked = false;
        }
    })
})();

function LikeAnswer(e) {
    if ($(e).css("color") == "green") {
        return;
    } else {
        let currentRating = $(e).parent().find("p");
        let answerId = $(e).parent().find("input").val();
        let username = $("#Username").text();
        let iconUp = $(e).parent().find(".ratingUp i");
        let iconDown = $(e).parent().find(".ratingDown i");

        $.ajax({
            method: "POST",
            url: answersBase,
            contentType: "application/json",
            data: JSON.stringify({ Rating: 1, AnswerId: answerId, Username: username })
        }).then(function (res) {
            let updatedRating = Number(currentRating.text())+1;
            currentRating.text(updatedRating);
            iconUp.removeClass("neutral").addClass("liked");
            iconDown.removeClass("disliked").addClass("neutral");
        }).catch(function (res) {
            console.log(res);
        })
    }
}

function DislikeAnswer(e) {
    if ($(e).css("color") == "red") {
        return;
    } else {
        let currentRating = $(e).parent().find("p");
        let answerId = $(e).parent().find("input").val();
        let username = $("#Username").text();
        let iconUp = $(e).parent().find(".ratingUp i");
        let iconDown = $(e).parent().find(".ratingDown i");
        $.ajax({
            method: "POST",
            url: answersBase,
            contentType: "application/json",
            data: JSON.stringify({ Rating: -1, AnswerId: answerId, Username: username })
        }).then(function (res) {
            let updatedRating = Number(currentRating.text())-1;
            currentRating.text(updatedRating);
            iconUp.removeClass("liked").addClass("neutral");
            iconDown.removeClass("neutral").addClass("disliked");
        }).catch(function (res) {
            console.log(res);
        })
    }
}

function LikeQuestion(e) {
    if ($(e).css("color") == "red") {
        return;
    } else {
        let currentRating = $(e).parent().find("p");
        let questionId = $(e).parent().find("input").val();
        let username = $("#Username").text();
        let iconUp = $(e).parent().find(".ratingUp i");
        let iconDown = $(e).parent().find(".ratingDown i");
        $.ajax({
            method: "POST",
            url: questionsBase,
            contentType: "application/json",
            data: JSON.stringify({ Rating: 1, QuestionId: questionId, Username: username })
        }).then(function (res) {
            let updatedRating = Number(currentRating.text())+1;
            currentRating.text(updatedRating);
            iconUp.removeClass("neutral").addClass("liked");
            iconDown.removeClass("disliked").addClass("neutral");
        }).catch(function (res) {
            console.log(res);
        })
    }
}

function DislikeQuestion(e) {
    if ($(e).css("color") == "red") {
        return;
    } else {
        let currentRating = $(e).parent().find("p");
        let questionId = $(e).parent().find("input").val();
        let username = $("#Username").text();
        let iconUp = $(e).parent().find(".ratingUp i");
        let iconDown = $(e).parent().find(".ratingDown i");
        $.ajax({
            method: "POST",
            url: questionsBase,
            contentType: "application/json",
            data: JSON.stringify({ Rating: -1, QuestionId: questionId, Username: username })
        }).then(function (res) {
            let updatedRating = Number(currentRating.text())-1;
            currentRating.text(updatedRating);
            iconUp.removeClass("liked").addClass("neutral");
            iconDown.removeClass("neutral").addClass("disliked");
        }).catch(function (res) {
            console.log(res);
        })
    }
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#profilePicIcon").hide();
            $('#profilePic').show();
            $('#profilePic')
                .attr('src', e.target.result)
        };

        reader.readAsDataURL(input.files[0]);
    }
}