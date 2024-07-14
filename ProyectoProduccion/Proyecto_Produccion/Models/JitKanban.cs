using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Produccion.Models;

[Table("JIT_KANBAN")]
public partial class JitKanban
{
    [Key]
    [Column("ID_JIT_KANBAN")]
    public short IdJitKanban { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Demanda { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TiempoVuelta { get; set; }

    public int TamañoRecipiente { get; set; }

    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:#,0.######}", ApplyFormatInEditMode = true)]
    public decimal N { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? InventarioMaximo { get; set; }
}
