$(document).ready(function () {

    function loadTasks() {
        var term = $("#searchTerm").val();
        var isCompleted = $("#statusFilter").val();
        var sortOrder = $("#sortOrder").val();

        $.ajax({
            url: "/Task/Search",
            type: "GET",
            data: {
                term: term,
                isCompleted: isCompleted,
                sortOrder: sortOrder
            },
            success: function (response) {
                $("#taskTableContainer").html(response);
            },
            error: function () {
                alert("Failed to load tasks.");
            }
        });
    }

    $("#searchTerm").on("keyup", function () {
        loadTasks();
    });

    $("#statusFilter, #sortOrder").on("change", function () {
        loadTasks();
    });

    $(document).on("click", ".delete-task", function () {
        var id = $(this).data("id");

        if (confirm("Are you sure you want to delete this task?")) {
            $.ajax({
                url: "/Task/Delete/" + id,
                type: "POST",
                success: function (response) {
                    if (response.success) {
                        loadTasks();
                    } else {
                        alert(response.message);
                    }
                },
                error: function () {
                    alert("Failed to delete task.");
                }
            });
        }
    });

    $(document).on("change", ".toggle-status", function () {
        var id = $(this).data("id");

        $.ajax({
            url: "/Task/ToggleStatus/" + id,
            type: "POST",
            success: function (response) {
                if (!response.success) {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Failed to update task status.");
            }
        });
    });
});