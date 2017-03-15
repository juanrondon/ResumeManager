(function () {
    var source = $("#educations-template").html();
    var template = Handlebars.compile(source);
    $(document).ready(function () {

        //display or hide Description
        $(document).on("click", ".showHide", function () {
            $(this).text(function (i, old) {
                return old === "See Description" ? "Hide Description" : "See Description";
            });
        });

        $("#FieldOfStudy,#FieldOfStudyModal").kendoAutoComplete({
            placeholder: "Field of study (ex. Business)",
            filter: "startswith",
            minLength: 1,
            dataSource: {
                pageSize: 100,
                transport: {
                    read: {
                        url: "/ResumeDraft/GetPreloadedFieldsOfStudy",
                        type: "get"
                    }
                }
            }
        });


        //Handlebars helper
        Handlebars.registerHelper("ifCond", function (v1, operator, v2, options) {
            switch (operator) {
                case "&&":
                    return (v1 && v2) ? options.fn(this) : options.inverse(this);
                default:
                    return options.inverse(this);
            }
        });

        $("#FromYear,#FromYearModal").kendoDatePicker({
            max: new Date(),
            min: new Date(1960, 1, 1),
            start: "decade",
            depth: "decade",
            format: "yyyy"
        });

        $("#ToYear,#ToYearModal").kendoDatePicker({
            min: new Date(1960, 1, 1),
            start: "decade",
            depth: "decade",
            format: "yyyy"
        });

        function listEducations() {
            //get educations from DB
            var getEducations = $.ajax({
                type: "get",
                url: "/ResumeDraft/GetEducations",
                data: { resumeDraftId: resumeDraftId }
            });
            getEducations.done(function (eduLisd) {
                if (eduLisd.length > 0) {
                    var context = { education: eduLisd };
                    var html = template(context);
                    $("#EducationListContainer").empty().html(html);
                }
                else {
                    $("#EducationListContainer").empty();
                }
            });
        }

        //Add Qualification 
        $("#addEducation").click(function (e) {
            e.preventDefault();
            if (!$("#createResumeDraftForm").valid())
                return;

            var data = {
                resumeDraftId: resumeDraftId,
                school: $("#School").val(),
                degree: $("#Degree").val(),
                fieldOfStudy: $("#FieldOfStudy").val(),
                grade: $("#Grade").val(),
                fromYear: $("#FromYear").val(),
                toYear: $("#ToYear").val(),
                description: $("#Description").val()
            };
            var addEducation = $.ajax({
                type: "post",
                url: "/ResumeDraft/AddEducation",
                data: data
            });
            addEducation.done(function () {
                $("#School").val("");
                $("#Degree").val("");
                $("#FieldOfStudy").val("");
                $("#Grade").val("");
                $("#FromYear").val("");
                $("#ToYear").val("");
                $("#ToYearError").html("");
                $("#Description").val("");
                listEducations();
            });
            addEducation.fail(function (errors) {
                displayErrors(errors, "Error");
            });
        });

        //Delete education
        $(document).on("click", ".removeEdu", function () {
            var draftEduId = $(this).data("id");
            var removeEducation = $.ajax({
                type: "post",
                url: "/ResumeDraft/RemoveEducation",
                data: {
                    draftEducationId: draftEduId
                }
            });
            removeEducation.done(function () {
                listEducations();
            });
        });

        //Update education - display modal
        $(document).on("click", ".editEdu", function () {
            var draftEduId = $(this).data("id");
            var editEducation = $.ajax({
                type: "get",
                url: "/ResumeDraft/GetDraftEducation",
                data: {
                    draftEducationId: draftEduId
                }
            });
            editEducation.done(function (data) {
                //Populate edit modal
                $("#SchoolModal").val(data.school);
                $("#DegreeModal").val(data.degree);
                $("#FieldOfStudyModal").val(data.fieldOfStudy);
                $("#GradeModal").val(data.grade);
                $("#FromYearModal").val(data.fromYear);
                $("#ToYearModal").val(data.toYear);
                $("#DescriptionModal").val(data.description);
                $("#EduId").val(data.id);
            });
        });

        //Update qualification - Save changes
        $(document).on("click", "#saveChangesModal", function (e) {
            e.preventDefault();
            var schoolError = $("#SchoolModal").valid();
            var degreeError = $("#DegreeModal").valid();
            if (!schoolError || !degreeError) {
                return;
            }
            var draftEduId = $("#EduId").val();
            var eduData = {
                draftEducationId: draftEduId,
                school: $("#SchoolModal").val(),
                degree: $("#DegreeModal").val(),
                fieldOfStudy: $("#FieldOfStudyModal").val(),
                grade: $("#GradeModal").val(),
                fromYear: $("#FromYearModal").val(),
                toYear: $("#ToYearModal").val(),
                description: $("#DescriptionModal").val()
            };
            var updateEducation = $.ajax({
                type: "post",
                url: "/ResumeDraft/UpdateDraftEducation",
                data: {
                    model: eduData
                }
            });
            updateEducation.done(function () {
                $("#ToYearModalError").html("");
                $("#education-editor").modal("hide");
                listEducations();
            });
            updateEducation.fail(function (errors) {
                displayErrors(errors, "ModalError");
            });
        });
        listEducations();
    });
})();