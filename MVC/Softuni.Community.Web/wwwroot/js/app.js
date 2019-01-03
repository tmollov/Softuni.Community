let answersBase = "https://localhost:5001/api/answers/";
let questionsBase = "https://localhost:5001/api/questions/";
let jokeBase = "https://localhost:5001/api/jokes/";
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
    if ($(e).hasClass("liked")) {
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
            let updatedRating = Number(currentRating.text()) + 1;
            currentRating.text(updatedRating);
            iconUp.removeClass("neutral").addClass("liked");
            iconDown.removeClass("disliked").addClass("neutral");
        }).catch(function (res) {
            console.log(res);
        })
    }
}

function DislikeAnswer(e) {
    if ($(e).hasClass("disliked")) {
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
            let updatedRating = Number(currentRating.text()) - 1;
            currentRating.text(updatedRating);
            iconUp.removeClass("liked").addClass("neutral");
            iconDown.removeClass("neutral").addClass("disliked");
        }).catch(function (res) {
            console.log(res);
        })
    }
}

function LikeQuestion(e) {
    if ($(e).hasClass("liked")) {
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
            let updatedRating = Number(currentRating.text()) + 1;
            currentRating.text(updatedRating);
            iconUp.removeClass("neutral").addClass("liked");
            iconDown.removeClass("disliked").addClass("neutral");
        }).catch(function (res) {
            console.log(res);
        })
    }
}

function DislikeQuestion(e) {
    if ($(e).hasClass("disliked")) {
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
            let updatedRating = Number(currentRating.text()) - 1;
            currentRating.text(updatedRating);
            iconUp.removeClass("liked").addClass("neutral");
            iconDown.removeClass("neutral").addClass("disliked");
        }).catch(function (res) {
            console.log(res);
        })
    }
}


function LikeJoke(e) {
    if ($(e).hasClass("liked")) {
        return;
    } else {
        let currentDislikes = $(e).parent().find("p.dislikes");
        let currentLikes = $(e).parent().find("p.likes");
        let jokeId = $(e).parent().find("span").text().replace("::","");
        let username = $("#Username").text();
        let iconUp = $(e).parent().find(".ratingUp i");
        let iconDown = $(e).parent().find(".ratingDown i");

        $.ajax({
            method: "POST",
            url: jokeBase,
            contentType: "application/json",
            data: JSON.stringify({ Rating: 1, JokeId: jokeId, Username: username })
        }).then(function (res) {
            currentLikes.text(Number(currentLikes.text()) + 1);
            iconUp.removeClass("neutral").addClass("liked");
            if (iconDown.hasClass("disliked")) {
                currentDislikes.text(Number(currentDislikes.text()) + 1);
            }

            iconDown.removeClass("disliked").addClass("neutral");
        }).catch(function (res) {
            console.log(res);
        })
    }
}

function DislikeJoke(e) {
    if ($(e).hasClass("disliked")) {
        return;
    } else {
        let currentDislikes = $(e).parent().find("p.dislikes");
        let currentLikes = $(e).parent().find("p.likes");
        let jokeId = $(e).parent().find("span").text().replace("::","");
        let username = $("#Username").text();
        let iconUp = $(e).parent().find(".ratingUp i");
        let iconDown = $(e).parent().find(".ratingDown i");


        $.ajax({
            method: "POST",
            url: jokeBase,
            contentType: "application/json",
            data: JSON.stringify({ Rating: -1, JokeId: jokeId, Username: username })
        }).then(function (res) {
            currentDislikes.text(Number(currentDislikes.text()) - 1);
            iconDown.removeClass("neutral").addClass("disliked");
            if (iconUp.hasClass("liked")) {
                currentLikes.text(Number(currentLikes.text()) - 1);
            }
            iconUp.removeClass("liked").addClass("neutral");
        }).catch(function (res) {
            console.log(res);
        });
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

// jquery Tabs
$(function () {
    $("#tabs").tabs();
});