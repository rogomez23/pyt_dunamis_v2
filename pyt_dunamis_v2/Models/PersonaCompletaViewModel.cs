using System;
using System.Collections.Generic;
using Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace pyt_dunamis_v2.Models
{
    public class PersonaCompletaViewModel
    {
        public Persona Persona { get; set; } = new Persona();
        public List<Email> Email { get; set; } = new List<Email>();
        public List<Telefono> Telefono { get; set; } = new List<Telefono>();
        public List<Direccion> Direccion { get; set; } = new List<Direccion>();
        public List<SelectListItem> TiposEmail { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> TiposTelefono { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> TiposDireccion { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Provincias { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Cantones { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Distritos { get; set; } = new List<SelectListItem>();
        public Colaborador Colaborador { get; set; } = new Colaborador();
        public List<SelectListItem> PerfilesPuestos { get; set; } = new List<SelectListItem>();
        public Catalogo_perfil_puesto Puesto { get; set; } = new Catalogo_perfil_puesto();
        public bool ColaboradorYaTieneUsuario { get; set; }
    }
}
