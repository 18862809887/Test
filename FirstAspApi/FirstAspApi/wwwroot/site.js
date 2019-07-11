const uri = "api/Userinfoes";
let todos = null;
function getCount(data) {
    const el = $("#counter");
    let name = "to-do";
    if (data) {
        if (data > 1) {
            name = "to-dos";
        }
        el.text(data + " " + name);
    } else {
        el.text("No " + name);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#todos");

            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    
                    .append($("<td></td>").text(item.id))
                    .append($("<td></td>").text(item.username))
                    .append($("<td></td>").text(item.age))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edit</button>").on("click", function () {
                                editItem(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Delete</button>").on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            todos = data;
        }
    });
}

function addItem() {
    const item = {
        id: $("#add-id").val(),
        username: $("#add-name").val(),
        age: $("#add-age").val()
    };
    console.log("id"+ item.id);
    console.log("username" + item.username);
    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            $("#add-name").val("");
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $("#edit-name").val(item.username);
            $("#edit-id").val(item.id);
            $("#edit-isComplete").val(item.age);
        }
    });
    $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function () {
    const item = {
        id: $("#edit-id").val(),
        username: $("#edit-name").val(),
        age: $("#edit-isComplete").val()
    };
    console.log("123:" + item.id);
    console.log("321" + item.name);
    $.ajax({
        url: uri + "/" + item.id,
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        success: function (result) {
            getData();
            console.log("success");
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $("#spoiler").css({ display: "none" });
}