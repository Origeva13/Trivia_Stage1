using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDbContext : DbContext
{
    //פעולת עזר שמקבלת שם אימייל סיסמא ומזירה שחקן חדש שנוסף לבסיס נתונים
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
    //פעולת עזר שמקבלת  אימייל סיסמא ובודקת האם הוא קיים בבסיס  הנתונים
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
    //פעולת עזר שמקבלת שחקן ומעדכנת את הנתונים שלו בבסיס הנתונים
    public void UpdatePlayer(Player p)
    {
        Entry(p).State = EntityState.Modified;
        SaveChanges();
    }
    //פעולת עזר שמקבל עצם מטיפוס שאלה ומוסיפה אותה לבסיס הנתונים
    public void AddQuestion(Question question)
    {
        Entry(question).State= EntityState.Added;
        SaveChanges();
    }
    //  פעולת עזר שיוצרת רשימה חדשה של כל השאלות שהסטטוס שלהם הוא 1 כלומר מחכה לאישור ומחזירה אותו
    public List<Question> GetPendingQuestions()
    {
        List<Question> pending = this.Questions.Where(q=>q.StatusIdquestion==1).ToList();
        return pending;
    }
    //פעולת עזר שמקבלת עצם מטיפוס שאלה ומעדכנת אותו בבסיס הנתונים
    public void UpdateStatusQuestion(Question q)
    {
        Entry(q).State = EntityState.Modified;
        SaveChanges();
    }
    //פעולת עזר שיוצרת רשימה חדשה של כל השאלות שהסטטוס שלהם הוא 2 כלומר מאושרות ומחזירה אותו
    public List<Question> GetApprovedQuestions()
    {
        List<Question> approved = this.Questions.Where(q => q.StatusIdquestion == 2).ToList();
        return approved;
    }
    //פעולת עזר שמקבלת שאלה(אין צורך לקבל שאלה אבל ניתן) ומחזירה מספר רנדומלי בין 1 ל4 ומחזירה את המספר שהתקבל
    public int ShuffleQuestions(Question q)
    {
        
        Random random = new Random();
       int ans=random.Next(1,5);
        return ans;
    }
}
