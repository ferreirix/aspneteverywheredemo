var PushpinFactory = new function () { 
 
    //Function for merging to simple objects 
    function mergeObjects(first, second) { 
        if (second != null) { 
            for (key in second) { 
                first[key] = second[key] 
            } 
        } 
        return first; 
    } 
 
    //Calculates a pixel radius based on a values linear relation to a maximum value. 
    //Radius is bounded by min/max radius values. 
    function calculateRadius(value, maxValue, minRadius, maxRadius) { 
        value = value || 0; 
 
        var radius = value / maxValue * maxRadius; 
 
        if (radius < minRadius) { 
            radius = minRadius; 
        } else if (radius > maxRadius) { 
            radius = maxRadius; 
        } 
 
        return radius; 
    } 
 
    this.ColoredPin = function (location, options) { 
        options.icon = options.icon || 'images/transparent_pin.png'; 
        options.width = options.width || 24; 
        options.height = options.height || 37; 
        options.fillColor = options.fillColor || new Microsoft.Maps.Color(255, 0, 175, 255); 
        options.anchor = options.anchor || new Microsoft.Maps.Point(13, 37); 
 
        var pinHTML = ['<svg height="', 
                    options.width, '" width="', options.height, 
                    '" version="1.0" xmlns="http://www.w3.org/2000/svg"><circle cx="13" cy="13" r="11" style="fill:', 
                    options.fillColor.toHex(), 
                    ';fill-opacity:1;"/></svg><img style="position:absolute;top:0;left:0;" src="', options.icon, '"/>']; 
 
        if (options.text) { 
            pinHTML.push('<span style="position:absolute;left:0px;color:#fff;font-size:12px;text-align:center;width:', options.width, 'px;top:5px;">', options.text, '</span>'); 
        } 
 
        options.htmlContent = pinHTML.join(''); 
 
        return new Microsoft.Maps.Pushpin(location, options); 
    }; 
 
 
 
    this.ScaledCircles = function (minRadius, maxRadius, maxValue) { 
 
        maxValue = maxValue || 100; 
        minRadius = minRadius || 1; 
        maxRadius = maxRadius || 50; 
 
        var defaultFillColor = new Microsoft.Maps.Color(150, 0, 175, 255), 
            defaultStrokeColor = new Microsoft.Maps.Color(150, 130, 130, 130), 
            defaultStrokeThickness = 2; 
 
        function createCircleHTML(radius, options) { 
            options = options || {}; 
            options.fillColor = options.fillColor || defaultFillColor; 
            options.strokeColor = options.strokeColor || defaultStrokeColor; 
            options.strokeThickness = options.strokeThickness || defaultStrokeThickness; 
 
            var circleHTML = ['<div"><svg height="', 
                    radius * 2, '" width="', radius * 2, 
                    '" version="1.0" xmlns="http://www.w3.org/2000/svg"><circle cx="', 
                    radius, '" cy="', radius, '" r="', radius, 
                    '" style="fill:', options.fillColor.toHex(), 
                    ';stroke:', options.strokeColor.toHex(), 
                    ';stroke-width:', options.strokeThickness, 
                    ';fill-opacity:', options.fillColor.getOpacity(), 
                    ';stroke-opacity:', options.strokeColor.getOpacity(), 
                    ';"/></svg>']; 
 
            if (options.text) { 
                circleHTML.push('<span style="position:absolute;left:0px;color:#fff;font-size:12px;text-align:center;width:', radius * 2, 'px;top:', radius - 6, 'px;">', options.text, '</span>'); 
            } 
 
            circleHTML.push('</div>'); 
 
            return circleHTML.join(''); 
        } 
 
        this.Create = function (location, value, options) { 
            var radius = calculateRadius(value, maxValue, minRadius, maxRadius); 
 
            var pin = new Microsoft.Maps.Pushpin(location, { 
                anchor: new Microsoft.Maps.Point(radius, radius), 
                height: radius * 2, 
                width: radius * 2, 
                htmlContent: createCircleHTML(radius, options) 
            }); 
 
            pin.setValue = function (val, opt) { 
                value = val; 
                radius = calculateRadius(value, maxValue, minRadius, maxRadius); 
                options = mergeObjects(options, opt); 
 
                pin.setOptions({ htmlContent: createCircleHTML(radius, options) }); 
            }; 
 
            pin.getValue = function(){ 
                return value; 
            }; 
 
            return pin; 
        }; 
    }; 

};
