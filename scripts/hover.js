$(function(){
   
$("#nav_container").css({'opacity':'0'});
  $(".slides_container").hover(function() {
    $('#nav_container').stop().fadeTo(500, 3);
      },
        function() {
        $('#nav_container').stop().fadeTo(500, 0);
        });
        $("#nav_container").hover(function() {
        $('#nav_container').stop().fadeTo(500, 3);                                  
        },
        function() {
        $('#nav_container').stop().fadeTo(500, 0);
        });
});    
