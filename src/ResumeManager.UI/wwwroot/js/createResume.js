(function () {
    $(document).ready(function () {
        $.validator.setDefaults({ ignore: "" });


        $("#successAlertContactDetails").hide();

        $("#languageList").kendoMultiSelect({
            placeholder: "Select Languages ..."
        });

        var validator = $("#content").kendoValidator({
            errorTemplate: "<span class='text-danger'>#=message#</span>",
            rules: {
                hasItems: function (input) {
                    if (input.is("[name=LanguageListIds]")) {
                        //Get the MultiSelect instance
                        var ms = input.data("kendoMultiSelect");
                        if (ms.value().length === 0) {
                            return false;
                        }
                    }
                    return true;
                }
            },
            messages: {
                hasItems: "Please select at least one language"
            }
        }).data("kendoValidator");


        var $imageupload = $(".imageupload");
        $imageupload.imageupload({
            allowedFormats: ["jpg", "png"],
            maxWidth: 150,
            maxHeight: 150,
            maxFileSizeKb: 512
        });

        //Validation contact details section
        $("#contactDetails").click(function () {
            var form = $("#contactDetails").closest("form");
            var valid = true;
            valid = validator.validate();
            valid = $(form).validate();
            if (!$(form).valid()) {
                return;
            } else {
                $.ajax(
                {
                    type: "POST", //HTTP POST Method  
                    url: "/Resume/SaveContactDetails", // Controller/View   
                    data: { //Passing data  
                        FirstName: $("#FirstName").val(), //Reading text box values using Jquery   
                        LastName: $("#LastName").val(),
                        Email: $("#Email").val(),
                        Mobile: $("#Mobile").val(),
                        Address: $("#Address").val(),
                        GitHub: $("#GitHub").val(),
                        LinkedIn: $("#LinkedIn").val(),
                        LanguageListIds: $("#LanguageListIds").val()
                    },
                    success: function () {
                        $("#successAlertContactDetails").alert();
                        $("#successAlertContactDetails").fadeTo(3500, 500).slideUp(500, function () {
                            $("#successAlertContactDetails").slideUp(500);
                        });
                    }
                });
            }
        });
    });
})();