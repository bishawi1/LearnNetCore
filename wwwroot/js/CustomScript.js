function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();

    }
}


function InitDatepicker () {
    $(".datepicker").datepicker({
        dateFormat: 'yy-mm-dd',
        changeMonth: true,
        changeYear: true,
        //minDate: new Date(2000, 1, 1),
        //maxDate: new Date(2020, 12, 31),
        //showOn: "both",
        //buttonText: "<i class='btn-primary fa fa-calendar'></i>"
        //buttonText:"Select"
    });
};