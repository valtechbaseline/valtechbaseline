$(document).ready(function ($) {

    $(".gallery-item").on('click', function () {

        var content = $(this).html();
        $(".gallery_container .gallery-inner").html(content);
        $(".gallery_container").show();
    });

    $("html").on("click", function (e) {
        if ($(e.target).is('.gallery-item') || $(e.target).closest('.gallery-item').length || $(e.target).is('.gallery-inner') || $(e.target).closest('.gallery-inner').length) {

        }
        else {
            $('.gallery_container').hide();
        }
    });

	/*if(window.innerWidth > 767)
	{
	    $('.image_tab_section_common').on('click', function () {
	        $('.image_tab_section_content_common').hide();
	        var count=$(this).data("count");
	        $('.tab_section_container .image_tab_section_content_'+count).show();
	    });
	}*/
	
	
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
	
});
