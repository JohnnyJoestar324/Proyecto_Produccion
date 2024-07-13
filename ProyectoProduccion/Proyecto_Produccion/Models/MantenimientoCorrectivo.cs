using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Produccion.Models;

[Table("MantenimientoCorrectivo")]
public partial class MantenimientoCorrectivo
{
    //cambio completado
    [Key]
    [Column("IDMantenimiento")]
    public short Idmantenimiento { get; set; }

    public int? HorasTrabajo { get; set; }

    public double? Mtbf { get; set; }

    public int? NumeroFallas { get; set; }

    public int DuracionTarea { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoHoraTrabajo { get; set; }

    [Column(TypeName = "money")]
    public decimal Repuestos { get; set; }

    [Column(TypeName = "money")]
    public decimal TareasAdicionales { get; set; }

    public int? RetrasoLogistico { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoUnitario { get; set; }

    [Column(TypeName = "money")]
    public decimal CostoFallaUnica { get; set; }

    public double? Resultado { get; set; }
}
