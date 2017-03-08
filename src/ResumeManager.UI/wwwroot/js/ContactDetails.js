
(function () {
    $(document).ready(function () {
       

        $.validator.setDefaults({
            ignore: ""
        });

        $("#LanguageList").kendoMultiSelect({
            placeholder: "Select Languages ..."
        });

        //Profile Photo Preview
        $(".image-preview-input input:file").change(function () {
            var file = this.files[0];
            var reader = new FileReader();
            // Set preview image 
            reader.onload = function (e) {
                $(".thumbnail.img-preview").attr("src", e.target.result);
                $("#uploadedPhoto").attr("val", e.target.result);
            };
            reader.readAsDataURL(file);
        });

        //Remove profile photo
        $("#clearPhoto").click(function () {
            $(".thumbnail.img-preview").attr("src", "/images/user-default.png");
            $("#uploadedPhoto").val("");            
            var data = {
                resumeId: resumeId //comming from _contactDetails partial
            };
            $.ajax({
                type: "POST",
                url: "/ResumeDraft/RemovePhoto",
                data: data
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
    });
})();