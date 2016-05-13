function deleteRow(id) {
    var toDelete = confirm("Are you sure you want to delete this course?")
    if (toDelete == true) {
        window.location.assign("/Course/Delete/" + id);
    }
}

function deleteUser(Id) {
    var toDelete = confirm("Are you sure you want to delete this User?");
    if (toDelete == true) {
        console.log("message");
        window.location.assign("/Admin/DeleteUser/" + Id);
    }
}

function deleteFromCourse(id) {
    window.location.assign("/Course/RemoveUser/" + id);
}

$(function helpIndex() {
    $(".answer").hide();
    $('#management li a').each(function () {
        $(this).click(function () {
            $(this).siblings(".answer").slideToggle();
        });
    });
});


