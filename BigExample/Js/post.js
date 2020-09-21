var arrayPost = new Array();
var checkChude = "";
var checkNoidung = "";
var checkUrl = "";

$(document).ready(function () {
	$.ajax({
		url: "/Admin/DangBai/GetAll",
		type: "POST",
		success: function (res) {
			var data = "";

			for (var i = 0; i < res.length; i++) {
				arrayPost[i] = {
					"PostId": res[i].Post_ID,
					"PostTheme": res[i].theme,
					"PostContent": res[i].content,
					"PostUrl": res[i].Urlfile
				};
			}

			for (var i = 0; i < res.length; i++) {
				data += "<tr ><td >"
					+ res[i].Post_ID
					+ "</td><td>"
					+ res[i].theme
					+ "</td><td>"
					+ res[i].content
					+ "</td><td>"
					+ res[i].Urlfile
					+ "</td><td>"
					+ "<a class='delete1' title='Delete1' data-target='#popupDeletePost' onclick = 'getIdDelete()'  class='btn btn-info btn-lg'  data-toggle='modal'><i class=' fas fa-trash' style='color: red;' /></a></td><td>"
					+ "<a  id='update-employee' class='btn btn-lg' onclick='getIdEdit()' data-target='#myPopupThemMoi' data-toggle='modal'><i class='fas fa-pencil-alt'></i></a></td></tr><hr>";

			}
			$("#tablePost").append(data);

		},
		error: function () {
			swal("Error", "change false", "error");
		}
	});
});

function getIdDelete() {
	var table1 = document.getElementById('tableAAAA');
	for (var i = 1; i < table1.rows.length; i++) {
		table1.rows[i].onclick = function name() {
			var valueID = this.cells[0].innerHTML;
			var valueName = this.cells[1].innerHTML;
			document.getElementById('PostId').value = valueID.trim();
			$('#titleDeletePost').html("bạn có muốn xóa " + valueName);

		};
	}
}



// delete row on add button click
function functionDeletePost() {
	var postId = document.getElementById("PostId").value;
	var row;
	for (var i = 0; i < arrayPost.length; i++) {
		if (postId == arrayPost[i]["Post_ID"]) {
			row = i;
		}
	}
	document.getElementById("tableAAAA").deleteRow(row + 1);
	arrayPost.splice(row, 1);
	$.ajax({
		url: "/Admin/DangBai/deletePost",
		type: "POST",
		data: {
			postId: postId,
		},
		success: function (data) {
			localStorage.setItem("swal",
				swal({
					title: "Success!",
					text: "Message Done",
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
			swal("Error", "Delete Post False", "error");
		}
	});
}

function getIdEdit() {
	var table = document.getElementById('tableAAAA');
	for (var i = 1; i < table.rows.length; i++) {
		table.rows[i].onclick = function name() {
			var valueID = this.cells[0].innerHTML;
			var valueTheme = this.cells[1].innerHTML;
			var valueContent = this.cells[2].innerHTML;
			var valueUrl = this.cells[3].innerHTML;
			document.getElementById('postIdEdit').value = valueID.trim();
			document.getElementById('chude').value = valueTheme.trim();
			document.getElementById('noidung').value = valueContent.trim();
			document.getElementById('urlFile').value = valueUrl.trim();
			$('#titleDelete').html("bạn có muốn xóa " + valueName);

		};
	}
	$(".for-addPost").each(function () {
		$(this).addClass("hide");
	});
	$(".for-updatePost").each(function () {
		$(this).removeClass("hide");
	});
}

function getAddPost() {

	document.getElementById('postIdEdit').value = "";
	document.getElementById('chude').value = "";
	document.getElementById('noidung').value = "";
	document.getElementById('urlFile').value = "";
	$(".for-updatePost").each(function () {
		$(this).addClass("hide");
	});
	$(".for-addPost").each(function () {
		$(this).removeClass("hide");
	});
}


//Add

$(document).on("click", "#submit-addPost-btn", function () {
	var valueTheme = document.getElementById("chude").value;
	var valueContent = document.getElementById("noidung").value;
	var valueUrl = document.getElementById("urlFile").value;
	$.ajax({
		url: "/Admin/DangBai/addPost",
		type: "Post",
		data: {
			valueTheme: valueTheme,
			valueContent: valueContent,
			valueUrl: valueUrl,
		},
		success: function (data) {
			localStorage.setItem("swal",
				swal({
					title: "Success!",
					text: "Message Done",
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
			swal("Error", "Add Post False??", "error");

		}

	});
});

//Edit

$(document).on("click", "#submit-updatePost-btn", function () {
	var postId = document.getElementById("postIdEdit").value;
	var valueTheme = document.getElementById("chude").value;
	var valueContent = document.getElementById("noidung").value;
	var valueUrl = document.getElementById("urlFile").value;
	$.ajax({
		url: "/Admin/DangBai/editPost",
		type: "Post",
		data: {
			postId: postId,
			valueTheme: valueTheme,
			valueContent: valueContent,
			valueUrl: valueUrl
		},
		success: function (data) {
			localStorage.setItem("swal",
				swal({
					title: "Success!",
					text: "Message Done",
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
			swal("Error", "Edit Post False ??", "error");

		}

	});
});