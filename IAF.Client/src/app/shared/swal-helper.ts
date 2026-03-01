import Swal from 'sweetalert2';

export class SwalHelper {
    static success(message: string) {
        Swal.fire('Success', message, 'success');
    } 

    static error(message: string) {
        Swal.fire('Error', message, 'error');
    }

    static info(message: string) {
        Swal.fire('Info', message, 'info');
    }

    static warning(message: string) {
        Swal.fire('Warning', message, 'warning');
    }

    static confirm(message: string, callback: () => void) {
        Swal.fire({
            title: 'Confirm',
            text: message,
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(result => {
            if (result.isConfirmed) {
            callback();
            }
        });
    }

    static showToast(message: string, icon: 'success' | 'error' | 'warning' | 'info' = 'info') {
        Swal.fire({
            toast: true,
            position: 'bottom-start',   
            showConfirmButton: false,
            timer: 3000,               
            icon: icon,
            title: message,
            didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer);
                toast.addEventListener('mouseleave', Swal.resumeTimer);
            }
        });
    }

}
