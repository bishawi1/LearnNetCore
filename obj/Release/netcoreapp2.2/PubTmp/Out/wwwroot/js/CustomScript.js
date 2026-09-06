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

function ApplyPermission(response) {
    var i;
    for (i = 0; i < response.ParentMenus.length; ++i) {
        if (response.ParentMenus[i] == "Settings") {
            $('#crdSettings').show();
            $('#mnuFiles').removeClass('hideElem');
        } else if (response.ParentMenus[i] == "Task") {
            $('#crdTasks').show();
            $('#mnuTask').removeClass('hideElem');
        } else if (response.ParentMenus[i] == "PurchaseOrder") {
            $('#crdOrders').show();
            $('#mnuPurchaseOrders').removeClass('hideElem');

        } else if (response.ParentMenus[i] == "Offers") {
            $('#crdOffers').show();
            $('#mnuOffers').removeClass('hideElem');
       }
    };
    for (i = 0; i < response.UserPermissions.length; ++i) {
        if (response.UserPermissions[i].PageName == "Employees") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkEmployees').removeClass('hideElem');            
            };
        } else if (response.UserPermissions[i].PageName == "Branches") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkBranches').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "projects") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkProjects').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Customers") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkCustomers').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Suppliers") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkSuppliers').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Item Units") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkItemUnits').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Items") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkItems').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Main Items") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkMainItems').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Item Categories") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkItemCategory').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Currency") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkCurrency').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "All Tasks") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkAllTasks').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Tasks By Project") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkTaskByProject').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Tasks By Owner") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkTaskByOwner').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Tasks By Responsible") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkTaskByResponsible').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Tasks By Status") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkTaskByStatus').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Tasks Reports") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkTaskReports').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "All Purchase Orders") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkAllPurchaseOrders').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "UserTaskPermissions") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkUserTaskPermissions').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Purchase Orders Reports") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkPurchaseOrdersReports').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "All Offers") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkAllOffers').removeClass('hideElem');
            };
        } else if (response.UserPermissions[i].PageName == "Offers Reports") {
            if (response.UserPermissions[i].CanView) {
                $('#lnkOffersReports').removeClass('hideElem');
            };
        }
    };

};
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




    // Builds the HTML Table out of myList.
    function buildHtmlTable(myList,selector) {
        var columns = addAllColumnHeaders(myList, selector);

        for (var i = 0; i < myList.length; i++) {
            var row$ = $('<tr/>');
            for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                var cellValue = myList[i][columns[colIndex]];
                if (cellValue == null) cellValue = "";
                row$.append($('<td/>').html(cellValue));
            }
            $(selector).append(row$);
        }
    }
    // Adds a header row to the table and returns the set of columns.
    // Need to do union of keys from all records as some records may not contain
    // all records.
    function addAllColumnHeaders(myList, selector) {
        var columnSet = [];
        var headerTr$ = $('<tr/>');

        for (var i = 0; i < myList.length; i++) {
            var rowHash = myList[i];
            for (var key in rowHash) {
                if ($.inArray(key, columnSet) == -1) {
                    columnSet.push(key);
                    headerTr$.append($('<th/>').html(key));
                }
            }
        }
        $(selector).append(headerTr$);

        return columnSet;
    }

};