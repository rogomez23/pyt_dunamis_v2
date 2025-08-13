/*f (window.$ && $.validator && $.validator.unobtrusive) {
    $.validator.unobtrusive.parse = function () { };
}*/

/*
document.addEventListener('DOMContentLoaded', function () {
    console.log("Validaciones generales activadas");

    const formularios = document.querySelectorAll("form");

    formularios.forEach(form => {
        form.addEventListener("submit", function (e) {
            let isValid = true;
            limpiarErrores(form);

            form.querySelectorAll(".requerido").forEach(campo => {
                if (!campo.value || campo.value.trim() === "") {
                    mostrarError(campo, "Este campo es obligatorio.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".validar-email").forEach(campo => {
                const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (campo.value.trim() !== "" && !regexEmail.test(campo.value.trim())) {
                    mostrarError(campo, "Correo electrónico inválido.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".validar-numero").forEach(campo => {
                if (campo.value.trim() !== "" && isNaN(campo.value.trim())) {
                    mostrarError(campo, "Solo se permiten números.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".validar-fecha").forEach(campo => {
                if (!campo.value) {
                    mostrarError(campo, "Seleccione una fecha.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".requerido-select").forEach(campo => {
                if (!campo.value || campo.value === "0") {
                    mostrarError(campo, "Seleccione una opción.");
                    isValid = false;
                }
            });

            if (!isValid) {
                e.preventDefault(); // Esto impide que se envíe
            }
        });
    });

    function mostrarError(campo, mensaje) {
        campo.classList.add("is-invalid");

        let span = document.createElement("span");
        span.className = "text-danger d-block";
        span.innerText = mensaje;

        campo.parentElement.appendChild(span);
    }

    function limpiarErrores(form) {
        form.querySelectorAll(".is-invalid").forEach(campo => {
            campo.classList.remove("is-invalid");
        });
        form.querySelectorAll(".text-danger").forEach(span => {
            span.remove();
        });
    }
});; */

