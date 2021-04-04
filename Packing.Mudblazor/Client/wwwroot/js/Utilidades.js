export function TodoCorrecto() {
    return Swal.fire({
        icon: 'success',
        title: 'La información ha sido guardada correctamente.',
        showConfirmButton: false,
        timer: 1500
    });
}

export function ErrorServidor() {
    return Swal.fire({
        icon: 'error',
        title: 'Ha ocurrido un error interno.',
        showConfirmButton: false,
        timer: 1500
    });
}