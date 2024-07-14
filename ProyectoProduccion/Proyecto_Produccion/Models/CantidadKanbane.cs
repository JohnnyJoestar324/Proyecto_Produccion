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

    [StringLength(6)]
    [Unicode(false)]
    [Display(Name = "Etiqueta:")]
    public string? Etiqueta { get; set; }

    [Column("Oferta_DPI")]
    [Display(Name = "Oferta:")]
    public int? OfertaDpi { get; set; }

    [Column("Demanda_DPI")]
    [Display(Name = "Demanda:")]
    public int? DemandaDpi { get; set; }

    [Display(Name = "DPI:")]
    public int? Demanda { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    [Display(Name = "Retraso:")]
    public decimal? Retraso { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    [Display(Name = "Tiempo de Entrega:")]
    public decimal? TiempoDeEntrega { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    [Display(Name = "Stock de Seguridad:")]
    public decimal? StockDeSeguridad { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    [Display(Name = "Capacidad de Almacenamiento:")]
    public decimal? CantidadAlmacenamiento { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    [Display(Name = "Resultado:")]
    public decimal? Resultado { get; set; }
}
