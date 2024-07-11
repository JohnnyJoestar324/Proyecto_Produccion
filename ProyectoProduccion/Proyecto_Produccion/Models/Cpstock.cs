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

    public int TiempoReposo { get; set; }

    public int InventarioSeguridad { get; set; }

    public int? Resultado { get; set; }
}
