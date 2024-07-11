using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Produccion.Models;

[Table("EOQ")]
public partial class Eoq
{
    [Key]
    [Column("IDEOQ")]
    public short Ideoq { get; set; }

    public int Demanda { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoPedido { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoMantenimiento { get; set; }

    public int PeriodoSeleccionado { get; set; }

    [Column(TypeName = "money")]
    public decimal? Resultado { get; set; }
}
