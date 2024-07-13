using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Produccion.Models;

public partial class CantidadKanbane
{
    [Key]
    [Column("IDCantidadKanbanes")]
    public short IdcantidadKanbanes { get; set; }

    [Column("Oferta_DPI")]
    public int? OfertaDpi { get; set; }

    [Column("Demanda_DPI")]
    public int? DemandaDpi { get; set; }

    public int? Demanda { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? Retraso { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? TiempoDeEntrega { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? StockDeSeguridad { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? CantidadAlmacenamiento { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? Resultado { get; set; }
}
