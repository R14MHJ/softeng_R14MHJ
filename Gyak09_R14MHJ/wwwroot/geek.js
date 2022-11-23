var viccek;
var kerdesek;

function download(d) {
    let ide = document.getElementById("ide");
    console.log("siker")
    console.log(' ${d.length} vicc erkezett')
    console.log(d)
    viccek = d;
    for (var i = 0; i < d.length; i++) {
        let elem = document.createElement("div")
        elem.innerHTML = d[i].text
        ide.appendChild(elem)
    }
    for (var i = 0; i < d.length; i++) {
        kerdesek[i] = d[i].text
    }
}

window.onload = function () {
    fetch('jokes.json')
        .then(response => response.json())
        .then(data => download(data));

}