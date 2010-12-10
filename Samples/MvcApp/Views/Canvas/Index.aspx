<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script src="/scripts/jquery-1.4.1.js" language="javascript" type="text/javascript"></script>
<script src="/scripts/json2.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    $(function () {
        var drawing = {
            Name: "Anonymous",
            IsDrawing: false,
            MvcApp_Models_Canvas: function (canvas) {
                var drawingCanvas = document.getElementById("drawing");
                if (drawingCanvas.getContext) {
                    var ctx = drawingCanvas.getContext('2d');
                    ctx.fillStyle = "rgb(" + canvas.R + "," + canvas.G + "," + canvas.B + ")";
                    ctx.fillRect(canvas.X, canvas.Y, 1, 1);
                    var who = $("#" + canvas.Id);
                    if (who.length == 0) {
                        $("#who").append('<span id="' + canvas.Id + '">' + canvas.Name + '</span>');
                        who = $("#" + canvas.Id);
                    }
                    else {
                        who.stop();
                    }
                    who.css('color', 'rgb(' + canvas.R + ',' + canvas.G + ',' + canvas.B + ')');
                    who.animate({ opacity: 1.0 }, 1000, function () {
                        $(this).fadeOut(1000, function () { $(this).remove(); })
                    });
                }
            },
            Clear: function () {
                var drawingCanvas = document.getElementById("drawing");
                drawingCanvas.width = drawingCanvas.width;
            },
            Draw: function (x, y) {
                if (drawing.IsDrawing) {
                    var canvas = { uri: "/canvas",
                        data: { x: x, y: y,
                            name: this.Name,
                            r: this.Color.Red(),
                            g: this.Color.Green(),
                            b: this.Color.Blue()
                        }
                    };
                    ws.send(JSON.stringify(canvas));
                }
            },
            Color: {
                Red: function () { return $("#color").val() == "red" ? 255 : 0; },
                Green: function () { return $("#color").val() == "green" ? 255 : 0; },
                Blue: function () { return $("#color").val() == "blue" ? 255 : 0; }
            }
        };
        var ws;
        // create a new websocket and connect
        ws = new WebSocket('ws://localhost:8181/');

        // when data is comming from the server, this method is called
        ws.onmessage = function (evt) {
            var response = JSON.parse(evt.data);
            if (typeof drawing[response.Uri] == "function") {
                drawing[response.Uri](response.Data);
            }
        };

        $("#drawing").mousedown(function (event) {
            drawing.IsDrawing = true;
            drawing.Draw(event.clientX - event.currentTarget.offsetLeft, event.clientY - event.currentTarget.offsetTop);
        });

        $("#drawing").mouseup(function (event) {
            drawing.IsDrawing = false;
        });

        $("#drawing").mousemove(function (event) {
            drawing.Draw(event.clientX - event.currentTarget.offsetLeft, event.clientY - event.currentTarget.offsetTop);
        });

        $("#clear").click(function () {
            drawing.Clear();
            return false;
        });

        $("#submit").click(function () {
            drawing.Name = $("#username").val();
            $("#chooseyourname").hide();
            $("#draw").show();
        });

        $("#username").keydown(function (event) {
            if (event.keyCode == '13') {
                $("#submit").click();
            }
        });
    });
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Html 5 canvas!</title>
        <style type="text/css">
            canvas { 
                border: 1px solid black; 
                cursor: crosshair; 
                display: inline-block;
            }
            .who {
                vertical-align: top;
                padding: 0 0 0 10px;
                display: inline-block;
            }
            #draw {
                display: none;
            }
        </style>
    </head>
    <body>
        <div id="chooseyourname">
            <h1>What is your name?</h1>
            <input type="text" id="username" />
            <input type="submit" value="Submit" id="submit" />
        </div>
        <div id="draw">
            <h1>Start drawing</h1>
            <canvas id="drawing" width="300" height="300"></canvas>
            <div class="who">
                Who is drawing
                <div id="who"></div>
            </div>
            <br />
            <select id="color">
                <option value="black">Black</option>
                <option value="red">Red</option>
                <option value="green">Green</option>
                <option value="blue">Blue</option>
            </select>
            <br />
            <a id="clear" href="#">Clear</a>
        </div>
    </body>
</html>
