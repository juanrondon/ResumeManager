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
                var context = { skills: skillsList };
                var html = template(context);
                $("#skillsListContainer").empty().html(html);
            });
        }
        $("#addSkill").click(function () {
            $("#addSkillError").html("");
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
            addSkill.fail(function (jqXHR, textStatus, errorThrown) {
                $("#Skill").val("");
                $("#addSkillError").html(jqXHR.responseJSON.error);
                listSkills();
            });
        });

        $(document).on("click", ".iconRemove", function () {
            var name = $(this).data("value");
            var removeSkill = $.ajax({
                type: "post",
                url: "/ResumeDraft/RemoveSkill",
                data: {
                    resumeDraftId: resumeDraftId,
                    skill: name
                }
            });
            removeSkill.done(function () {
                listSkills();
            });
        });

        $("#Skill").kendoAutoComplete({
            placeholder: "Select skill ...",
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