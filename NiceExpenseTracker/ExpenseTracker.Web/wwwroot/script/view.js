(function ($) {

    var progressBar = $('.progress');
    var errormsg = $('.alert-primary');
    var name = $('#text-name');
    var date = $('#text-date');
    var expenseApiUrl = "http://localhost:60299/api/expense"

    $("form").submit(function (event) {
        var dataToPost = {
            name: name.val(),
            month: new Date(date.val()).getMonth() + 1, //API considers Jan=0
            year: new Date(date.val()).getFullYear()
        }

        clearMessagesAndShowSpinner();

        var jqxhr = $.ajax({
            url: `${expenseApiUrl}/${dataToPost.name}/${dataToPost.month}/${dataToPost.year}`,
            type: "GET",
            data: dataToPost,
            dataType: "json"
        })
            .done(function (data) {               
                if (typeof data === "string") {
                    $(".chart").html("");
                    return;
                }             
            
                var expensesAvgAmount = d3.nest()
                    .key(function (d) { return d.category; })
                    .rollup(function (v) { return d3.sum(v, function (d) { return d.amount; }); })
                    .entries(data);

                progressBar.hide();
                createBarChart(expensesAvgAmount);
            })
            .fail(function (error) {
                progressBar.hide();
                errormsg.slideDown("slow");
            });


        event.preventDefault();
    });

    var clearMessagesAndShowSpinner = function () {
        progressBar.show();
        errormsg.hide();
        $(".chart").html("");
    }

    var createBarChart = function (data) {
        if (data.length == 0) {
            $(".chart").html("");
            return;
        } 

        //calculate max to set tallest bar length
        var maxLengthLimit = 800;
        var max = data.
            sort(function (a, b) { return d3.ascending(a.values, b.values); })[data.length - 1].values;
        max = max == 0 ? 1 : max;

        d3.select(".chart")
            .selectAll("div")
            .data(data)
            .enter().append("div")
            .style("width", function (d) { return d.values * (maxLengthLimit / max) + "px"; })
            .append("span")
            .text(function (d) { return `${d.key}(${d.values})`; });
    }

})(jQuery);