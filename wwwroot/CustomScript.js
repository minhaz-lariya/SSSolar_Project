function SuccessAlert(Message) {
	toastr.success(Message);
}

function DangerAlert(Message) {
	toastr.error(Message);
}



function isRequired(txtName, Cnt) {
	const Value = $("#" + txtName).val();
	if (Value == "") {
		Cnt++;
		$("#" + txtName).css("border", "1px solid red");
		$("#" + txtName).prop("title", "Field is Required");
	}
	else {
		$("#" + txtName).css("border", "1px solid #eee");
		$("#" + txtName).prop("title", "");
	}

	return Cnt;
}

function isEmailValidation(txtName, Cnt) {

	var mailformat = /^\w+([\.-]?\w+)*@@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
	var Email = $("#" + txtName).val();

	if (Email != "") {
		if (Email.match(mailformat)) {
			$("#" + txtName).css("border", "1px solid #eee");
			$("#" + txtName).prop("title", "");
		}
		else {
			Cnt++;
			$("#" + txtName).css("border", "1px solid red");
			$("#" + txtName).prop("title", "Invalid Email");
		}
	}
	return Cnt;
}

function comparePassword(txtPasswd, txtConfirm, Cnt) {
	const passwd = $("#" + txtPasswd).val();
	const confirmPasswd = $("#" + txtConfirm).val();

	if (passwd != "" && confirmPasswd != "") {

		if (passwd != confirmPasswd) {
			Cnt++;
			$("#" + txtConfirm).css("border", "1px solid red");
			$("#" + txtConfirm).prop("title", "Password is not match");
		}
		else {
			$("#" + txtConfirm).css("border", "1px solid #eee");
			$("#" + txtConfirm).prop("title", "");
		}
	}

	return Cnt;
}