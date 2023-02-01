var datatables;

$(document).ready(function () {
    loadDatatables();
});

function loadDatatables()
{
    datatables = $('#data').DataTable({
        "ajax": {
            "url": "/Product/GetAll"
        },
        "columns":[
            { "data": "title", "width": "15%" },
            { "data": "description", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) { 
                    return `
                        <div class="w-75 btn-group" role="group">
                         <a href="/Product/Upsert/?id=${data}" 
                            class="btn btn-outline-dark mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                         <a onclick=deleted('/Product/Delete/${data}') 
                            class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                        </div>        
                    `  
                },
                "width":"15%"
            }
        ]
    });
}

function deleted(url)
{
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        datatables.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}