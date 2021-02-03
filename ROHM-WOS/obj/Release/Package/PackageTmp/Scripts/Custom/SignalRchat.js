
    $(function () {
           
        $(".conv").on("click", function () {
            $("#ConversationID").val($(this).attr('id'));
            $(".widget-chat-header-title").text($(this).attr('id'));
            RestartChat();
        });

        $('#message').keypress(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            //event.preventDefault();
            if (keycode == '13') {
                $('#sendmessage').trigger("click");
            }
        });
    });


function RestartChat() {
    var chat = $.connection.chatHub;
    chat.client.addNewMessageToPage = function (name, message, ConversationID, Timenow) {
        if (ConversationID == $("#ConversationID").val()) {
            if (name == $('#currentuser').val()) {
                $("#discussionchatbox").append(
                    "<div class='widget-chat-item right'>" +
                    "    <div class='widget-chat-info'>" +
                    "        <div class='widget-chat-info-container'>" +
                    "            <div class='widget-chat-message'>" + message + "</div>" +
                    "            <div class='widget-chat-time'>" + Timenow + "</div>" +
                    "        </div>" +
                    "    </div>" +
                    "</div>"
                )
            }
            else {
                $("#discussionchatbox").append(
                    "<div class='widget-chat-item with-media left'>" +
                    "    <div class='widget-chat-media'>" +
                    "        <img alt='' src='/PictureResources/UsersPhoto/1.jpg' />" +
                    "    </div>" +
                    "    <div class='widget-chat-info'>" +
                    "        <div class='widget-chat-info-container'>" +
                    "            <div class='widget-chat-name text-primary'>" + name + "</div>" +
                    "            <div class='widget-chat-message'>" + message + "</div>" +
                    "            <div class='widget-chat-time'>" + Timenow + "</div>" +
                    "        </div>" +
                    "    </div>" +
                    "</div>"
                )
            }
            $('#discussionchatbox').scrollTop($('#discussionchatbox')[0].scrollHeight);
        }
    };

    $('#displayname').val($('#currentuser').val());
    $('#displayphoto').val($('#currentuser').val());
    $('#message').focus();
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            chat.server.send($('#displayname').val(), $('#message').val(), $("#ConversationID").val());
            $('#message').val('').focus();
        });
    });
   
}
  