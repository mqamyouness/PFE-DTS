$(function (){
   $('.user_profile .user_profile_body > header .item').mouseover(function (){
        index = $(this).attr('profile-item-index');
        $('.user_profile .user_profile_body > header .item').removeClass('item_hover');
        
       $(this).addClass('item_hover');
       
       $('.user_profile .user_profile_body .user_info_item:visible').hide();
       $('.user_profile .user_profile_body .user_info_item').eq(index).show();
    });
});