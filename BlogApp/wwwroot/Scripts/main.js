function loaderShow() {
    $("#laoder").removeClass('tp-hide');
}
function loaderHide() {
    $("#laoder").addClass('tp-hide');
}
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'UA-153182251-1');
window.fbAsyncInit = function () {
    FB.init({
        appId: '3242127552528704',
        status: true,
        cookie: true,
        xfbml: true,
        version: 'v2.4'
    });
};
(function () {
    var e = document.createElement('script'); e.async = true;
    e.src = document.location.protocol +
        '//connect.facebook.net/en_US/sdk.js';
    document.getElementById('head').appendChild(e);

}());
(function () {
    $.ajax({
        url: '/NewsType/menuLayout',
        dataType: 'html',
        contentType: "application/html; charset=utf-8",
        success: function (data, status, xhr) {
            $('#dvmenu').html(data);
        },
        error: function (data, status, xhr) {

        }
    });
 $('body,html').animate({
     scrollTop: 0
 }, 800);
 $(window).scroll(function () {
     if ($(this).scrollTop() > 50) {
         $('#btntop').removeClass('tp-hide');
         $('#btntop').addClass('tp-show');
     }
     else {
         $('#btntop').removeClass('tp-show');
         $('#btntop').fadeOut('tp-hide');
     }
 });
 $('#btntop').click(function (e) {
     $('body,html').animate({
         scrollTop: 0
     }, 800);
 });
    $('#btnSubmit').click(function (e) {
        if ($('#name').val() == "") {
            alert('Please Enter Your Name');
            return;
        }
        if ($('#txtphone').val() == "") {
            alert('Please Enter Your Phone No');
            return;
        }
        if ($('#txtmsg').val() == "") {
            alert('Please Enter Your Message');
            return;
        }
     var obj = {};
     obj.Name = $('#name').val();
     obj.Email = $('#txtemail').val();
     obj.Phone = $('#txtphone').val();
     obj.Message = $('#txtmsg').val();
     $.ajax({
         url: '/Home/SendEmail',
         type: 'Post',
         data: JSON.stringify(obj),
         dataType: 'json',
         contentType: "application/json",
         success: function (data, status, xhr) {
             $('#name').val('');
             $('#txtemail').val('');
             $('#txtphone').val('');
             $('#txtmsg').val('');
             alert("We have receieved your concern ! our team well get back to you shortly..");
         },
         error: function (data, status, xhr) {

         }
     });
 });
})();

