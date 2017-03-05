(function () {
    $(document).ready(function () {
        //default settings for the notifications
        $.notifyDefaults({
            allow_dismiss: false,
            placement: {
                from: "top",
                align: "center"
            },
            animate: {
                enter: "animated bounceInDown",
                exit: "animated bounceOutUp"
            },
            delay: 800,
            offset: {
                y: 100
            }
        });

        $.validator.setDefaults({
            ignore: ""
        });


        $("#LanguageList").kendoMultiSelect({
            placeholder: "Select Languages ..."
        });
        //photo js
        $(".image-preview-input input:file").change(function () {
            var file = this.files[0];
            var reader = new FileReader();
            // Set preview image 
            reader.onload = function(e) {
                $(".thumbnail.img-preview").attr("src", e.target.result);
                $("#uploadedPhoto").attr("val", e.target.result);
            };
            reader.readAsDataURL(file);
        });
        //Remove profile photo
        $("#clearPhoto").click(function () {
            $(".thumbnail.img-preview").attr("src", "/images/user-default.png");
            $("#uploadedPhoto").val("");
            $.ajax({
                type: "POST",
                url: "/Resume/RemovePhoto"
            });
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
        //and saving the form and the photo if all
        //fields are correct
        $("#contactDetails").click(function () {
            var form = this.closest("form");
            var result = validator.validate();
            $(form).validate();
            if (!$(form).valid() || !result) {
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
                };
                var saveForm = $.ajax({
                    type: "POST",
                    url: "/Resume/SaveContactDetails",
                    data: {
                        model
                    }
                }),
                    savePhoto = saveForm.then(function () {
                        var data = new FormData();
                        var files = $("#uploadedPhoto").get(0).files;
                        // Add the uploaded image content to the form data collection
                        if (files.length > 0) {
                            data.append("photo", files[0]);
                            return $.ajax({
                                type: "POST",
                                url: "/Resume/SavePhoto",
                                contentType: false,
                                processData: false,
                                data: data
                            });
                        } else {
                            return null;
                        }
                    });

                savePhoto.done(function () {
                    $.notify({
                        icon: "fa fa-check",
                        title: "<strong>Success: </strong>",
                        message: "Contact details correctly saved :)"
                    }, {
                        type: "success"
                    });
                });
            }
        });
    });
})();