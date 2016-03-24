var x = document.getElementById("demo");

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(showPosition);
    }
    else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    var lat = position.coords.latitude;
    var lon = position.coords.longitude;
    //x.innerHTML = "Latitude: " + lat + "<br>Longitude: " + lon;

    var xhr = new XMLHttpRequest();
    var params = "latitude=" + lat + "&longitude=" + lon;
    console.log("Params: " + params);
    xhr.open('PUT', 'http://localhost:5000/locations', true);
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    xhr.onreadystatechange = function () {
        console.log("Response:" & this.responseText);
    };
    xhr.send(params);
}