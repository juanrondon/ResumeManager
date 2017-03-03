(function () {
    $(document).ready(function () {
        $.validator.setDefaults({ ignore: "" });

        $("#successAlertContactDetails").hide();

        $("#LanguageList").kendoMultiSelect({
            placeholder: "Select Languages ..."
        });
        //photo js
        $(".image-preview-input input:file").change(function () {
            var file = this.files[0];
            var reader = new FileReader();
            // Set preview image 
            reader.onload = function (e) {
                $(".thumbnail.img-preview").attr("src", e.target.result);
                $("#uploadedPhoto").attr("val", e.target.result);
            }
            reader.readAsDataURL(file);
        });
        //Remove profile photo
        $("#clearPhoto").click(function () {
            $(".thumbnail.img-preview").attr("src", "/images/user-default.png");
            $("#uploadedPhoto").val("");

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


        //Validation contact details section
        $("#contactDetails").click(function () {
            var form = $("#contactDetails").closest("form");
            var valid = true;
            valid = validator.validate();
            valid = $(form).validate();
            if (!$(form).valid()) {
                return;
            } else {
                var model = {
                    FirstName: $("#FirstName").val(),
                    LastName: $("#LastName").val(),
                    Email: $("#Email").val(),
                    Mobile: $("#Mobile").val(),
                    Address: $("#Address").val(),
                    GitHub: $("#GitHub").val(),
                    LinkedIn: $("#LinkedIn").val(),
                    LanguageListIds: $("#LanguageList").val()
                }
                $.ajax(
                {
                    type: "POST", //HTTP POST Method
                    url: "/Resume/SaveContactDetails", // Controller/View   
                    data: { model },
                    success: function () {
                        //upload photo
                        var data = new FormData();
                        var files = $("#uploadedPhoto").get(0).files;
                        // Add the uploaded image content to the form data collection
                        if (files.length > 0) {
                            data.append("photo", files[0]);
                        }
                        $.ajax(
                        {
                            type: "POST", //HTTP POST Method
                            url: "/Resume/SavePhoto", // Controller/View   
                            contentType: false,
                            processData: false,
                            data: data,
                            success: function () {
                                $("#successAlertContactDetails").alert();
                                $("#successAlertContactDetails").fadeTo(3000, 30).slideUp(500, function () {
                                    $("#successAlertContactDetails").slideUp(500);
                                });
                            }
                        });
                    }
                });
            }
        });
    });
})();