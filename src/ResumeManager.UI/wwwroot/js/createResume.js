(function () {
    $(document).ready(function () {
        $.validator.setDefaults({ ignore: "" });

        $("#references").wysiwyg({
            
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