<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script src="/scripts/jquery-1.4.1.js" language="javascript" type="text/javascript"></script>
<script src="/scripts/json2.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    $(function () {
        var drawing = {
            IsDrawing: false,
            MvcApp_Models_Canvas: function (canvas) {
                $("#drawing").each(function () {
                    if (this.getContext) {
                        var ctx = this.getContext('2d');
                        ctx.fillStyle = "rgb(0,0,0)";
                        ctx.fillRect(canvas.X, canvas.Y, 1, 1);
                    }
                });
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
        });

        $("#drawing").mouseup(function (event) {
            drawing.IsDrawing = false;
        });

        $("#drawing").mousemove(function (event) {
            if (drawing.IsDrawing && this.getContext) {
                var canvas = { uri: "/canvas", data: { x: event.clientX - event.currentTarget.offsetLeft, y: event.clientY - event.currentTarget.offsetTop} };
                ws.send(JSON.stringify(canvas));
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
            }
        </style>
    </head>
    <body>
        <h1>Start drawing</h1>
        <canvas id="drawing" width="200" height="200" />
    </body>
</html>
