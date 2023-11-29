using System;
using System.Collections.Generic;
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
}
