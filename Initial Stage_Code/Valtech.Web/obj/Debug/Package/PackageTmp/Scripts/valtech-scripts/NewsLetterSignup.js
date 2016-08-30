$(document).ready(function ($) {
    $('.newsLetter_container, .newsLetter_inner').hide();
    $('.newsLetter').val("");
    $('#NewsLetterEmailId').on('keyup', function () {
        if ($('#NewsLetterEmailId').val() != "") {
            $('#NewsletterSignup').removeAttr("disabled");
        } else {
            $('#NewsletterSignup').attr("disabled", "disabled");
        }
    });
    $("#NewsletterSignup").click(function (e) {

        $('.newsLetter_container, .newsLetter_inner').show();
        $("#EmailId").val($("#NewsLetterEmailId").val());
    });
   
	
});

function SuccessNewsLetter(data) {
    if (data.Status === "Success" || data.Status === "ValidationError") {
        $('.newsLetter_container').hide();
        alert(data.Message);
        $('#NewsLetterEmailId').val("");
        $('#NewsletterSignup').attr("disabled", "disabled");

    } else {
        alert(data.Message);

    }
}
