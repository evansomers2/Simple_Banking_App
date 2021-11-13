//File:     employee.js
//Purpose:  holds the CRUD methods for the application
//Author:   Evan Somers
//Date:     November 1st, 2019
$(function () { // studentlist.js
    var key = Math.floor(1000 + Math.random() * 9000);
    $("#actionbutton").click(async () => {
        var un = $("#TextBoxUsername").val();
        let response = await fetch(`api/user/${un}`);
        if (!response.ok) // or check for response.status
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let data = await response.json(); // this returns a promise, so we await it 
        if (data.userName === "not found") {
            $("#loginmsg").text("Username not found");
            $("#loginmsg").css('color', 'red');
        }
        else {
            let passHash = await fetch(`api/ResetPassword/${un}/${key}`);
            if (!passHash.ok) // or check for response.status
                throw new Error(`Status - ${passHash.status}, Text - ${passHash.statusText}`);
            let hash = await passHash.json(); // this returns a promise, so we await it 
            $("#TextBoxKey").show();
            $("#resetbtn").show();
            $("#status").css('color', 'Green');
            $("#status").text("Reset email sent!")
        }
        
    });

    $("#resetbtn").click(() => {
        var inputKey = $("#TextBoxKey").val();
        if (inputKey == key) {
            $("#status").css('color', 'Green');
            $("#status").text("Valid Key!")
            window.location.href = "Login.html";
        }
        else {
            $("#status").css('color', 'Red');
            $("#status").text("Invalid Key")
        }
    });




}); // jQuery ready method