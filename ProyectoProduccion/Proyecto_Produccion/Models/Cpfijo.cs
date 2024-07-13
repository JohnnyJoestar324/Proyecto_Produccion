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

    public int Demanda { get; set; }

    public int InventarioPedido { get; set; }

    public int InventarioSeguridad { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? Resultdado { get; set; }
}
