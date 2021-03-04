$(document).ready(function () {
    loadData();
});
function loadData() {
    $.ajax({
        url: "/EmployeeInformaion/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Age + '</td>';
                html += '<td>' + item.District + '</td>';
                html += '<td>' + item.Country + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | <a href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Add() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        Age: $('#Age').val(),
        District: $('#District').val(),
        Country: $('#Country').val()
    };
    $.ajax({
        url: "/EmployeeInformaion/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getbyID(ID) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
    $('#District').css('border-color', 'lightgrey');
    $('#Country').css('border-color', 'lightgrey');
    $.ajax({
        url: "/EmployeeInformaion/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ID').val(result.ID);
            $('#Name').val(result.Name);
            $('#Age').val(result.Age);
            $('#District').val(result.District);
            $('#Country').val(result.Country);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Update() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        Age: $('#Age').val(),
        District: $('#District').val(),
        Country: $('#Country').val()
    };
    $.ajax({
        url: "/EmployeeInformaion/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ID').val("");
            $('#Name').val("");
            $('#Age').val("");
            $('#District').val("");
            $('#Country').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/EmployeeInformaion/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function clearTextBox() {
    $('#ID').val("");
    $('#Name').val("");
    $('#Age').val("");
    $('#District').val("");
    $('#Country').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
    $('#District').css('border-color', 'lightgrey');
    $('#Country').css('border-color', 'lightgrey');
}
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() === "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Age').val().trim() === "") {
        $('#Age').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Age').css('border-color', 'lightgrey');
    }
    if ($('#District').val().trim() === "") {
        $('#District').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#District').css('border-color', 'lightgrey');
    }
    if ($('#Country').val().trim() === "") {
        $('#Country').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Country').css('border-color', 'lightgrey');
    }
    return isValid;
}