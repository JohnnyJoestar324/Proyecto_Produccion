using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Produccion.Models;

[Table("CPFijo")]
public partial class Cpfijo
{
    [Key]
    [Column("IDCPFijo")]
    public short Idcpfijo { get; set; }
    
    [Display(Name = "Demanda:")]
    public int Demanda { get; set; }

    [Display(Name = "Inventario Pedido:")]
    public int InventarioPedido { get; set; }
    [Display(Name = "Inventario Seguridad:")]
    public int InventarioSeguridad { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    [Display(Name = "Resultado:")]
    public string? Resultdado { get; set; }
}
