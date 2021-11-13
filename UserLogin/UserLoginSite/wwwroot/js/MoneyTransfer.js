//File:     employee.js
//Purpose:  holds the CRUD methods for the application
//Author:   Evan Somers
//Date:     November 1st, 2019
$(function () { // studentlist.js


    $("#fullname").click(() => {
        window.location.href = "updateAccount.html";
    });


    $("#logoutBtn").click(() => {
        sessionStorage.clear();
        $("#theModal").modal("toggle");
    });
    $("#sendbtn").click(() => {
        let data = JSON.parse(sessionStorage.getItem('bankAccount'));
        sessionStorage.setItem("Id", data.id);
        sessionStorage.setItem('timer', data.timer);
        sendMoney();
    }); // actionbutton click

    const sendMoney = async () => {
        let sendid = parseInt($("#accountid").val());
        let val = parseInt($("#amount").val());
        let acc = parseInt(sessionStorage.getItem("Id"));
        let withdrawresp = await fetch(`api/MoneyTransfer/${acc}/${sendid}/${val}`);
        
        if (withdrawresp.ok) // or check for response.status 
        {
            let data = await withdrawresp.json();
            var msg = data.msg;
            $("#status").text("Money Sent!");
        }
        else {
            $("#status").text(`${response.status}, Text - ${response.statusText}`);
        } // else
    }
    const onLoad = () => {
        $("#theModal").modal("toggle");
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