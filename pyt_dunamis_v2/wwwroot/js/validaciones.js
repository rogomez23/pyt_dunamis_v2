document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('form');

    form.addEventListener('submit', function (e) {
        let isValid = true;
        limpiarErrores();

        // Validar cédula
        const cedula = document.querySelector('[name="Persona.id_persona"]');
        if (!cedula.value.trim()) {
            mostrarError(cedula, 'La cédula es obligatoria.');
            isValid = false;
        }

        // Validar nombre
        const nombre = document.querySelector('[name="Persona.nombre"]');
        if (!nombre.value.trim()) {
            mostrarError(nombre, 'El nombre es obligatorio.');
            isValid = false;
        }

        // Validar primer apellido
        const apellido1 = document.querySelector('[name="Persona.apellido_1"]');
        if (!apellido1.value.trim()) {
            mostrarError(apellido1, 'El primer apellido es obligatorio.');
            isValid = false;
        }

        // Validar fecha de nacimiento
        const fechaNacimiento = document.querySelector('[name="Persona.fecha_nacimiento"]');
        if (!fechaNacimiento.value) {
            mostrarError(fechaNacimiento, 'Debe ingresar la fecha de nacimiento.');
            isValid = false;
        }

        // Validar al menos un teléfono
        const telefonos = document.querySelectorAll('[name^="Telefono"][name$=".numero"]');
        let algunTelefonoValido = false;
        telefonos.forEach(tel => {
            if (tel.value.trim().length >= 8) {
                algunTelefonoValido = true;
            }
        });
        if (!algunTelefonoValido) {
            mostrarError(telefonos[0], 'Debe ingresar al menos un número de teléfono válido.');
            isValid = false;
        }

        // Validar al menos un correo electrónico válido
        const correos = document.querySelectorAll('[name^="Email"][name$=".email"]');
        let algunEmailValido = false;
        const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        correos.forEach(email => {
            if (regexEmail.test(email.value.trim())) {
                algunEmailValido = true;
            }
        });
        if (!algunEmailValido) {
            mostrarError(correos[0], 'Debe ingresar al menos un correo electrónico válido.');
            isValid = false;
        }

        // Validar selección de puesto
        const puesto = document.querySelector('[name="Colaborador.catalogo_perfil_puesto_id_perfil_puesto"]');
        if (!puesto.value || puesto.value === "0") {
            mostrarError(puesto, 'Debe seleccionar un puesto.');
            isValid = false;
        }

        // Validar fecha de ingreso
        const fechaIngreso = document.querySelector('[name="Colaborador.fecha_ingreso"]');
        if (!fechaIngreso.value) {
            mostrarError(fechaIngreso, 'Debe seleccionar la fecha de ingreso.');
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault(); // Detiene el envío del formulario
        }
    });

    function mostrarError(campo, mensaje) {
        const span = document.createElement('span');
        span.classList.add('text-danger');
        span.innerText = mensaje;
        campo.classList.add('is-invalid');
        campo.parentElement.appendChild(span);
    }

    function limpiarErrores() {
        document.querySelectorAll('.text-danger').forEach(el => el.remove());
        document.querySelectorAll('.is-invalid').forEach(el => el.classList.remove('is-invalid'));
    }
});