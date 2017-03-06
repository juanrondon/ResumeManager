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
            var addSkill = $.ajax({
                type: "POST",
                url: "/Resume/AddSkill",
                data: {
                    skill: $("#CoreSkills").val()
                }
            });
            addSkill.done(function () {
                listSkills();
            });
        });
        listSkills();
    });
})();
