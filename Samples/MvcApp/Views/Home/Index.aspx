<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script src="/scripts/jquery-1.4.1.js" language="javascript" type="text/javascript"></script>
<script src="/scripts/json2.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    $(function () {
        var ws;
        var index = {
            MvcApp_Models_Poll: function (poll) {
                $("#voting").hide();
                $("#yes").text(poll.Yes);
                $("#no").text(poll.No);
                $("#poll-result").show();
            },
            MvcApp_Models_Chat: function (chat) {
                $("#chat").append(chat.Text + "<br />");
            }
        };
        // create a new websocket and connect
        ws = new WebSocket('ws://localhost:8181/');

        // when data is comming from the server, this method is called
        ws.onmessage = function (evt) {
            var response = JSON.parse(evt.data);
            if (typeof index[response.Uri] == "function") {
                index[response.Uri](response.Data);
            }
        };

        // when the connection is established, this method is called
        ws.onopen = function () {
            //inc.innerHTML += '.. connection open<br/>';
        };

        // when the connection is closed, this method is called
        ws.onclose = function () {
            //inc.innerHTML += '.. connection closed<br/>';
        }
        $("#yesvote").click(function () {
            var vote = { uri: "/select", data: { yes: true} };
            ws.send(JSON.stringify(vote));
            $("#poll").hide();
            $("#voting").show();
            return false;
        });
        $("#novote").click(function () {
            var vote = { uri: "/select", data: { yes: false} };
            ws.send(JSON.stringify(vote));
            $("#poll").hide();
            $("#voting").show();
            return false;
        });
        function chat() {
            var text = $("#chatinput");
            var chat = { uri: "/chat", data: { text: text.val()} };
            ws.send(JSON.stringify(chat));
            text.val('');
        }
        $("#chatbutton").click(function () {
            chat();
        });
        $("#chatinput").keyup(function (event) {
            if (event.keyCode == '13') {
                chat();
            }
        });
    });
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Html web sockets!</title>
    </head>
    <body>
        <div id="poll">
            Do you vote?
            <br />
            <a id="yesvote" href="#">Yes!</a>
            <br />
            <a id="novote" href="#">No!</a>
        </div>
        <div id="voting" style="display: none;">
            Voting...
        </div>
        <div id="poll-result" style="display: none;">
            Answers so far <br />
            Yes <span id="yes">0</span>
            <br />
            No <span id="no">0</span>
        </div>
        <a href="/canvas">Check out the canvas</a>
        <br />
        <label for="chatinput">Chat</label>
        <input type="text" id="chatinput" />
        <input type="button" value="Submit" id="chatbutton" />
        <div id="chat"></div>
    </body>
</html>
