var current = new Date();
var month = current.getMonth() + 1;
var year = current.getFullYear();

function convertDate(data) {
	var getdate = parseInt(data.replace("/Date(", "").replace(")/", ""));
	var ConvDate = new Date(getdate);
	var month = parseInt(ConvDate.getMonth()) + 1;
	return ConvDate.getFullYear() + "/" + month + "/" + ConvDate.getDate();
}

$(document)
	.ready(

		function () {
			$("#select_all").change(function () { // "select all"
				// change

				$(".checkbox").each(function () {
					if ($("#select_all").is(':checked') == false) {
						$(this).prop('checked', false); // change all
						// ".checkbox"
						// checked
						// status
					} else {
						$(this).prop('checked', true);
					}
				});
			});
			$
				.ajax({
					url: "/Admin/ConfirmRegistration/GetAllList",
					type: "Post",
					data: {
						month: month,
						year: year
					},

					success: function (res) {
						var data = "";
						for (var i = 0; i < res.length; i++) {
							var split1 = res[i].Registration_Date.split("(");
							var split2 = split1[1].split(")");
							var dateRegistration = convertDate(split2[0]);
							if (res[i].Status_flag == 'Bị Hủy'
								|| res[i].Status_flag == 'Đã duyêt') {
								data += "<tr><td></td><td hidden>"
									+ res[i].ot_Id
									+ "</td><td  >"
									+ res[i].employee_Name
									+ "</td><td>"
									+ dateRegistration
									+ "</td><td>"
									+ res[i].Time_Start.Hours + ":" + res[i].Time_Start.Minutes + ":00"
									+ "</td><td>"
									+ res[i].Time_Finish.Hours + ":" + res[i].Time_Finish.Minutes + ":00"
									+ "</td><td>"
									+ res[i].Reason
									+ "</td><td>"
									+ res[i].Reason_For_Cancel
									+ "</td><td>"
									+ res[i].Status_flag
									+ "</td><td></td></tr>";

							} else {
								data += "<tr><td><div class='checkbox'>"
									+ "<label> <input class='checkbox check_id' data-id='"
									+ res[i].ot_Id
									+ "' type='checkbox' name='check[]'>"
									+ "</label>"
									+ "</div></td><td hidden>"
									+ res[i].ot_Id
									+ "</td><td>"
									+ res[i].employee_Name
									+ "</td><td>"
									+ dateRegistration
									+ "</td><td>"
									+ res[i].Time_Start.Hours + ":" + res[i].Time_Start.Minutes + ":00"
									+ "</td><td>"
									+ res[i].Time_Finish.Hours + ":" + res[i].Time_Finish.Minutes + ":00"
									+ "</td><td>"
									+ res[i].Reason
									+ "</td><td>"
									+ res[i].Reason_For_Cancel
									+ "</td><td>"
									+ res[i].Status_flag
									+ "</td><td><button type='button' class='btn btn-outline-danger'"
									+ " data-target='#myModalAdd1'  onclick=functionEdit()"
									+ " data-toggle='modal' >Hủy</button></td></tr>";
							}
						}
						$('#allRegistration').html(data);
						//$('#table').excelTableFilter();
					},
					error: function () {
						swal("Error", "Your imaginary file is safe :)", "error");
					}

				});
		});

function PagitrationByMonth(id, index) {
	temp;
	var link;
	if (month == 1 && id == 'PreviousByMonth') {
		month = 12;
		year = year - 1;
		link = "/Admin/ConfirmRegistration/" + id;
	} else {
		if (month == 12 && id == 'NextByMonth') {
			month = 1;
			year = year + 1;
			link = "/Admin/ConfirmRegistration/" + id;
		} else {
			month += parseInt(index);
			link = "/Admin/ConfirmRegistration/" + id;

		}
	}
	$
		.ajax({
			url: link,
			type: "Post",
			data: {
				temp: temp,
				month: month,
				year: year
			},
			success: function (res) {
				var data = "";
				for (var i = 0; i < res.length; i++) {
					var split1 = res[i].Registration_Date.split("(");
					var split2 = split1[1].split(")");
					var dateRegistration = convertDate(split2[0]);
					if (res[i].Status_flag == 'Bị Hủy' || res[i].Status_flag == 'Đã duyêt') {
						data += "<tr><td></td><td hidden>" + res[i].ot_Id
							+ "</td><td  >" + res[i].employee_Name
							+ "</td><td>" + dateRegistration
							+ "</td><td>" + res[i].Time_Start.Hours + ":" + res[i].Time_Start.Minutes + ":00"
							+ "</td><td>" + res[i].Time_Finish.Hours + ":" + res[i].Time_Finish.Minutes + ":00"
							+ "</td><td>" + res[i].Reason + "</td><td>"
							+ res[i].Reason_For_Cancel + "</td><td></td></tr>";

					} else {
						data += "<tr><td><div class='checkbox'>"
							+ "<label> <input class='checkbox check_id' data-id='"
							+ res[i].ot_Id
							+ "' type='checkbox' name='check[]'>"
							+ "</label>"
							+ "</div></td><td hidden>"
							+ res[i].ot_Id
							+ "</td><td>"
							+ res[i].employee_Name
							+ "</td><td>"
							+ dateRegistration
							+ "</td><td>"
							+ res[i].Time_Start.Hours + ":" + res[i].Time_Start.Minutes + ":00"
							+ "</td><td>"
							+ res[i].Time_Finish.Hours + ":" + res[i].Time_Finish.Minutes + ":00"
							+ "</td><td>"
							+ res[i].Reason
							+ "</td><td>"
							+ res[i].Reason_For_Cancel
							+ "</td><td><button type='button' class='btn btn-outline-danger' data-target='#myModalAdd1'  onclick=functionEdit() data-toggle='modal' >Hủy</button></td></tr>";
					}
				}
				$('#allRegistration').html(data);
				$(".dropdown-filter-dropdown").each(function () {
					$(this).remove();
				});
				//$('#table').excelTableFilter();
			},
			error: function () {
				swal("Error", "Your imaginary file is safe :)", "error");
			}
		});
}

