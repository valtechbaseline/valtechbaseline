$(document).ready(function ($) {

    $('#Countries').parent().append('<ul id="ul_Countries" name="Countries" class="lang_filter_ul"></ul>');
    $('#Countries option').each(function () {
        $('#ul_Countries').append('<li value="' + $(this).val() + '">' + $(this).text() + '</li>');
    });
    $('#ul_Countries li').on('click', function () {
        var option_value = $(this).text();
        $('#Countries option').attr('selected', false);
        $('#Countries option').filter(function () {
            return $(this).text() == option_value;
            $(this).trigger('click');
        }).attr('selected', true);
    });
    $('#ul_Countries li').on('click', function () {
        var culture = $('#Countries').val();
        $.ajax({
            cache: false,
            url: "/api/sitecore/LanguageSwitcher/ChangeCurrentCulture",
            type: "GET",
            data: { 'culture': culture },
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.Status == "Success")
                    window.location.href = window.location.href;
            },
            error: function () {

            }
        });
    });


    if(window.innerWidth > 767)
	{
		$('.menu_search i').on('click',function(){
		    $('.search_toggle').addClass('search_toggle_active');
		    if ($('.search_toggle').val().length > 0) {
		        Search();
		    }
		});
		$(".search_toggle").on("keydown", function (e) {
		    if (e.keyCode === 13 && $(this).val().length > 0) {  //checks whether the pressed key is "Enter"
		        Search();
		    }
		});
		$("html").on("click",function(e) {
			  if( $(e.target).is('#menuSearch') || $(e.target).closest('#menuSearch').length) {
					
			  }
			  else {
				 $('.search_toggle').removeClass('search_toggle_active');
			  }
		});
		
	}
	
	$('.menu_icon').click(function(e) {
		if ($(this).hasClass('close')) {
			$(this).removeClass('close');
			$(this).addClass('open');
			$('.menu_list').slideDown();
		} else {
			$(this).removeClass('open');
			$(this).addClass('close');
			$('.menu_list').slideUp();
		}
	});
	if(window.innerWidth < 768)
	{
		$("html").on("click",function(e) {
			  if( $(e.target).is('nav') || $(e.target).closest('nav').length) {
					
			  }
			  else {
				$('.menu_icon').removeClass('open');
				$('.menu_icon').addClass('close');
				$('.menu_list').slideUp();
			  }
		});
	}
	$('#login_app').click(function(e) {
		$('.langauge_inner').hide();
		$('.login_container').show();
		/*$('html,body').animate({ scrollTop: 0 }, 0);*/
	});
	/*$('.language_translator').click(function(e) {
		$('.login_container, .langauge_inner, .login_inner').hide();
		$('.login_container, .langauge_inner').slideDown();
	});*/
	$("html").on("click",function(e) {
	    if ($(e.target).is('#login_app') || $(e.target).closest('#login_app').length || $(e.target).is('.login_inner') || $(e.target).closest('.login_inner').length || $(e.target).is('.language_translator') || $(e.target).closest('.language_translator').length || $(e.target).is('.langauge_inner') || $(e.target).closest('.langauge_inner').length || $(e.target).is('.login_relative') || $(e.target).closest('.login_relative').length) {
				
		  }
		  else {
	        $('.login_container').hide();
		  }
	});
	/*$('.close_login').on('click', function () {
	    $('.login_container').slideUp();
	});*/
	$(".image_tab_section .gallery-item").on('click', function () {

        var content = $(this).html();
        $(".gallery_container .gallery-inner").html(content);
        $(".gallery_container").fadeIn();
    });

    $("html").on("click", function (e) {
        if ($(e.target).is('.gallery-item') || $(e.target).closest('.gallery-item').length || $(e.target).is('.gallery-inner') || $(e.target).closest('.gallery-inner').length ) {

        }
        else {
            $('.gallery_container').fadeOut();
        }
    });

    $("html").on("click", function (e) {
        if ($(e.target).is('#NewsLetterForm') || $(e.target).closest('#NewsLetterForm').length || $(e.target).is('#NewsletterSignup') || $(e.target).is('.newsLetter') || $(e.target).closest('.newsLetter').length || $(e.target).closest('#NewsletterSignup').length  || $(e.target).is('.newsLetter_inner') || $(e.target).closest('.newsLetter_inner').length) {

        }
        else {
            $('.newsLetter_container').hide();
        }
    });
    $("#login").click(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/sitecore/Login/DoLogin',
            type: "POST",
            dataType: "html",
            cache: false,
            data: { "emailId": $("#emailId").val(), "password": $("#password").val(), "rememberMeChk": $("#rememberMeChkBox").prop('checked') },
            success: function (data) {
                window.location.reload();
            },
            error: function (response) {
                alert("Something Bad Happened!");
            }
        });
    });
    function closeclick(){
        $(".fa-close-icon").on('click', function () {
            $(".gallery_container").fadeOut();
        });
    }
    $(".gallery-item").on('click', function () {
        closeclick();
    });
});

function Search(p1, p2) {
    var url = $('.search_toggle').data('searchresultspage');
    var searched = $('.search_toggle').val();
    window.location = url + '?searchedValue=' + searched;
}
