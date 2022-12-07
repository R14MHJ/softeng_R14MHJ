var jovalasz;
var questionId = 4;
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
    if (!data) return;
    console.log(data)
    document.getElementById("kerdes").innerText = data.question1
    document.getElementById("V1").innerText = data.answer1
    document.getElementById("V2").innerText = data.answer2
    document.getElementById("V3").innerText = data.answer3
    if (data.image) {
        document.getElementById("img1").src = "https://szoft1.comeback.hu/hajo/" + data.image;
        document.getElementById("img1").classList.remove("rejtett")
    }
    else {
        document.getElementById("img1").classList.add("rejtett")
    }
    jovalasz = data.correctAnswer;
    document.getElementById("V1").classList.remove("jo", "rossz")
    document.getElementById("V2").classList.remove("jo", "rossz")
    document.getElementById("V3").classList.remove("jo", "rossz")
}
function kerdesbetoltes(id) {
    fetch(`/hajo/kerdes/${id}`)
        .then(response => {
            if (!response.ok) {
                console.error(`Hibás válasz: ${response.status}`)
            }
            else {
                return response.json()
            }
        })
        .then(data => kerdesmegjelenites(data));
}
function elore() {
    questionId++
    kerdesbetoltes(questionId)
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
}