
function inicijalizacijaTabele(tabelaId) {
    var tabela = $('#' + tabelaId).DataTable({
            "destroy": true,
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": "/Agreement/AgreementData",
                "type": "POST",
                "datatype": "json"
            },
            "columns": [
                { "data": "id" },
                { "data": "user.userName" },
                { "data": "productGroupId"},
                { "data": "productGroup.groupDescription"},
                { "data": "product.productNumber"},
                { "data": "product.productDescription"},
                { "data": "effectiveDate" },
                { "data": "expirationDate" },
                { "data": "productPrice" },
                { "data": "newPrice" },
                { "data": "id" },
            ],
            "columnDefs": [
                {
                    "render": function (data, type, row, meta) {
                        console.log(data);
                        return tabela.page.info().start + meta.row + 1;
                    },
                    "className": "algRight", "searchable": false, "orderable": false, "targets": 0
                },
                { "className": "algLeft", "searchable": false, "orderable": false, "targets": 1 },
                { "className": "algLeft", "searchable": false, "orderable": false, "targets": 2 },
                { "className": "algLeft", "searchable": false, "orderable": false, "targets": 3 },
                { "className": "algRight", "searchable": false, "orderable": false, "targets": 4 },
                { "className": "algLeft", "searchable": false, "orderable": false, "targets": 5 },
                { "className": "algLeft", "searchable": false, "orderable": false, "targets": 6 },
                { "className": "algLeft", "searchable": false, "orderable": false, "targets": 7 },
                {
                    "render": function (data, type, row, meta) {
                        return numeral(data).format('0,0.00');
                    },
                    "className": "algLeft", "searchable": false, "orderable": false, "targets": 8
                },
                {
                    "render": function (data, type, row, meta) {
                        return numeral(data).format('0,0.00');
                    },
                    "className": "algLeft", "searchable": false, "orderable": false, "targets": 9
                },
                {
                    "render": function (data, type, row, meta) {
                        return "<a href='#' class='btn btn-warning' onclick='editAgreement(this)''>" + Edit + "</a>" + " " + "<a href='#' class='btn btn-warning' onclick='deleteAgreement(this)'>" + Delete + "</a>";

                        //    return "<a href='#' class='btn btn-danger' onclick=editAgreement('" + row.productGroupId + "," + row.productId + "," + row.effectiveDate + "," + row.expirationDate + "," + row.newPrice + "'); >Edit</a>" + " " + " <a href='#' class='btn btn-danger' onclick = deleteAgreement('" + data + "'); > Delete</a >";
                    },
                    "className": "algLeft", "searchable": false, "orderable": false, "targets": 10
                }
            ],
            "ordering": true,
            "paging": true,
            "scrollX": true,
            "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
            "pageLength": 20
        });
}

function AgreementAddEdit(formId) {
    $.ajax({
        type: 'POST',
        url: '/Agreement/AddAgreement',
        data: $("#" + formId).serialize(),
        dataType: 'html',
        contentType: 'application/x-www-form-urlencoded; charset-UTF-8'
    })
        .done(function (response) {
            if (response == "1") {
                console.log("Uspesan upis");
                inicijalizacijaTabele();
            }
            else {
                console.log("Greska pri upisu");
            }
        })
        .fail(function (response) {
            console.log("greska " + response);
        })
}