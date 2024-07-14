// 需要 jQuery
function LogOut(redirectURL) {
    $.ajax({
        type: "get",
        url: "/api/Registrant/LogOut",
        success: function (response) {
            window.location = redirectURL
        }
    })
}
