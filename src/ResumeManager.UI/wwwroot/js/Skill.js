(function () {
    var source = $("#skills-template").html();
    var template = Handlebars.compile(source);

    $(document).ready(function () {
        function listSkills() {
            //get skills from DB
            var getSkills = $.ajax({
                type: "get",
                url: "/ResumeDraft/GetSkills",
                data: { resumeDraftId: resumeDraftId }
            });
            getSkills.done(function (skillsList) {
                if (skillsList.length > 0) {
                    var context = { skills: skillsList };
                    var html = template(context);
                    $("#skillsListContainer").empty().html(html);
                } else {
                    $("#skillsListContainer").empty();
                }
            });
        }
        $("#addSkill").click(function (e) {
            e.preventDefault();
            if (!$("#createResumeDraftForm").valid())
                return;
            var name = $("#Skill").val();
            var addSkill = $.ajax({
                type: "get",
                url: "/ResumeDraft/AddSkill",
                data: {
                    resumeDraftId: resumeDraftId,
                    skill: name
                }
            });
            addSkill.done(function () {
                $("#Skill").val("");
                listSkills();
            });
            addSkill.fail(function (errors) {
                displayErrors(errors, "Error");
                listSkills();
            });
        });

        $(document).on("click", ".removeSkill", function () {
            var id = $(this).data("id");
            var removeSkill = $.ajax({
                type: "post",
                url: "/ResumeDraft/RemoveSkill",
                data: {
                    skillId: id
                }
            });
            removeSkill.done(function () {
                listSkills();
            });
        });

        $("#Skill").kendoAutoComplete({
            placeholder: "Type your skill ...",
            filter: "startswith",
            minLength: 1,
            dataSource: {
                pageSize: 100,
                transport: {
                    read: {
                        url: "/ResumeDraft/GetPreloadedSkills",
                        type: "get"
                    }
                }
            }
        });
        listSkills();
    });
})();