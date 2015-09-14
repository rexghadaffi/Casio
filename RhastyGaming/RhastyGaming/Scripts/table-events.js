function SearchStudent() {
    $('#tblstudent').bootstrapTable('destroy');
    var studentNumber = $('#txtSearchStudent').val();
    $('#tblstudent').bootstrapTable({
        url: '/Student/Find?snum=' + studentNumber,
        pagination: true,
        classes: 'table table-hover table-condensed',
        sidePagination: 'server',
        queryParamsType: 'Else',
        uniqueId: 'ID',
        toolbarAlign: 'right',
        toolbar: '#studentToolbar',
        pageList: [10, 20, 50, 100],
        onDblClickRow: function (row, $element) {
            var key = row["ID"];
            window.location.href = "/student/edit/" + key;
        }
    });
}
function Refresh() {
    $('#tblstudent').bootstrapTable('destroy');
    $('#tblstudent').bootstrapTable({
        url: '/Student/Data',
        pagination: true,
        classes: 'table table-hover table-condensed',
        sidePagination: 'server',
        queryParamsType: 'Else',
        uniqueId: 'ID',
        toolbarAlign: 'right',
        toolbar: '#studentToolbar',
        pageList: [10, 20, 50, 100],
        onDblClickRow: function (row, $element) {
            var key = row["ID"];
            window.location.href = "/student/edit/" + key;
        }
    });
}