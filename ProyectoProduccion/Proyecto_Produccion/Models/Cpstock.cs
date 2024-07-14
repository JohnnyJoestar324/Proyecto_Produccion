using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Produccion.Models;

[Table("CPStock")]
public partial class Cpstock
{
    [Key]
    [Column("IDCPStock")]
    public short Idcpstock { get; set; }

    public int Demanda { get; set; }

    [Display(Name = "Tiempo Reposo:")]
    public int TiempoReposo { get; set; }
   
    [Display(Name = "Inventario de Seguridad:")]
    public int InventarioSeguridad { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    [Display(Name = "Resultado:")]
    public string? Resultado { get; set; }
}
