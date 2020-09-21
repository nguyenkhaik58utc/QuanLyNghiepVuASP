var monthNow;
var current = new Date();
var getMonthNow = current.getMonth() + 1;
var getDayNow = current.getDay();
if (getMonthNow == 1 && getDayNow < 10) {
	monthNow = 12;
}
else {
	if (getDayNow < 10) monthNow = getMonthNow - 1;
	else monthNow = getMonthNow;
}

$(document).ready(function () {
	var menuName = $("#XemLuong").text();
	document.getElementById('XemLuong').innerHTML = menuName + " " + monthNow;
});

function closeNav() {
	document.getElementById("mySidenav").style.width = "0";
	document.getElementById("mainlayout").style.marginLeft = "0";
}

function homePage() {
	$.ajax({
		url: "/Admin/Home/Homepage",
		type: "POST",
		success: function (data) {

			window.location = data;

		},
		error: function () {
			swal("Error", "change false", "error");
		}
	});
}


