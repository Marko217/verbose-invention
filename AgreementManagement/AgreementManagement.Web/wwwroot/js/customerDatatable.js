
function initializationTable(tableId) {
    var table = $('#' + tableId).DataTable({
        "destroy": true,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Agreement/AgreementData",
            "type": "POST",
            "dataType": "json",
            "context": this,
            "async": false
        },
        "columns": [
            { "data": "srn" },
            { "data": "user" },
            { "data": "groupCode" },
            { "data": "productGroupDescription" },
            { "data": "productNumber" },
            { "data": "productDescription" },
            { "data": "effectiveDate" },
            { "data": "expirationDate" },
            { "data": "productPrice" },
            { "data": "newPrice" },
            { "data": "id" },
        ],
        "columnDefs": [
            {
                "render": function (data, type, row, meta) {
                    return data;
                },
                "searchable": false, "orderable": false, "targets": 0
            },
            { "searchable": false, "orderable": false, "targets": 1 },
            { "searchable": false, "orderable": false, "targets": 2 },
            { "searchable": false, "orderable": false, "targets": 3 },
            { "searchable": false, "orderable": false, "targets": 4 },
            { "searchable": false, "orderable": false, "targets": 5 },
            {
                "render": function (data, type, row, meta) {
                    if (data != null)
                        return moment(data).format('MM/DD/YYYY');
                    else
                        return data;
                },
                "searchable": false, "orderable": false, "targets": 6
            },
            {
                "render": function (data, type, row, meta) {
                    if (data != null)
                        return moment(data).format('MM/DD/YYYY');
                    else
                        return data;
                },
                "searchable": false, "orderable": false, "targets": 7
            },
            {
                "render": function (data, type, row, meta) {
                    return numeral(data).format('0,0.00');
                },
                "searchable": false, "orderable": false, "targets": 8
            },
            {
                "render": function (data, type, row, meta) {
                    return numeral(data).format('0,0.00');
                },
                "searchable": false, "orderable": false, "targets": 9
            },
            {
                "render": function (data, type, row, meta) {
                    return "<a href='#' class='btn btn-primary' onclick='editAgreement(this)'> Edit </a>" + " " + "<a href='#' class='btn btn-danger' onclick='deleteModalAgreement(this)'> Delete </a>";
                },
                "searchable": false, "orderable": false, "targets": 10
            }
        ],
        "ordering": true,
        "paging": true,
        "pageLength": 5
    });
}