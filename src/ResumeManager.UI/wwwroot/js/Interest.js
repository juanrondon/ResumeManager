(function () {
    var source = $("#interests-template").html();
    var template = Handlebars.compile(source);

    $(document).ready(function () {
        function listInterests() {
            //get interests from DB
            var getInterests = $.ajax({
                type: "get",
                url: "/DraftInterest/GetInterests",
                data: { resumeDraftId: resumeDraftId }
            });
            getInterests.done(function (interestsList) {
                if (interestsList.length > 0) {
                    var context = { interests: interestsList };
                    var html = template(context);
                    $("#interestsListContainer").empty().html(html);
                } else {
                    $("#interestsListContainer").empty();
                }
            });
        }
        $("#addInterest").click(function (e) {
            e.preventDefault();
            if (!$("#createResumeDraftForm").valid())
                return;
            var name = $("#Interest").val();
            var addInterest = $.ajax({
                type: "get",
                url: "/DraftInterest/AddInterest",
                data: {
                    resumeDraftId: resumeDraftId,
                    interest: name
                }
            });
            addInterest.done(function () {
                $("#Interest").val("");
                listInterests();
            });
            addInterest.fail(function (errors) {
                displayErrors(errors, "Error");
                listInterests();
            });
        });

        $(document).on("click", ".removeInterest", function () {
            var id = $(this).data("id");
            var removeInterest = $.ajax({
                type: "post",
                url: "/DraftInterest/RemoveInterest",
                data: {
                    interestId: id
                }
            });
            removeInterest.done(function () {
                listInterests();
            });
        });

        $("#Interest").kendoAutoComplete({
            placeholder: "Type your interest ...",
            filter: "startswith",
            minLength: 1,
            dataSource: {
                pageSize: 100,
                transport: {
                    read: {
                        url: "/DraftInterest/GetPreloadedInterests",
                        type: "get"
                    }
                }
            }
        });
        listInterests();
    });
})();