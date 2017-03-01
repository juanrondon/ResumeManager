(function () {
    $(document).ready(function () {
        $.validator.setDefaults({ ignore: "" });

        $("#languageList").kendoMultiSelect({
            placeholder: "Select Languages ...",
            dataTextField: "name",
            dataValueField: "languageId",
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: "/resume/GetLanguages",
                        contentType: "application/json; charset=utf-8",
                        type: "GET",
                        dataType: "json"
                    }
                }
            }
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
            validator.validate();
            $("#createResumeFormId").validate().element("#FirstName");
            $("#createResumeFormId").validate().element("#LastName");
            $("#createResumeFormId").validate().element("#Email");
            $("#createResumeFormId").validate().element("#Mobile");
            $("#createResumeFormId").validate().element("#Address");
        });

        var $imageupload = $(".imageupload");
        $imageupload.imageupload({
            allowedFormats: ["jpg", "png"],
            maxWidth: 150,
            maxHeight: 150,
            maxFileSizeKb: 512
        });
    });
})();