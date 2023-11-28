using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Question
{
    [Key]
    public int QuestionNum { get; set; }

    public int PlayerId { get; set; }

    [Column("StatusIDQuestion")]
    public int StatusIdquestion { get; set; }

    [Column("SubID")]
    public int SubId { get; set; }

    [StringLength(240)]
    public string QuestionContent { get; set; } = null!;

    [StringLength(240)]
    public string CorrectAnswer { get; set; } = null!;

    [StringLength(240)]
    public string WrongAnswer1 { get; set; } = null!;

    [StringLength(240)]
    public string WrongAnswer2 { get; set; } = null!;

    [StringLength(240)]
    public string WrongAnswer3 { get; set; } = null!;

    [ForeignKey("PlayerId")]
    [InverseProperty("Questions")]
    public virtual Player Player { get; set; } = null!;

    [ForeignKey("StatusIdquestion")]
    [InverseProperty("Questions")]
    public virtual QuestionStatus StatusIdquestionNavigation { get; set; } = null!;

    [ForeignKey("SubId")]
    [InverseProperty("Questions")]
    public virtual SubQuestion Sub { get; set; } = null!;
}
