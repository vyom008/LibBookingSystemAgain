$(document).ready(function () {
    $.getJSON("/api/rooms", function (data) {
        var roomSelect = $("#room");
        data.forEach(function (room) {
            roomSelect.append(new Option(room.roomName, room.id));
        });
    });

    $("#reservationForm").submit(function (event) {
        event.preventDefault();

        var email = $("#email").val();
        var validEmailDomains = ["@student.weltec.ac.nz", "@weltec.ac.nz"];
        var emailDomain = email.substring(email.indexOf("@"));

        if (!validEmailDomains.includes(emailDomain)) {
            alert("Invalid email domain. Please use a valid WelTec email address.");
            return;
        }

        var reservation = {
            email: email,
            roomID: $("#room").val(),
            reservationDate: $("#date").val(),
            startTime: $("#startTime").val(),
            endTime: $("#endTime").val()
        };

        $.ajax({
            url: "/api/reservations",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(reservation),
            success: function () {
                alert("Reservation successful!");
            },
            error: function () {
                alert("Failed to make reservation.");
            }
        });
    });
});
