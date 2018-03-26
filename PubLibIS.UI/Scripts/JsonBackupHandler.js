$(document).ready(() => {

    $(document).on('change', ':file', function () {
        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [numFiles, label]);
    });

    $(':file').on('fileselect', function (event, numFiles, label) {

        $("#browse-file-label").val(label);
        console.log(numFiles);
        console.log(label);
    });

    $("#browse-file-btn").click(() => { $(':file').click(); });

    $("#backup-button").click(() => {
        var ids = [];
        $("input:checkbox[name='JsonImport']").each((i, checkbox) => {
            if (checkbox.checked) {
                ids.push(checkbox.getAttribute("json-data"));
            }
        });
        if (ids.length > 0) {
            $.ajax({
                url: $("#json-handler").attr("urlToGetJson"),
                contentType: "application/json",
                data: JSON.stringify(ids),
                method: 'POST',
                success: (data) => {
                    $("#backup-ok").removeClass("hidden");
                    setTimeout(() => $("#backup-ok").addClass("hidden"), 2000)
                    $("input:checkbox[name='JsonImport']").each((i, checkbox) => {
                        checkbox.checked = false;
                    });
                },
                error: (data) => {
                    $("#backup-not-ok").removeClass("hidden");
                    setTimeout(() => $("#backup-not-ok").addClass("hidden"), 2000)
                }
            });
        }
     });
});