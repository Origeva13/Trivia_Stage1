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
    //public Player Profile(string email)
    //{
    //    //Player? p1=this.Players.Where(p=>p.Email == email).FirstOrDefault();
    //    //if(p1 == null)
    //    //{
    //    //    throw new Exception("Plyer does not exist in DB yet!");
    //    //}
    //    //else
    //    //{
    //    //    return p1;
    //    //}
    //    Player? pl = new Player();
    //    foreach (Player p in this.Players)
    //    {
    //        if (p.Email.Equals(email))
    //        {
    //            pl = p;
    //        }
    //    }
    //    return pl;
    //}
    public void UpdateEmail(Player p,string email)
    {
        ////this.Players.Set(email);
        //p.Updateemail; 
        //SaveChanges();
        //Player? p = this.Players.Update(email=>)
    }
}
