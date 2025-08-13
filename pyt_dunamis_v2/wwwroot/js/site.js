// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", function () {
    const boton = document.getElementById("btnAgregarPersona");
    if (boton) {
        boton.addEventListener("click", function (e) {
            e.preventDefault();
            console.log("Botón de nueva persona clickeado");
            fetch('/Persona/ModalInsertarPersonaCompleta')
                .then(response => response.text())
                .then(html => {
                    document.getElementById('modalContainer').innerHTML = html;
                    const modalElement = document.getElementById('insertarPersonaModal');
                    const modal = new bootstrap.Modal(modalElement);
                    modal.show();
                })
                .catch(error => console.error('Error al cargar el modal:', error));
        });
    }
});




//Buscar persona
document.addEventListener("DOMContentLoaded", function () {
    const btnBuscar = document.getElementById('btnBuscarPersona');

    if (btnBuscar) {
        btnBuscar.addEventListener('click', function () {
            const criterio = document.getElementById('criterioBusqueda').value;
            const valor = document.getElementById('valorBusqueda').value;

            if (!valor.trim()) {
                alert("Por favor ingrese un valor para buscar.");
                return;
            }

            // Redirige con los parámetros
            window.location.href = `/Persona/BuscarPersona?criterio=${criterio}&valor=${encodeURIComponent(valor)}`;
        });
    }
});



// Click para la tabla
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".fila-clickeable").forEach(function (fila) {
        fila.addEventListener("click", function () {
            const idPersona = fila.getAttribute("data-id");
            if (idPersona) {
                window.location.href = `/Persona/ObtenerPersonaCompleta?idPersona=${idPersona}`;
            }
        });
    });
});


// Click para la tabla colaboradores
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".fila-clickeable-colaborador").forEach(function (fila) {
        fila.addEventListener("click", function () {
            const idPersona = fila.getAttribute("data-id");
            if (idPersona) {
                window.location.href = `/Persona/ObtenerColaboradorDatosCompletos?idPersona=${idPersona}`;
            }
        });
    });
});

/*
//Combos de provincia, cantón y distrito
document.addEventListener("DOMContentLoaded", function () {
    const provinciaSelect = document.getElementById("provinciaSelect");
    const cantonSelect = document.getElementById("cantonSelect");
    const distritoSelect = document.getElementById("distritoSelect");
    
    provinciaSelect.addEventListener("change", function () {
        const idProvincia = this.value;
        console.log("ID provincia seleccionada:", idProvincia); // 👈 Verifica aquí
        fetch(`/Catalogos/ObtenerCantones?idProvincia=${idProvincia}`)
            .then(response => response.json())
            .then(data => {
                cantonSelect.innerHTML = data.map(c => `<option value="${c.value}">${c.text}</option>`).join("");
                distritoSelect.innerHTML = "";
            });
    });

    cantonSelect.addEventListener("change", function () {
        const idCanton = this.value;
        fetch(`/Catalogos/ObtenerDistritos?idCanton=${idCanton}`)
            .then(response => response.json())
            .then(data => {
                distritoSelect.innerHTML = data.map(d => `<option value="${d.value}">${d.text}</option>`).join("");
            });
    });
}); */



document.addEventListener("change", function (e) {
    // Si se cambió una provincia
    if (e.target && e.target.classList.contains("provinciaSelect")) {
        const provinciaSelect = e.target;
        const selectedProvinciaId = provinciaSelect.value;

        // Encuentra los select relacionados
        const parentDiv = provinciaSelect.closest(".direccion-item");
        const cantonSelect = parentDiv.querySelector(".cantonSelect");
        const distritoSelect = parentDiv.querySelector(".distritoSelect");

        // Limpia canton y distrito
        cantonSelect.innerHTML = "<option value=''>Cargando...</option>";
        //distritoSelect.innerHTML = "<option value=''>Seleccione cantón primero</option>";

        // Cargar cantones
        fetch(`/Catalogos/ObtenerCantones?idProvincia=${selectedProvinciaId}`)
            .then(res => res.json())
            .then(data => {
                cantonSelect.innerHTML = data.map(c =>
                    `<option value="${c.value}">${c.text}</option>`).join('');
            });
    }

    // Si se cambió un cantón
    if (e.target && e.target.classList.contains("cantonSelect")) {
        const cantonSelect = e.target;
        const selectedCantonId = cantonSelect.value;

        const parentDiv = cantonSelect.closest(".direccion-item");
        const distritoSelect = parentDiv.querySelector(".distritoSelect");

        // Cargar distritos
        fetch(`/Catalogos/ObtenerDistritos?idCanton=${selectedCantonId}`)
            .then(res => res.json())
            .then(data => {
                distritoSelect.innerHTML = data.map(d =>
                    `<option value="${d.value}">${d.text}</option>`).join('');
            });
    }
});




