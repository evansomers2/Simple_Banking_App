//File:     employee.js
//Purpose:  holds the CRUD methods for the application
//Author:   Evan Somers
//Date:     November 1st, 2019
$(function () { // studentlist.js

    $("#updateBtn").click(() => {
        let data = JSON.parse(sessionStorage.getItem('userAccount'));
        sessionStorage.setItem('timer', data.timer);
        sessionStorage.setItem('id', data.id);
        var msg = update();
        
    });
    const setStatus = (msg) => {
        msg === "" ? // are we appending to an existing message
            $("#status").text("Account Updated") : $("#status").text(`${msg} - Account Loaded`);
        loadAfterUpdate();
    }
    const update = async (e) => {
        let pass = $("#TextBoxPassword").val();
        let passHash = await fetch(`api/HashPassword/${pass}`);
        if (!passHash.ok) // or check for response.status
            throw new Error(`Status - ${passHash.status}, Text - ${passHash.statusText}`);
        let hash = await passHash.json(); // this returns a promise, so we await it 

        account = new Object();
        account.username = $("#TextBoxUsername").val();
        account.password = hash.passWord;
        account.firstname = $("#TextBoxFirstname").val();
        account.lastname = $("#TextBoxLastname").val();
        account.email = $("#TextBoxEmail").val();
        account.dob = $("#TextBoxDob").val();
        account.timer = sessionStorage.getItem('timer');
        account.id = sessionStorage.getItem('id');

        let response = await fetch("api/user", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(account)
        });
        if (response.ok) // or check for response.status 
        {
            let data = await response.json();
            var msg = data.msg;
            setStatus(msg);
        }
        else {
            $("#status").text(`${response.status}, Text - ${response.statusText}`);
        } // else
        
    }

    const loadData = () => {
        let data = JSON.parse(sessionStorage.getItem("userAccount"));

        $("#TextBoxPassword").val(data.passWord);
        $("#TextBoxFirstname").val(data.firstName);
        $("#TextBoxUsername").val(data.userName);
        $("#TextBoxLastname").val(data.lastName);
        $("#TextBoxEmail").val(data.email);
        $("#TextBoxDob").val(formatDate(data.dob));
    }

    const loadAfterUpdate = async () => {
        let un = $("#TextBoxUsername").val();
        let response = await fetch(`api/user/${un}`);
        if (!response.ok) // or check for response.status
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let data = await response.json(); // this returns a promise, so we await it 


        $("#TextBoxPassword").val(data.passWord);
        $("#TextBoxFirstname").val(data.firstName);
        $("#TextBoxUsername").val(data.userName);
        $("#TextBoxLastname").val(data.lastName);
        $("#TextBoxEmail").val(data.email);
        $("#TextBoxDob").val(formatDate(data.dob));
    }

    const formatDate = (date) => {
        if (date === null) {
            return "";
        }
        let d;
        (date === undefined) ? d = new Date() : d = new Date(Date.parse(date));
        let _day = d.getDate();
        let _month = d.getMonth()+1;
        let _year = d.getFullYear();
        let _hour = d.getHours();
        let _min = d.getMinutes();
        if (_min < 10) { _min = "0" + _min; }
        return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min;
    }//format date
    loadData();
}); // jQuery read