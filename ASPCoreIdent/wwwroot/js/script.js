$('.message a').click(function(){
   $('form').animate({height: "toggle", opacity: "toggle"}, "slow");
});

/*File*/
$('.my').change(function () {
    if ($(this).val() != '') $(this).prev().text('Выбрано файлов: ' + $(this)[0].files.length);
    else $(this).prev().text('Choose files');
});