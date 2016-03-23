var map;
var randLocations = [],
    maxValue = 5000;

function PlotMe() {
    map = new Microsoft.Maps.Map(document.getElementById("mapDiv"), {
        credentials: "AuK13WyKqmzFSUQ4WTbABe-gy2ev2U5tjUni7QWa8tAHmxIcrXC29tydS6qjIwdt" ,
        mapTypeId: Microsoft.Maps.MapTypeId.road
    });

    GenerateTestLocations();
}

function GenerateTestLocations() {
    for (var i = 0; i < 50; i++) {
        randLocations.push(new Microsoft.Maps.Location(Math.random() * 170 - 85, Math.random() * 360 - 180));
    }
}

function GetRandomColor(alpha) {
    alpha = alpha || 255;

    return new Microsoft.Maps.Color(alpha,
        Math.round(Math.random() * 255),
        Math.round(Math.random() * 255),
        Math.round(Math.random() * 255));
}

function ShowColoredImagePins() {
    map.entities.clear();

    for (var i = 0; i < randLocations.length; i++) {
        var color;

        switch (i % 3) {
            case 0:
                color = 'green';
                break;
            case 1:
                color = 'yellow';
                break;
            case 2:
                color = 'red';
                break;
        }

        var pin = new Microsoft.Maps.Pushpin(randLocations[i], {
            icon: 'img/' + color + '.png',
            text: i + ''
        });

        map.entities.push(pin);
    }
}
function CreateScaledCircles() {
    map.entities.clear();

    var pinFactory = new PushpinFactory.ScaledCircles(10, 40, maxValue);

    for (var i = 0; i < randLocations.length; i++) {
        //create a mock value;
        var val = Math.round(Math.random() * maxValue);

        var pin = pinFactory.Create(randLocations[i], val, {
            fillColor: GetRandomColor(150),
            text: val + ''
        });

        map.entities.push(pin);
    }

    
}

