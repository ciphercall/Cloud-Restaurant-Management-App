$(function () {
    $("#rolegrid").jqGrid({
        url: "/ASL_ROLE/GetTodoLists",
        editurl: '/ASL_ROLE/Edit',

        postData: {
            txtUserName: function () { return jQuery("#txtUserNameID option:selected").val(); },
            module: function () { return jQuery("#txtModuleNm option:selected").val(); },
            menuType: function () { return jQuery("#menuTypeID option:selected").val(); },
        },

        //editData: {
        //    txtUserName: $("#userID").val(),
        //    module: $("#moduleID").val(),
        //    menuType:$("#menuTypeID option:selected").val(),
        //},

        datatype: 'json',
        mtype: 'Get',
        colNames: ['Id', 'UserId', 'Menu Id','Menu Name','Action Name','Controller Name', 'Status', 'Insert', 'Update', 'Delete'],
        colModel: [
            //{
            //    name: "Edit Actions",
            //    width: 30,
            //    formatter: "actions",
            //    formatoptions: {
            //        keys: false,
            //        editOptions: {
            //            url: "/ASL_ROLE/Edit"
            //        },

            //    }
            //},
            { key: true, hidden: true, name: 'Id', index: 'Id', editable: true },
            { key: false, hidden: true, name: 'USERID', index: 'USERID', editable: true },
            { key: false, hidden: true, name: 'MENUID', index: 'MENUID', editoptions: { readonly: "readonly" }, editable: true, width: 50 },
            { key: false, name: 'MENUNM', index: 'MENUNM', editoptions: { readonly: "readonly" }, editable: true, width: 50 },
            { key: false, name: 'ACTIONNAME', index: 'ACTIONNAME', editoptions: { readonly: "readonly" }, editable: true, width: 50 },
            { key: false, name: 'CONTROLLERNAME', index: 'CONTROLLERNAME', editoptions: { readonly: "readonly" }, editable: true, width: 50 },
            { key: false, name: 'STATUS', index: 'STATUS', editable: true, width: 50, edittype: 'select', editoptions: { value: { 'A': 'Active', 'I': 'Inactive' } } },
            { key: false, name: 'INSERT', index: 'INSERT', editable: true, width: 50, edittype: 'select', editoptions: { value: { 'A': 'Active', 'I': 'Inactive' } } },
            { key: false, name: 'UPDATE', index: 'UPDATE', editable: true, width: 50, edittype: 'select', editoptions: { value: { 'A': 'Active', 'I': 'Inactive' } } },
            { key: false, name: 'DELETE', index: 'DELETE', editable: true, width: 50, edittype: 'select', editoptions: { value: { 'A': 'Active', 'I': 'Inactive' } } }],
        pager: jQuery('#pager'),
        onSelectRow: editRow,
        rowNum: 15,
        rowList: [15, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Permission List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false

    }).navGrid('#pager', { edit: true, add: false, del: false, search: false, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/ASL_ROLE/Edit',
            editData: {
                txtUserName: function () { return jQuery("#txtUserNameID option:selected").val(); },
                module: function () { return jQuery("#txtModuleNm option:selected").val(); },
                menuType: function () { return jQuery("#menuTypeID option:selected").val(); },
            },
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });

    var lastSelection;

    function editRow(id) {
        if (id && id !== lastSelection) {
            var grid = $("#rolegrid");
            grid.restoreRow(lastSelection);
            grid.editRow(id, true);
            lastSelection = id;
        }
    }
});


