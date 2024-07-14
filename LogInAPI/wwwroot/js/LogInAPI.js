// 需要 jQuery
// import { jQuery } from "./jquery-3.7.1";

function SignUp(inputFormID, redirectURL) {
    var formID = "#" + inputFormID
    $(formID).submit(function (event) {
        event.preventDefault();

        var forms = $(this);

        $.ajax({
            type: document.getElementById(inputFormID).getAttribute("method"),
            url: document.getElementById(inputFormID).getAttribute("action"),
            data: forms.serialize(),
            success: function (response) {
                alert(response.message);
                window.location = redirectURL;
            }
        })
    });
};

function LogIn(inputFormID, redirectURL) {
    var formID = "#" + inputFormID
    $(formID).submit(function (event) {
        event.preventDefault();

        var forms = $(this);

        $.ajax({
            type: document.getElementById(inputFormID).getAttribute("method"),
            url: document.getElementById(inputFormID).getAttribute("action"),
            data: forms.serialize(),
            success: function (response) {
                var id = response.RegistrantID;
                window.location = redirectURL + "?" + "id=" + id;
            }
        })
    });
};

function LogOut(redirectURL) {
    $.ajax({
        type: "get",
        url: "/api/Registrant/LogOut",
        success: function (response) {
            window.location = redirectURL
        }
    })
}
