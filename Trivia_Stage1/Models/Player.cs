using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("Player")]
[Index("Email", Name = "UQ__Player__A9D10534871EDB3E", IsUnique = true)]
public partial class Player
{
    [Key]
    public int PlayerId { get; set; }

    [StringLength(60)]
    public string PlayerName { get; set; } = null!;

    [StringLength(120)]
    public string? Email { get; set; }

    public int NumOfPoints { get; set; }

    public int NumPlayerType { get; set; }

    [Column("pass")]
    [StringLength(100)]
    public string? Pass { get; set; }

    [ForeignKey("NumPlayerType")]
    [InverseProperty("Players")]
    public virtual TypeOfPlayer NumPlayerTypeNavigation { get; set; } = null!;

    [InverseProperty("Player")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
