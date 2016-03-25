function getLocation() {
    var x = document.getElementById("demo");
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(showPosition);
    }
    else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    var lat = position.coords.latitude.toFixed(1);
    var lon = position.coords.longitude.toFixed(1);
    document.getElementById("demo").innerHTML = "You're around Latitude: " + lat + " and Longitude: " + lon;

    var xhr = new XMLHttpRequest();
    var params = "latitude=" + lat + "&longitude=" + lon;
    console.log("Params: " + params);
    xhr.open('PUT', '/locations', true);
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    xhr.onreadystatechange = function () {
        console.log("Response:" & this.responseText);
    };
    xhr.send(params); 
}