document.addEventListener('DOMContentLoaded', function () {
    console.log("Validaciones generales y Modal de confirmación activados");

    // ==============================
    // Validaciones de formularios
    // ==============================
    const formularios = document.querySelectorAll("form");

    formularios.forEach(form => {
        form.addEventListener("submit", function (e) {
            let isValid = true;
            limpiarErrores(form);

            form.querySelectorAll(".requerido").forEach(campo => {
                if (!campo.value || campo.value.trim() === "") {
                    mostrarError(campo, "Este campo es obligatorio.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".validar-email").forEach(campo => {
                const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (campo.value.trim() !== "" && !regexEmail.test(campo.value.trim())) {
                    mostrarError(campo, "Correo electrónico inválido.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".validar-numero").forEach(campo => {
                if (campo.value.trim() !== "" && isNaN(campo.value.trim())) {
                    mostrarError(campo, "Solo se permiten números.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".validar-fecha").forEach(campo => {
                if (!campo.value) {
                    mostrarError(campo, "Seleccione una fecha.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".requerido-select").forEach(campo => {
                if (!campo.value || campo.value === "0") {
                    mostrarError(campo, "Seleccione una opción.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".validar-cedula").forEach(campo => {
                const dígitos = campo.value.replace(/\D/g, '');
                if (dígitos.length !== 9) {
                    mostrarError(campo, "Cédula inválida. Debe tener 9 dígitos numéricos.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".solo-texto").forEach(campo => {
                const val = campo.value.trim();
                const regexTexto = /^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$/;
                if (val && !regexTexto.test(val)) {
                    mostrarError(campo, "Solo se permiten letras y espacios.");
                    isValid = false;
                }
            });

            form.querySelectorAll(".validar-fecha-nacimiento").forEach(campo => {
                if (!campo.value) {
                    mostrarError(campo, "Seleccione una fecha.");
                    isValid = false;
                } else {
                    const fecha = new Date(campo.value);
                    const hoy = new Date();
                    let edad = hoy.getFullYear() - fecha.getFullYear();
                    const cumpleEsteAño = new Date(hoy.getFullYear(), fecha.getMonth(), fecha.getDate());
                    if (hoy < cumpleEsteAño) edad--;
                    if (edad < 18) {
                        mostrarError(campo, "La persona debe ser mayor de 18 años.");
                        isValid = false;
                    }
                }
            });


            form.querySelectorAll(".validar-contrasena").forEach(campo => {
                const val = campo.value;

                // Chequeos individuales
                const faltaLongitud = val.length < 14;
                const faltaLetra = !/[A-Za-z]/.test(val);
                const faltaNumero = !/\d/.test(val);
                const faltaEspecial = !/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(val);

                if (faltaLongitud || faltaLetra || faltaNumero || faltaEspecial) {
                    let mensaje;
                    if (faltaLongitud) {
                        mensaje = "La contraseña debe tener al menos 14 caracteres.";
                    } else if (faltaLetra) {
                        mensaje = "La contraseña debe incluir al menos una letra.";
                    } else if (faltaNumero) {
                        mensaje = "La contraseña debe incluir al menos un número.";
                    } else if (faltaEspecial) {
                        mensaje = "La contraseña debe incluir al menos un carácter especial.";
                    }
                    mostrarError(campo, mensaje);
                    isValid = false;
                }
            });




            if (!isValid) {
                e.preventDefault(); // Esto impide que se envíe
            }
        });
    });

    function mostrarError(campo, mensaje) {
        campo.classList.add("is-invalid");

        let span = document.createElement("span");
        span.className = "text-danger d-block";
        span.innerText = mensaje;

        campo.parentElement.appendChild(span);
    }

    function limpiarErrores(form) {
        form.querySelectorAll(".is-invalid").forEach(campo => {
            campo.classList.remove("is-invalid");
        });
        form.querySelectorAll(".text-danger").forEach(span => {
            span.remove();
        });
    }

    // ==============================
    // Modal de confirmación de acciones
    // ==============================
    let actionUrl = '';  // URL de la acción a realizar
    let actionType = ''; // Tipo de acción (agregar, editar, asignar, eliminar)

    // Función para mostrar el modal y establecer la URL y el tipo de acción
    function showConfirmationModal(url, type) {
        actionUrl = url;
        actionType = type;
        $('#confirmModal').modal('show');  // Muestra el modal


        // Actualizar el título, mensaje y el ícono según el tipo de acción
        const title = document.getElementById('confirmModalLabel');
        const message = document.getElementById('modalMessage');
        const icon = document.getElementById('actionIcon');

        if (actionType === 'delete') {
            title.textContent = "Estás a punto de eliminar un registro!";
            message.textContent = "¿Está seguro de que desea eliminar este registro?";
            icon.classList.add('bi', 'bi-trash', 'text-danger', 'fs-3');  // Icono de eliminación
        } else if (actionType === 'assign' || actionType === 'edit') {
            title.textContent = "Estás a punto de editar un registro!";
            message.textContent = "¿Está seguro de que desea editar este registro?";
            icon.classList.add('bi', 'bi-pencil-square', 'text-primary', 'fs-3');  // Icono de edición
        } else if (actionType === 'add') {
            title.textContent = "Estás a punto de agregar un nuevo registro!";
            message.textContent = "¿Está seguro de que desea agregar este registro?";
            icon.classList.add('bi', 'bi-plus-circle', 'text-success', 'fs-3');  // Icono de agregar
        }

    }

    // Al hacer clic en "Sí" en el modal, se realiza la acción
    const confirmButton = document.getElementById('confirmActionButton');
    if (confirmButton) {
        confirmButton.addEventListener('click', function () {
            if (actionType === 'delete') {
                // Si la acción es eliminar, ejecutamos la URL
                window.location.href = actionUrl;
            } else if (actionType === 'assign' || actionType === 'edit' || actionType === 'add') {
                // Para agregar, editar o asignar, se envía el formulario o redirige
                const form = document.getElementById(actionType + 'PermissionForm');
                form.submit(); // Envia el formulario
            }
            $('#confirmModal').modal('hide');  // Cierra el modal
        });
    }

    // Enlazar eventos para mostrar el modal de confirmación
    const actionButtons = document.querySelectorAll('.action-button');
    actionButtons.forEach(button => {
        button.addEventListener('click', function (event) {
            event.preventDefault();  // Prevenir que se ejecute la acción inmediatamente

            const url = button.getAttribute('href');  // Obtiene la URL del enlace
            const actionType = button.getAttribute('data-action-type');  // Obtiene el tipo de acción (agregar, editar, asignar, eliminar)

            // Muestra el modal con la URL de la acción y el tipo
            showConfirmationModal(url, actionType);
        });
    });
});


$(function () {
    $('.mask-cedula').inputmask('9-9999-9999', {
        placeholder: '_',
        removeMaskOnSubmit: true
    });
});

