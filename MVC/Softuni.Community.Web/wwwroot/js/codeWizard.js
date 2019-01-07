$(document).ready(StartApp);

var problemBase = "https://localhost:5001/api/problem/";

var gameScore = 0;

var startSection = $("#start");
var loadingDiv = $("#loadingDiv");
var game = $("#game");
var playAgain = $("#playAgain");
var quizContent = $("#quizContent");
var score = $("#score");

var questionNo = $("#questionNo");

var choiceA = $("#choiceA");
var choiceB = $("#choiceB");
var choiceC = $("#choiceC");
var choiceD = $("#choiceD");

function StartApp() {
    choiceA.click(checkChoise);
    choiceB.click(checkChoise);
    choiceC.click(checkChoise);
    choiceD.click(checkChoise);
    playAgain.click(function () {
        nextQuestion();
        ResetApp();
    });

    configStartButton(loadingDiv, game);
}

function configStartButton(loadingDiv, game) {
    let startBtn = $("#startGame");
    startBtn.click(function () {
        let isAdmin = $("#manageGameDiv");
        if (isAdmin != undefined) {
            isAdmin.hide();
        }
        loadingDiv.show();
        setTimeout(function () {
            nextQuestion();
            startSection.hide();
            startBtn.hide();
            game.show();
        }, 1000);
    });
}

function SetQuestion(content) {
    quizContent.find("h1").text(content);
}

function SetAnswers(a, b, c, d) {
    choiceA.find("h3").text(a);
    choiceB.find("h3").text(b);
    choiceC.find("h3").text(c);
    choiceD.find("h3").text(d);
}

function nextQuestion() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            var result = JSON.parse(this.responseText);
            let answers = [];
            for (var i = 0; i < 4; i++) {
                answers.push(result.answers[i]);
            }
            console.log(result);
            $("#currentRightAnswer").text(result.rightAnswer);
            SetQuestion(result.problemContent);
            SetAnswers(answers[0], answers[1], answers[2], answers[3]);
        }
    };
    xhttp.open("GET", problemBase, true);
    xhttp.send();
}

function checkChoise(event) {
    let element = $(event.currentTarget);
    element.css("background-color", "rgb(231, 137, 14)");

    if (IsRightChoise(element.find("h3").text())) {
        setTimeout(() => {
            element.css("background-color", "rgb(109, 241, 0)");
            element.fadeIn(100).fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);
        }, 1500);
        setTimeout(() => {
            gameScore += 100;
            questionNo.text(Number(questionNo.text()) + 1);
            element.css("background-color", "rgba(0, 0, 0, 0)");
            nextQuestion();
        }, 3000);

    }
    else {
        setTimeout(function () {
            element.css("background-color", "rgb(255, 0, 0)");
            element.fadeOut(0).fadeIn(100).fadeOut(0).fadeIn(100);
        }, 1500);
        setTimeout(function () {
            element.css("background-color", "rgba(0, 0, 0, 0)");
            game.hide();
            score.find("h2").text(gameScore);
            score.show();
        }, 3000);
    }

}

function IsRightChoise(choiceContent) {
    if ($("#currentRightAnswer").text() === choiceContent) {
        return true;
    }
    return false;
}

function ResetApp() {
    questionNo.text("1");
    gameScore = 0;
    score.hide();
    game.show();
}