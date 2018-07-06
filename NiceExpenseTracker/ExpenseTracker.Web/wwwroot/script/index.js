(function ($) {

    var progressBar = $('.progress');
    var errormsg = $('.alert-primary');
    var successmsg = $('.alert-success');
    var name = $('#text-name');
    var amount = $('#text-amount');
    var category = 'unknown';
    var date = $('#text-date');
    var expenseApiUrl = "http://localhost:60299/api/expense"

    $('.data-category button').on("click", function () {
        category = $(this).attr('data-value')
    });

    $("form").submit(function (event) {

        var dataToPost = {
            "name": name.val(),
            "amount": amount.val(),
            "category": category,
            "dateSubmitted": date.val()
        }

        clearMessagesAndShowSpinner();

        var jqxhr = $.ajax({
            url: expenseApiUrl,
            type: "POST",
            data: JSON.stringify(dataToPost),
            contentType: 'application/json',
        })
            .done(function () {
                progressBar.hide();
                successmsg.slideDown("slow");
            })
            .fail(function (error) {
                progressBar.hide();
                errormsg.slideDown("slow");
            })

        event.preventDefault();

    });

    var clearMessagesAndShowSpinner = function () {
        progressBar.show();
        successmsg.hide();
        errormsg.hide();
    }

})(jQuery);