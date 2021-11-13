//File:     employee.js
//Purpose:  holds the CRUD methods for the application
//Author:   Evan Somers
//Date:     November 1st, 2019
$(function () { // studentlist.js

    $("#create").click(() => {
        add();
        

    });
    const startup = () => {
        $("#status").css('color', 'Black');
        $("#status").val("");
    }
    const add = async () => {
        let pass = $("#TextBoxPassword").val();
        let passHash = await fetch(`api/HashPassword/${pass}`);
        if (!passHash.ok) // or check for response.status
            throw new Error(`Status - ${passHash.status}, Text - ${passHash.statusText}`);
        let hash = await passHash.json(); // this returns a promise, so we await it 

        var account = new Object();
        account.username = $("#TextBoxUsername").val();
        account.password = hash.passWord;
        account.firstname = $("#TextBoxFirstname").val();
        account.lastname = $("#TextBoxLastname").val();
        account.email = $("#TextBoxEmail").val();
        account.dob = $("#TextBoxDob").val();
        account.timer = null;
        account.id = -1;
        
        let response = await fetch("api/user", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(account)
        });
        if (response.ok) // or check for response.status 
        {
            let data = await response.json();
            $("#status").css('color', 'Green');
            $("#status").text("Account Created!");
            window.location.href = "Login.html";
        }
        else {
            $("#status").text(`${response.status}, Text - ${response.statusText}`);
        } // else
        
    }

}); // jQuery read