function functionEdit() {
	var table = document.getElementById('table');
	for (var i = 0; i < table.rows.length; i++) {
		table.rows[i].onclick = function name() {
			document.getElementById('otId').value = this.cells[1].innerHTML;
		};
	}
};

function cancelRegistration() {
	var a = document.getElementById("otId").value;
	var b = document.getElementById("reasonForCancel").value;
	$.ajax({
		url: "/Admin/ConfirmRegistration/RefuseRegistration",
		type: "POST",
		data: {
			otId: a,
			reasonForCancel: b
		},
		success: function (data) {
			localStorage.setItem("swal",
				swal({
					title: "Success!",
					text: "Message sent",
					type: "success",
					timer: 800,
					showConfirmButton: false
				})
			);
			window.setTimeout(function () {
				location.reload();
			}, 800);
		},
		error: function () {
			swal("Error", "Your imaginary file is safe :)", "error");
		}
	});
}

function Submit() {
	var arrId = new Array();
	$(".check_id:checked").each(function (i) {
		arrId[i] = $(this).attr("data-id");
	});
	$.ajax({
		url: "/Admin/ConfirmRegistration/SubmitRegistration",
		type: "POST",
		data: {
			arrId: arrId
		},
		success: function (res) {
			localStorage.setItem("swal",
				swal({
					title: "Success!",
					text: "Message sent",
					type: "success",
					timer: 800,
					showConfirmButton: false
				})
			);
			window.setTimeout(function () {
				location.reload();
			}, 800);
		},
		error: function () {
			swal("Lỗi!", "Vui lòng chon trước khi Submit :)", "error");
		}
	});
}

temp = 0;
function functionSearch(searchId, statusFlag) {
	month = current.getMonth() + 1;
	var link = "/Admin/ConfirmRegistration/" + searchId;
	$
		.ajax({
			url: link,
			type: "POST",
			data: {
				statusFlag: statusFlag
			},
			success: function (res) {
				var data = "";
				for (var i = 0; i < res.length; i++) {
					var split1 = res[i].Registration_Date.split("(");
					var split2 = split1[1].split(")");
					var dateRegistration = convertDate(split2[0]);
					var status;
					if (res[i].Status_flag == 1)
						status = "Đã duyệt";
					else {
						if (res[i].Status_flag == 2)
							status = "Chờ duyệt";
						else status = "Bị hủy";
					}
					data += "<tr><td></td><td hidden>"
						+ res[i].OT_ID
						+ "</td><td>"
						+ res[i].Time_Start.Hours + ":" + res[i].Time_Start.Minutes + ":00"
						+ "</td><td>"
						+ dateRegistration
						+ "</td><td>"
						+ res[i].Time_Start.Hours + ":" + res[i].Time_Start.Minutes + ":00"
						+ "</td><td>"
						+ res[i].Time_Finish.Hours + ":" + res[i].Time_Finish.Minutes + ":00"
						+ "</td><td>"
						+ res[i].Reason + "</td><td>"
						+ status + "</td><td></td></tr>";
				}
				$('#allRegistration').html(data);
				$(".dropdown-filter-dropdown").each(function () {
					$(this).remove();
				});
				$('#table').excelTableFilter();
			},
			error: function () {
				swal("Error", "Your imaginary file is safe :)", "error");
			}
		});
}

function exportExcel1() {
	$("#year").val(year);
	$("#temp").val(temp);
	$("#month").val(month);
	$("#btn-download").parent().submit();
}
