(function () {
    var source = $("#Experiences-template").html();
    var template = Handlebars.compile(source);
    $(document).ready(function () {

        $("#Location,#LocationModal").kendoAutoComplete({
            placeholder: "Location (ex. Australia)",
            filter: "startswith",
            minLength: 1,
            dataSource: {
                pageSize: 100,
                transport: {
                    read: {
                        url: "/DraftExperience/GetPreloadedLocations",
                        type: "get"
                    }
                }
            }
        });


        $("#StartDate,#StartDateModal").kendoDatePicker({
            max: new Date(),
            min: new Date(1960, 1, 1),
            start: "year",
            depth: "year",
            format: "mm/yyyy"
        });

        $("#EndDate,#EndDateModal").kendoDatePicker({
            min: new Date(1960, 1, 1),
            start: "year",
            depth: "year",
            format: "mm/yyyy"
        });

        function listExperiences() {
            //get experiences from DB
            var getExperiences = $.ajax({
                type: "get",
                url: "/DraftExperience/GetExperiences",
                data: { resumeDraftId: resumeDraftId }
            });
            getExperiences.done(function (expList) {
                
                if (expList.length > 0) {
                    var context = { experience: expList };
                    var html = template(context);
                    $("#ExperienceListContainer").empty().html(html);
                }
                else {
                    $("#ExperienceListContainer").empty();
                }
            });
            getExperiences.fail(function (error) {
                debugger;
            })
        }

        //Add Experience 
        $("#addExperience").click(function (e) {
            e.preventDefault();
            if (!$("#createResumeDraftForm").valid())
                return;

            var data = {
                resumeDraftId: resumeDraftId,
                Title: $("#Title").val(),
                Company: $("#Company").val(),
                Location: $("#Location").val(),
                StartDate: $("#StartDate").val(),
                EndDate: $("#EndDate").val(),
                CurrentlyWorking: $("#CurrentlyWorking").val(),
                Description: $("#Description").val()
            };
            var addExperience = $.ajax({
                type: "post",
                url: "/DraftExperience/AddExperience",
                data: data
            });
            addExperience.done(function () {
                $("#Title").val("");
                $("#Company").val("");
                $("#Location").val("");
                $("#StartDate").val("");
                $("#EndDate").val("");
                $("#CurrentlyWorking").val("");
                $("#Description").val("");
                listExperiences();
            });
            addExperience.fail(function (errors) {
                displayErrors(errors, "Error");
            });
        });

        //Delete experience
        $(document).on("click", ".removeExp", function () {
            var draftExpId = $(this).data("id");
            var removeExperience = $.ajax({
                type: "post",
                url: "/DraftExperience/RemoveExperience",
                data: {
                    draftExperienceId: draftExpId
                }
            });
            removeExperience.done(function () {
                listExperiences();
            });
        });

        //Update experience - display modal
        $(document).on("click", ".editExp", function () {
            var draftExpId = $(this).data("id");
            var editExperience = $.ajax({
                type: "get",
                url: "/DraftExperience/GetDraftExperience",
                data: {
                    draftExperienceId: draftExpId
                }
            });
            editExperience.done(function (data) {
                //Populate edit modal
                $("#TitleModal").val(data.title);
                $("#CompanyModal").val(data.company);
                $("#LocationModal").val(data.location);
                $("#StartDateModal").val(data.startDate);
                $("#EndDateModal").val(data.endDate);
                $("#CurrentlyWorkingModal").val(data.currentlyWorking);
                $("#DescriptionModal").val(data.description);
                $("#ExpId").val(data.id);
            });
        });

        //Update experience - Save changes
        $(document).on("click", "#saveChangesModal", function (e) {
            e.preventDefault();
            var titleError = $("#TitleModal").valid();
            var companyError = $("#CompanyModal").valid();
            if (!titleError || !companyError) {
                return;
            }
            var draftExpId = $("#ExpId").val();
            var expData = {
                draftExperienceId: draftExpId,
                titleModal: $("#TitleModal").val(data.title),
                companyModal: $("#CompanyModal").val(data.company),
                locationModal: $("#LocationModal").val(data.location),
                startDateModal: $("#StartDateModal").val(data.startDate),
                endDateModal: $("#EndDateModal").val(data.endDate),
                currentlyWorkingModal: $("#CurrentlyWorkingModal").val(data.currentlyWorking),
                description: $("#DescriptionModal").val()
            };
            var updateExperience = $.ajax({
                type: "post",
                url: "/DraftExperience/UpdateDraftExperience",
                data: {
                    model: expData
                }
            });
            updateExperience.done(function () {                
                $("#experience-editor").modal("hide");
                listExperiences();
            });
            updateExperience.fail(function (errors) {
                displayErrors(errors, "ModalError");
            });
        });
        listExperiences();
    });
})();