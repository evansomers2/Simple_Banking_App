//File:     employee.js
//Purpose:  holds the CRUD methods for the application
//Author:   Evan Somers
//Date:     November 1st, 2019
$(function () { // studentlist.js


    $("#fullname").click(() => {
        window.location.href = "updateAccount.html";
    });

    $("#balancebtn").click(() => {
        $("#theModal").modal("toggle");
        loadAccount();
    });
    $("#logoutBtn").click(() => {
        sessionStorage.clear();
        window.location.href = "Login.html";
    });

    $("#withdrawbtn").click(() => {
        let data = JSON.parse(sessionStorage.getItem('bankAccount'));
        sessionStorage.setItem('timer', data.timer);
        withdraw();
    }); // actionbutton click
    $("#depositbtn").click(() => {
        let data = JSON.parse(sessionStorage.getItem('bankAccount'));
        sessionStorage.setItem('timer', data.timer);
        deposit();
    }); // actionbutton click
    const loadAccount = async () => {
        let id = sessionStorage.getItem("Id");
        let response = await fetch(`api/bankaccount/${id}`);
        if (!response.ok) // or check for response.status
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let data = await response.json(); // this returns a promise, so we await it 
        sessionStorage.setItem('bankAccount', JSON.stringify(data));
        $("#accountid").text(data.id);
        $("#balance").text(data.balance);
    }
    const onLoad = async () => {
        loadAccount();
    }
    const withdraw = async () => {
        let withdrawVal = parseInt($("#TextBoxWithdraw").val());
        let balance = parseInt($("#balance").text());
        let newBalance = balance - withdrawVal;
        let id = sessionStorage.getItem("Id");
        let response = await fetch(`api/bankaccount/${id}`);
        if (!response.ok) // or check for response.status
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let data = await response.json(); // this returns a promise, so we await it 

        account = new Object();
        account.id = data.id;
        account.balance = newBalance;
        account.timer = sessionStorage.getItem("timer");
        account.customerid = id;

        let withdrawresp = await fetch("api/bankaccount", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(account)
        });
        if (withdrawresp.ok) // or check for response.status 
        {
            let data = await withdrawresp.json();
            var msg = data.msg;
        }
        else {
            $("#status").text(`${response.status}, Text - ${response.statusText}`);
        } // else
        loadAccount();
    }

    const deposit = async () => {
        let depositVal = parseInt($("#TextBoxDeposit").val());
        let balance = parseInt($("#balance").text());
        let newBalance = balance + depositVal;
        let id = sessionStorage.getItem("Id");
        let response = await fetch(`api/bankaccount/${id}`);
        if (!response.ok) // or check for response.status
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let data = await response.json(); // this returns a promise, so we await it 

        depoAccount = new Object();
        depoAccount.id = data.id;
        depoAccount.balance = newBalance;
        depoAccount.timer = sessionStorage.getItem("timer");
        depoAccount.customerid = id;

        let despositresp = await fetch("api/bankaccount", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(depoAccount)
        });
        if (despositresp.ok) // or check for response.status 
        {
            let data = await despositresp.json();
        }
        else {
            $("#status").text(`${despositresp.status}, Text - ${despositresp.statusText}`);
        } // else
        loadAccount();
    }
    const formatDate = (date) => {
        if (date === null) {
            return "";
        }
        let d;
        (date === undefined) ? d = new Date() : d = new Date(Date.parse(date));
        let _day = d.getDate();
        let _month = d.getMonth() + 1;
        let _year = d.getFullYear();
        let _hour = d.getHours();
        let _min = d.getMinutes();
        if (_min < 10) { _min = "0" + _min; }
        return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min;
    }//format date
    onLoad();
}); // jQuery ready method