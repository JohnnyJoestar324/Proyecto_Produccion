using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Produccion.Models;

[Table("LUC")]
public partial class Luc
{
    [Key]
    [Column("IDLUC")]
    public short Idluc { get; set; }

    public int RequerimientoBruto { get; set; }

    public int Periodo { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoMantenimiento { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoDeOrdenar { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoTotal { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoTotalU { get; set; }
}
