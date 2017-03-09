﻿(function () {
    var source = $("#qualifications-template").html();
    var template = Handlebars.compile(source);

    //Handlebars Helper
    Handlebars.registerHelper("formatDate", function (datetime, format) {
        if (moment) {
            return moment(datetime).format(format);
        }
        else {
            return datetime;
        }
    });

    $(document).ready(function () {

        $("#DateAquired").kendoDatePicker({
            value: new Date(),
            start: "year",
            depth: "year",
            format: "MMMM yyyy"
        });

        $("#DateAquiredModal").kendoDatePicker({
            value: new Date(),
            start: "year",
            depth: "year",
            format: "MMMM yyyy"
        });

        function listQualifications() {
            //get qualifications from DB
            var getQualifications = $.ajax({
                type: "get",
                url: "/ResumeDraft/GetQualifications",
                data: { resumeDraftId: resumeDraftId }
            });
            getQualifications.done(function (qualList) {
                if (qualList.length > 0) {
                    var context = { qualification: qualList };
                    var html = template(context);
                    $("#QualificationListContainer").empty().html(html);
                }
                else {
                    $("#QualificationListContainer").empty();
                }
            });
        }

        $("#addQualification").click(function () {
            var data = {
                resumeDraftId: resumeDraftId,
                name: $("#Name").val(),
                type: $("#Type").val(),
                institution: $("#Institution").val(),
                dateAquired: $("#DateAquired").val(),
                otherInfo: $("#OtherInfo").val()
            };
            var addQualification = $.ajax({
                type: "post",
                url: "/ResumeDraft/AddQualification",
                data: data
            });
            addQualification.done(function () {
                $("#Name").val("");
                $("#Type").val("");
                $("#Institution").val("");
                $("#OtherInfo").val("");
                $("#DateAquired").data('kendoDatePicker').value(new Date());
                listQualifications();
            });
        });


        $(document).on("click", ".removeQual", function () {
            var draftQualId = $(this).data("id");
            var removeQualification = $.ajax({
                type: "post",
                url: "/ResumeDraft/RemoveQualification",
                data: {
                    draftQualificationId: draftQualId
                }
            });
            removeQualification.done(function () {
                listQualifications();
            });
        });

        $(document).on("click", ".editQual", function () {
            var draftQualId = $(this).data("id");
            var editQualification = $.ajax({
                type: "get",
                url: "/ResumeDraft/GetDraftQualification",
                data: {
                    draftQualificationId: draftQualId
                }
            });
            editQualification.done(function (data) {
                $("#NameModal").val(data.name);
                $("#NameModal").val(data.name);
                $("#QualId").val(data.id);
                $("#TypeModal").val(data.type);
                $("#InstitutionModal").val(data.institution);
                $("#OtherInfoModal").val(data.otherInfo);
                $("#DateAquiredModal").data('kendoDatePicker').value(data.dateAquired);
            });
        });

        $(document).on("click", "#saveChangesModal", function () {
            var draftQualId = $("#QualId").val();
            var qualData = {
                draftQualificationId: draftQualId,
                name: $("#NameModal").val(),
                type: $("#TypeModal").val(),
                institution: $("#InstitutionModal").val(),
                dateAquired: $("#DateAquiredModal").val(),
                otherInfo: $("#OtherInfoModal").val()
            };
            var updateQualification = $.ajax({
                type: "post",
                url: "/ResumeDraft/UpdateDraftQualification",
                data: {
                    model: qualData
                }
            });
            updateQualification.done(function () {
                $('#qualification-editor').modal('hide')
                listQualifications();
            });
        });
        listQualifications();
    });
})();