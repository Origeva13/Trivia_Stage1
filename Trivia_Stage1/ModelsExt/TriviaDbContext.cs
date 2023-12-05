using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDbContext : DbContext
{
    public Player SignUp(string name, string email, string pass)
    {
        const int PlayerType = 3;
        Player p1 = new Player
        {
            PlayerName = name,
            Email = email,
            NumOfPoints = 0,
            NumPlayerType = PlayerType,
            Pass = pass
        };
    
       this.Players.Add(p1);
        SaveChanges();
        return p1;
    }
    public Player Login( string email, string pass)
    {

        Player? p = this.Players.Where(e =>e.Email == email && e.Pass==pass).Include(pp=>pp.NumPlayerTypeNavigation).FirstOrDefault();
        if (p == null)
        {
            throw new Exception("Email and Password do not exist in DB!");
        }
        else
        {
            return p;
        }
            
    }
    
    public void UpdatePlayer(Player p)
    {
        Entry(p).State = EntityState.Modified;
        SaveChanges();
    }
    public void AddQuestion(Question question)
    {
        Entry(question).State= EntityState.Added;
        SaveChanges();
    }
    public List<Question> GetPendingQuestions()
    {
        List<Question> pending = this.Questions.Where(q=>q.StatusIdquestion==1).ToList();
        return pending;
    }
    public void UpdateStatusQuestion(Question q)
    {
        Entry(q).State = EntityState.Modified;
        SaveChanges();
    }
}
