using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("QuestionStatus")]
public partial class QuestionStatus
{
    [Key]
    [Column("StatusID")]
    public int StatusId { get; set; }

    [StringLength(50)]
    public string StatusOfQuestion { get; set; } = null!;

    [InverseProperty("StatusIdquestionNavigation")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
