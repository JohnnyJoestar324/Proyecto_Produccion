using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Produccion.Models;

[Table("LTC")]
public partial class Ltc
{
    [Key]
    [Column("IDLTC")]
    public short Idltc { get; set; }

    public int Periodo { get; set; }

    public int Unidades { get; set; }

    public int PeridodosMantenidos { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoMantenimiento { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoMantenimientoAcumulado { get; set; }
}
