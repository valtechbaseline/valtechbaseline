$(document).ready(function ($) {

    $(".searchGoalTracking").click(function (e) {
        e.preventDefault();
        var redirectLink = $(this).attr("href");
        $.ajax({
            url: '/api/sitecore/Search/SearchedArticleGoalTrigger',
            type: "POST",
            dataType: "html",
            cache: false,
            success: function (data) {
                //alert("goal");
                window.location=redirectLink;
                $("#vbDealsPopup").attr("style", "");
            },
            error: function (response) {
                alert("Something Bad Happened!");
            }
        });
    });
	
});
