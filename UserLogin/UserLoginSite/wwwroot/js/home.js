//File:     employee.js
//Purpose:  holds the CRUD methods for the application
//Author:   Evan Somers
//Date:     November 1st, 2019
$(function () { // studentlist.js
    
    const setName = () => {
        var fname = sessionStorage.getItem("firstname");
        var lname = sessionStorage.getItem("lastname");
        var fullname = fname + " " + lname;
        $("#fullname").text(fullname + " ");
        let data = JSON.parse(sessionStorage.getItem("userAccount"));
        $("#usrname").text(data.userName);
        $("#firstname").text(data.firstName);
        $("#lastname").text(data.lastName);
        $("#dob").text(formatDate(data.dob));
        $("#status").text(`Welcome ${data.userName}!`)
    }

    $("#fullname").click(() => {
        window.location.href = "updateAccount.html";
    });


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
    setName();

}); // jQuery ready method