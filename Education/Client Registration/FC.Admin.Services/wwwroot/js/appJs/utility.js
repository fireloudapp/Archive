var Toast;

$(function () {
    Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })
});
// Ref: https://sweetalert2.github.io/#examples
//ico: 'warning', 'error', 'success', 'info', and 'question'
function fnMessage(ico, msg) {
    Toast.fire({
        icon: ico,
        title: msg
    });//Signed in successfully
}

function fnConfirmAndAct(action, msg) {
    Swal.fire({
        title: 'Are you sure?',
        text: msg, // "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            
            /*Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )*/
            action();            
        }
    })
}


$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        html: true,
        content: function () {
            return $('#primary-popover-content').html();
        }
    });
});