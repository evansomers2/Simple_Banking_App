//File:     employee.js
//Purpose:  holds the CRUD methods for the application
//Author:   Evan Somers
//Date:     November 1st, 2019
$(function () { // studentlist.js

    const login = async () => {
        var un = $("#TextBoxUsername").val();
        var pw = $("#TextBoxPassword").val();
        let response = await fetch(`api/user/${un}`);
        if (!response.ok) // or check for response.status
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let data = await response.json(); // this returns a promise, so we await it 
        if (pw.length > 0) {
            let hashResponse = await fetch(`api/HashPassword/${pw}`);
            if (!hashResponse.ok) // or check for response.status
                throw new Error(`Status - ${hashResponse.status}, Text - ${hashResponse.statusText}`);
            let hash = await hashResponse.json(); // this returns a promise, so we await it 

            if (data.passWord === hash.passWord) {
                sessionStorage.setItem('username', data.userName);
                sessionStorage.setItem('firstname', data.firstName);
                sessionStorage.setItem('lastname', data.lastName);
                sessionStorage.setItem('userAccount', JSON.stringify(data));
                $("#loginmsg").css('color', 'Green');
                $("#loginmsg").text("Login Success!");
                sessionStorage.setItem("Id", data.id);
                window.location.href = "home.html";
            }
            else {
                $("#loginmsg").text("Incorrect password");
                $("#loginmsg").css('color', 'red');
            }
        }
        
        else {
            if (data.userName === "not found") {
                $("#loginmsg").text("Username not found");
                $("#loginmsg").css('color', 'red');
            }
        }
    }
    $("#loginBtn").click(() => {
        $("#theModal").modal("toggle");
    });
    $("#createAccount").click(() => {
        $("#createModal").modal("toggle");
    });
    $("#actionbutton").click(() => {
        login();
    }); // actionbutton click

    $("#forgotpassword").click(() => {
        window.location.href = "forgotpassword.html";
    });
    $("#employeeList").click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === "employeeList" || Id === "") {
            Id = e.target.id;

        } // clicked on row somewhere else

        if (Id !== "status" && Id !== "heading") {
            let data = JSON.parse(sessionStorage.getItem("allemployees"));;
            Id === "0" ? setupForAdd() : setupForUpdate(Id, data);

        } else {
            return false; // ignore if they clicked on heading or status

        }
    });

}); // jQuery ready method