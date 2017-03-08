(function () {
    var source = $("#some-template").html();
    var template = Handlebars.compile(source);
    $(document).ready(function () {
        function listSkills() {
            //get skills from DB
            var getSkills = $.ajax({
                type: "GET",
                url: "/Resume/GetSkills"
            });
            getSkills.done(function (skillsList) {
                var context = { skills: skillsList };
                var html = template(context);
                $("#content-placeholder").empty().html(html);
            });
        }
        $("#addSkill").click(function () {
            var form = this.closest("form");
            $(form).validate();
            if (!$(form).valid()) {
                return;
            }
            var addSkill = $.ajax({
                type: "POST",
                url: "/Resume/AddSkill",
                data: {
                    skill: $("#CoreSkills").val()
                }
            });
            addSkill.done(function () {
                $("#CoreSkills").val("");
                listSkills();
            });
        });
        listSkills();

        $(document).on("click", ".iconRemove", function () {
            var name = $(this).data("value");
            var removeSkill = $.ajax({
                type: "POST",
                url: "/Resume/RemoveSkill",
                data: {
                    skill: name
                }
            });
            removeSkill.done(function () {
                listSkills();
            });
        });
        $(document).on("click", ".iconEdit", function () {
            var name = $(this).data("value");
            var editSkill = $.ajax({
                type: "POST",
                url: "/Resume/EditSkill",
                data: {
                    skill: name
                }
            });
            editSkill.done(function () {
                listSkills();
            });
        });
        $("#CoreSkills").kendoAutoComplete({
            placeholder: "Select skill ...",
            filter: "startswith",
            minLength: 1,
            dataSource: {
                type: "JSON",
                pageSize: 100,
                transport: {
                    read: {
                        url: "/Resume/getAllSkills",
                        type: "GET"
                    }
                }
            }
        });
    });
})();
