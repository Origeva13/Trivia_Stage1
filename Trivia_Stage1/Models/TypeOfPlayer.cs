using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("TypeOfPlayer")]
public partial class TypeOfPlayer
{
    [Key]
    public int NumPlayerType { get; set; }

    [StringLength(30)]
    public string PlayerType { get; set; } = null!;

    [InverseProperty("NumPlayerTypeNavigation")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
