var dataTables;


$(document).ready(function () {
    debugger;
    loadTables();
});

function loadTables() {
    dataTables = $('tblData').DataTable({
        "ajax": {
            "url":"Order/GetAll"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "name", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <div class="w-75 btn-group" role="group">
                         <a href="/Order/Details/?orderId=${data}" 
                            class="btn btn-outline-dark mx-2"> <i class="bi bi-pencil-square"></i> Details </a>
                        </div>          
                    `
                }
            }
        ]
    })
}
