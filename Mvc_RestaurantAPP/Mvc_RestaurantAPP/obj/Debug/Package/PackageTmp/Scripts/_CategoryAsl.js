$(function () {


    $("#categorygrid").jqGrid({
        url: "/CategoryItem/GetTodoLists",
        editurl: '/CategoryItem/Edit',

        postData: {
            companyId: function () { return jQuery("#txtCompId").val(); },
            categoryId: function () { return jQuery("#txtCategoryId").val(); },
            latitute: function () { return jQuery("#latlonRms").val(); },
        },

        editData: {
            companyId: $("#txtCompId").val(),
            categoryId: $("#txtCategoryId").val(),
            latitute: $("#latlonRms").val()
        },

        datatype: 'json',
        mtype: 'Get',
        colNames: ['Action', 'RMS_ITEMid', 'CompanyId', 'CategoryId', 'ItemId', 'Item Name', 'Brand', 'Unit', 'Buy Rate ', 'Sale Rate', 'Stock Minimum', 'Discount', 'Remarks', 'UPDLTUDE'],
        colModel: [
          {
              name: "Edit Actions",
              width: 40,
              formatter: "actions",
              formatoptions: {
                  keys: false,
                  editbutton: true,
                  delbutton: true,
                  editOptions: {},
                  addOptions: {},
                  delOptions: {
                      url: "/CategoryItem/Delete"
                  },
                  afterSave: function (rowid) {
                      $('#pager').show();
                      alert("record saved!");
                  },
                  afterRestore: function (rowid) {
                      $('#pager').show();
                      return false;
                  }
              }
          },
            //Hidden Columns
            { key: true, hidden: true, name: 'RMS_ITEMid', index: 'RMS_ITEMid', editable: true },
            { key: false, hidden: true, name: 'COMPID', index: 'COMPID', editable: true },
            { key: false, hidden: true, name: 'CATID', index: 'CATID', editable: true },
            { key: false, hidden: true, name: 'ITEMID', index: 'ITEMID', editable: true },
            //displayed Columns
            { key: false, name: 'ITEMNM', index: 'ITEMNM', editable: true, width: 80 },
            { key: false, name: 'BRAND', index: 'BRAND', editable: true, width: 50 },
            { key: false, name: 'UNIT', index: 'UNIT', editable: true, width: 30 },
            {
                key: false, name: 'BUYRT', index: 'BUYRT', editable: true, width: 45, edittype: "text", editoptions: {
                    dataInit: function (element) {
                        $(element).keyup(function () {
                            var val1 = element.value;
                            var num = new Number(val1);
                            if (isNaN(num)) {
                                alert("Please enter a valid number in Buy rate column.");
                            }
                        });
                    }
                }
            },
            {
                key: false, name: 'SALRT', index: 'SALRT', editable: true, width: 45, edittype: "text", editoptions: {
                    dataInit: function (element) {
                        $(element).keyup(function () {
                            var val1 = element.value;
                            var num = new Number(val1);
                            if (isNaN(num)) {
                                alert("Please enter a valid number in Sale rate column");
                            }
                        });
                    }
                }
            },
            {
                key: false, name: 'STKMIN', index: 'STKMIN', editable: true, width: 65, edittype: "text", editoptions: {
                    dataInit: function (element) {
                        $(element).keyup(function () {
                            var val1 = element.value;
                            var num = new Number(val1);
                            if (isNaN(num)) {
                                alert("Please enter a valid number in Stoke minimum column");
                            }
                        });
                    }
                }
            },
            {
                key: false, name: 'DISCOUNT', index: 'DISCOUNT', editable: true, width: 40, edittype: "text", editoptions: {
                    dataInit: function (element) {
                        $(element).keyup(function () {
                            var val1 = element.value;
                            var num = new Number(val1);
                            if (isNaN(num)) {
                                alert("Please enter a valid number in Discount column.");
                            }
                        });
                    }
                }
            },
            { key: false, name: 'REMARKS', index: 'REMARKS', editable: true, width: 50 },
            { key: false, hidden: true, name: 'UPDLTUDE', index: 'UPDLTUDE', editable: true}],
        pager: jQuery('#pager'),
        onSelectRow: editRow,
        rowNum: 15,
        rowList: [15, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Item List',
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

    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/CategoryItem/Edit',
            editData: {
                companyId: function () { return jQuery("#txtCompId").val(); },
                categoryId: function () { return jQuery("#txtCategoryId").val(); },
                latitute: function () { return jQuery("#latlonRms").val(); }
            },
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/CategoryItem/Create",
            editData: {
                companyId: function () { return jQuery("#txtCompId").val(); },
                categoryId: function () { return jQuery("#txtCategoryId").val(); },
                latitute: function () { return jQuery("#latlonRms").val(); }
            },
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/CategoryItem/Delete",
            editData: {
                companyId: function () { return jQuery("#txtCompId").val(); },
                categoryId: function () { return jQuery("#txtCategoryId").val(); },
                latitute: function () { return jQuery("#latlonRms").val(); }
            },
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });

    var lastSelection;

    function editRow(id) {
        if (id && id !== lastSelection) {
            var grid = $("#categorygrid");
            grid.restoreRow(lastSelection);
            grid.editRow(id, true);
            lastSelection = id;

        }
    }

});









