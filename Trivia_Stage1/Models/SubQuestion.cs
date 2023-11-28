using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("SubQuestion")]
public partial class SubQuestion
{
    [Key]
    [Column("SubID")]
    public int SubId { get; set; }

    [StringLength(50)]
    public string SubOfQuestion { get; set; } = null!;

    [InverseProperty("Sub")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
