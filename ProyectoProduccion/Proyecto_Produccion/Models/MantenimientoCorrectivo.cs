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

    [Display(Name = "Horas de Trabajo:")]
    public int? HorasTrabajo { get; set; }
    [Display(Name = "MTBF:")]
    public double? Mtbf { get; set; }
    [Display(Name = "Numero de Fallas:")]
    public int? NumeroFallas { get; set; }
    [Display(Name = "Duración de Tarea:")]
    public int DuracionTarea { get; set; }

    [Column(TypeName = "money")]
    [Display(Name = "Costo x Hora de Trabajo:")]
    public decimal CostoHoraTrabajo { get; set; }

    [Column(TypeName = "money")]
    [Display(Name = "Repuestos:")]
    public decimal Repuestos { get; set; }

    [Column(TypeName = "money")]
    [Display(Name = "Tareas Adicionales:")]
    public decimal TareasAdicionales { get; set; }

    [Display(Name = "Retraso Logístico:")]
    public int? RetrasoLogistico { get; set; }

    [Column(TypeName = "money")]
    [Display(Name = "Costo Unitario:")]
    public decimal CostoUnitario { get; set; }

    [Column(TypeName = "money")]
    [Display(Name = "Costo x Falla Única:")]
    public decimal CostoFallaUnica { get; set; }
    [Display(Name = "Resultado:")]
    public double? Resultado { get; set; }
}
