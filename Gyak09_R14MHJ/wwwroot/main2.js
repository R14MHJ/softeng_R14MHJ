var jovalasz;
var questionId = 4;
var hotList = [];           
var questionsInHotList = 3; 
var displayedQuestion;      
var numberOfQuestions;      
var nextQuestion = 1;
var TOhandler;
TOhandler = setTimeout(előre, 3000);

window.onload = function (e) {
    console.log("oldal betoltve");
    document.getElementById("vissza").onclick = vissza;
    document.getElementById("elore").onclick = elore;
    kerdesbetoltes(questionId)
}
fetch('/hajo/kerdes/4')
    .then(response => response.json())
    .then(data => kerdesmegjelenites(data));
function kerdesmegjelenites(data) {
    let kérdés = hotList[displayedQuestion].question; 
    if (!data) return;
    console.log(data)
    document.getElementById("kerdes").innerText = kérdés.question1
    document.getElementById("V1").innerText = kérdés.answer1
    document.getElementById("V2").innerText = kérdés.answer2
    document.getElementById("V3").innerText = kérdés.answer3
    if (kérdés.image) {
        document.getElementById("img1").src = "https://szoft1.comeback.hu/hajo/" + kérdés.image;
        document.getElementById("img1").classList.remove("rejtett")
    }
    else {
        document.getElementById("img1").classList.add("rejtett")
    }
    jovalasz = kérdés.correctAnswer;
    document.getElementById("V1").classList.remove("jo", "rossz")
    document.getElementById("V2").classList.remove("jo", "rossz")
    document.getElementById("V3").classList.remove("jo", "rossz")
}
function kerdesbetoltes(id, cel) {
    fetch(`/hajo/kerdes/${id}`)
        .then(response => {
            if (!response.ok) {
                console.error(`Hibás válasz: ${response.status}`)
            }
            else {
                return response.json()
            }
        })
        .then(
            q => {
                hotList[cel].question = q;
                hotList[cel].goodAnswers = 0;
                console.log(`A ${id}. kérdés letöltve a hot list ${cel}. helyére`)
                if (displayedQuestion == undefined && cel == 0) { 
                    displayedQuestion = 0;
                    kerdesmegjelenítés();
            }
        );
        .then(data => kerdesmegjelenites(data));
}
function bet() {
    for (var i = 0; i < questionsInHotList; i++) {
        let x = {
            question: {},
            goodAnswers: 0
        }
        hotList[i] = x;
    }
    for (var i = 0; i < questionsInHotList; i++) {
        kérdésBetöltés(nextQuestion, i);
        nextQuestion++;
    }
}
function elore() {
    displayedQuestion++
    if (displayedQuestion == questionsInHotList) displayedQuestion = 0;
    questionId++
    kerdesbetoltes(questionId)
    clearTimeout(TOhandler)
}
function vissza() {
    questionId--
    kerdesbetoltes(questionId)
}
function valasztas(n) {
    if (n != jovalasz) {
        document.getElementById(`V${n}`).classList.add("rossz")
        document.getElementById(`V${jovalasz}`).classList.add("jo")
    }
    else {
        document.getElementById(`V${n}`).classList.add("jo")
    }
    document.getElementById(`V1`).style.pointerEvents = "none"
    document.getElementById(`V2`).style.pointerEvents = "none"
    document.getElementById(`V3`).style.pointerEvents = "none"
}