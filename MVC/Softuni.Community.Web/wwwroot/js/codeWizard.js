$(document).ready(StartApp);

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
        loadingDiv.show();
        setTimeout(function () {
            startSection.hide();
            startBtn.hide();
            game.show();
        }, 1000);
    });
}

function GetQuestions() {

}

function nextQuestion() {
    quizContent.find("h1").text("What is HTML?");
    choiceA.find("h3").text("Haskell-TeX Mashup Language");
    choiceB.find("h3").text("А fictional word from students");
    choiceC.find("h3").text("Hyper Text Markup Language");
    choiceD.find("h3").text("Search engine");
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
            element.css("background-color", "rgba(0, 0,0, 0)");
            nextQuestion();
        }, 3000);

    }
    else {
        setTimeout(function () {
            element.css("background-color", "rgb(255, 0, 0)");
            element.fadeOut(0).fadeIn(100).fadeOut(0).fadeIn(100);
        }, 1500);
        setTimeout(function () {
            element.css("background-color", "rgba(0, 0,0, 0)");
            game.hide();
            score.find("h2").text(gameScore);
            score.show();
        }, 3000);
    }

}

function IsRightChoise(choiceContent) {
    let choises = ["Version-control system" , "Hyper Text Markup Language"];
    console.log(choiceContent);
   
    
    let index = Number(questionNo.text())-1;
    console.log(index);
    if (choises[index] === choiceContent) {
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