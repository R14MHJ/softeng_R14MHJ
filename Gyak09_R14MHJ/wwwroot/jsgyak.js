console.log("minta")
/////for (var i = 0; i < 10; i++) {
/////   let sor = document.createElement("div")   
/////
var factor = function (n) {
    let er = 1;
    for (let i = 2; i <= n; i++) {
        er = er * i;
    }
    return er;  
}
var factorR = (n) => {
    if (n == 0 || n == 1) {
        return 1;
    }
    else {
        return n*factor(n-1)
    }
}
for (var i = 0; i < 10; i++) {
    console.log(`${i}:${factorR(i)}`)
}