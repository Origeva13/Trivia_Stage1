using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.UI
{
    public class TriviaScreensImp:ITriviaScreens
    {

        //Place here any state you would like to keep during the app life time
        //For example, player login details...
        private Player currentPlayer;//יצרנו שחקן חדש

        //Implememnt interface here
        public bool ShowLogin()//פעולה שמראה את מסך לוג אין
        {
            bool success = true;//משתנה שכאשר הוא לא שווה לנכון הפעולה נגמרת ועוברת למסך הב נגדיר נכון בשביל שהפעולה תרוץ
            char c = ' ';
            while (c != 'B' && c != 'b' && this.currentPlayer == null)//לולאה שרצה כל עוד לא הכניסו את האות בי והשחקן הנוכחי שונה מנל
            {
                //Clear screen
                CleareAndTtile("Login");
                Console.WriteLine("Please enter your email");
                string email = Console.ReadLine();//מקבלים את האימייל שהמשתמש הכניס
                Console.WriteLine("Please enter your password");
                string password = Console.ReadLine();// מקבלים את הסיסמא שהמתשתמש הכניס
                Console.WriteLine("Connecting to Server...");
                //Create instance of Business Logic and call the signup method
                // *For example:
                try// בודקים האם המשתמש קיים בבסיס הנתונים
                {
                    TriviaDbContext db = new TriviaDbContext();
                    this.currentPlayer = db.Login(email, password);//מחפשים את השחקן הנוכחי בעזרת פעולת העזר
                    Console.WriteLine("YOU SUCCESSFULLY LOGGED IN");//הודעה שהמשתמש קיים
                    success = true;// השחקן קיים מחזירים נכון
                }
                catch (Exception ex)//האם השחקן לא קיים 
                {
                    Console.WriteLine("Failed to login!");
                    success = false;//מחזירים לא נכון
                }
                //Provide a proper message for example:
                if (!success)//אם תפסנו לא נכון ניתן למשתמש הזדמנות לחזור חזרה
                    Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                else
                    Console.WriteLine("Press any key to continue...");
                //Get another input from user
                c = Console.ReadKey(true).KeyChar;
                return success;//נחזיר את המשתנה ונעבור למסך הבא
            }

        
            return (false);
        }
        public bool ShowSignup()
        {
            //Logout user if anyone is logged in!
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            this.currentPlayer = null;
            bool success = true;//משתנה המוגדר כאמת ואם ההרשמה מצליחה אז המשתנה נשאר אמת ואם ההרשמה לא מצליחה אז המתשנה נהיה שקר 
            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu
            char c = ' ';
            while (c != 'B' && c != 'b' && this.currentPlayer == null)//לולאה הרצה כל עוד המשתמש לא הכניס את האות בי וכל עוד המשתנה שונה מנל
            {
                //Clear screen
                CleareAndTtile("Signup");

                Console.Write("Please Type your email: ");
                string email = Console.ReadLine();//נותן למשתמש להכניס אימייל
                while (!IsEmailValid(email))//לולאה הרצה כל עוד האימייל שהמשתמש הכניס לא תקין
                {
                    Console.Write("Bad Email Format! Please try again:");
                    email = Console.ReadLine();
                }

                Console.Write("Please Type your password: ");//נותן למשתמש להכניס סיסמא
                string password = Console.ReadLine();
                while (!IsPasswordValid(password))//לולאה הרצה כל עוד הסיסמא שהמשתמש הכניס לא תקינה
                {
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    password = Console.ReadLine();
                }

                Console.Write("Please Type your Name: ");//נותן למשתמש להכניס שם
                string name = Console.ReadLine();
                while (!IsNameValid(name))//לולאה הרצה כל עוד השם שהמשתמש הכניס לא תקין
                {
                    Console.Write("name must be at least 3 characters! Please try again: ");
                    name = Console.ReadLine();
                }


                Console.WriteLine("Connecting to Server...");
                //Create instance of Business Logic and call the signup method
                // *For example:
                try
                {
                    TriviaDbContext db = new TriviaDbContext();
                    this.currentPlayer = db.SignUp(name,  email, password);//מנסה להוסיף את המשתמש לבסיס נתונים בעזרת פעולת עזר
                    Console.WriteLine("YOU SUCCESSFULLY SIGNED UP");//נותן הודעה מתאימה אם ההרשמה הצליחה
                    success = true;//משתנה את המשתנה לאמת
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");//נותן הודעה מתאימה אם ההרשמה לא הצליחה
                    success = false;//משתנה את המשתנה לשקר
                }



                //Provide a proper message for example:
                if (!success)
                    Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                else
                    Console.WriteLine("Press any key to continue...");
                //Get another input from user
                c = Console.ReadKey(true).KeyChar;
            }
            //return true if signup suceeded!
            return (success);//מחזיר את המשתמש לעמוד הבא
        }

        public void ShowAddQuestion()//מסך שאפשר להוסיף שאלה
        {
            CleareAndTtile("Add Questions");
            
                if (this.currentPlayer.NumOfPoints >= 100 || this.currentPlayer.NumPlayerType == 1)//האם המשתמש הוא מנהל או יש לו 100 נקודות
                {
                    Console.WriteLine("Enter x if the subject of your question is: " + '\n' + "1- sports" + '\n' + "2- politics" + '\n' + "3- history" + '\n' + "4- science" + '\n' + "5- ramon high school");
                    int subject = int.Parse(Console.ReadLine());//מקבל אינדקס של נושא השאלה

                    Console.WriteLine("Please enter your question");
                    string ques = Console.ReadLine();//מקבל את תוכן השאלה

                    Console.WriteLine("Please enter the answer to your question");
                    string ans = Console.ReadLine();//מקבל את התשובה הנכונה לשאלה

                    Console.WriteLine("Please enter 3 wrong answers");
                    string wrong1 = Console.ReadLine();//מקבל את התשובה השגויה הראשונה
                    string wrong2 = Console.ReadLine();//מקבל את התשובה השגויה השנייה
                    string wrong3 = Console.ReadLine();//מקבל את התשובה השגויה השלישית

                    Question q = new Question()//יוצרת שאלה חדשה
                    {
                        PlayerId = this.currentPlayer.PlayerId,//האינדקס של השחקן זהה
                        SubId = subject,//הנושא של השאלה הוא האינדקס שקיבלנו
                        QuestionContent = ques, //התוכן ש השאלה הוא תוכן השאלה שקיבלנו
                        CorrectAnswer = ans,// התשובה הנכונה היא התשובה הנכונה שקיבלנו
                        WrongAnswer1 = wrong1,//התשובה השגויה היא התשובה השגויה שקיבלנו
                        WrongAnswer2 = wrong2,//התשובה השגויה היא התשובה השגויה שקיבלנו
                        WrongAnswer3 = wrong3,//התשובה השגויה היא התשובה השגויה שקיבלנו
                        StatusIdquestion = 1//השאלה מחכה לאישור
                    };
                    try
                    {
                        TriviaDbContext db = new TriviaDbContext();
                        db.AddQuestion(q);//נשתמש בפעולת העזר ונוסיף את השאלה לבסיס הנתונים
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Couldn't add question!");
                    }

                }
                else
                {
                    Console.WriteLine("You dont have the position to add Press (B)ack to go back or any other key to signup again...");
                }
                Console.ReadKey(true);
         
        }

        public void ShowPendingQuestions()//פעולה שמראה את השאלות שמחכות לאישור ונותנת לאשר או לפסול את השאלות
        {
            CleareAndTtile("Pending Questions");
            if (currentPlayer.NumPlayerType==2 || currentPlayer.NumPlayerType == 1)//האם המשתמש יכול לפסול או לאשר שאלות
            {
                TriviaDbContext db = new TriviaDbContext();
                List<Question> q = db.GetPendingQuestions();// מקבל רשימה של כל השאלות שמחכות לאישור בעזרת פעולת עזר 
                foreach (Question ques in q)//עובר על כל השאלות שעוד לא אושרו
                {
                    CleareAndTtile("Question #"+ques.QuestionNum);
                    Console.WriteLine(ques.QuestionContent);//מציג את השאלה
                    Console.WriteLine("Correct answers: " + ques.CorrectAnswer + '\n' + '\n' + "Wrong answers: " + '\n' + ques.WrongAnswer1 + '\n' + ques.WrongAnswer2 + '\n' + ques.WrongAnswer3);// מציג את התשובה הנכונה ואת שלושת התשובות הלא נכונות

                    Console.WriteLine("If you want to:" + '\n' + " approve this question click a, " + '\n' + "disapprove this question click d" + '\n' + "skip click s");//שואל את המשתמש אם הוא רוצה לאשר את השאלה, לא לאשר אותה או לעבור לשאלה אחרת
                    char ch = char.Parse(Console.ReadLine());
                    if (ch == 'a')//בודק אם המשתמש בחר לאשר את השאלה
                    {
                        try
                        {
                            ques.StatusIdquestion = 2;//מנסה לשנות את הסטטוס של השאלה למאושר
                            db.UpdateStatusQuestion(ques);//משנה בבסיס הנתונים את סטטוס השאלה בעזרת פעולת עזר
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Couldn't add question!");
                        }

                    }
                    else if (ch == 'd')//בודק אם המשתמש בחר לא לאשר את השאלה
                    {
                        try
                        {
                            ques.StatusIdquestion = 3;//מנסה לשנות את הסטטוס של השאלה ללא מאושר
                            db.UpdateStatusQuestion(ques);// משנה בבסיס נתונים את סטטוס השאלה בעזרת פעולה עזר
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Couldn't add question!");
                        }
                    }
                    else if (ch == 's')//בודק אם המשתמש בחר לעבור הלאה לשאלה הבאה
                    {
                        Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                    }

                }
            }
           
            Console.ReadKey(true);
        }
        public void ShowGame()
        {
            
            char c = ' ';
            CleareAndTtile("Game");
            TriviaDbContext db = new TriviaDbContext();
            List<Question> q = db.GetApprovedQuestions();//מקבל רשימה של השאלות המאושרות בעזרת פעולת עזר 
          
              foreach (Question ques in q)//עובר על כל השאלות ברשימה שהתקבלה
                {
                    
                    CleareAndTtile("GAME");
                    Console.WriteLine("Name: " + currentPlayer.PlayerName + '\n' + "Points: " + currentPlayer.NumOfPoints);//מציג את שם השחקן ואת מספר הנקודות שיש לו
                    Console.WriteLine("Question #" + ques.QuestionNum);// מציג את מספר השאלה
                    Console.WriteLine(ques.QuestionContent);//מציג את תוכן השאלה
                    int ans=db.ShuffleQuestions(ques);//משתמש בפעולת עזר כדי לשנות את המקום של התשובה הנכונה
   
                    if (ans == 1)//בודק אם התשובה הנכונה המקום הראשון
                    {
                        Console.WriteLine("1. " + ques.CorrectAnswer);//מציג את התשובה הנכונה
                        Console.WriteLine("2. " + ques.WrongAnswer1);//מציג את התשובה הלא נכונה הראשונה
                        Console.WriteLine("3. " + ques.WrongAnswer2);//מציג את התשובה הלא נכונה השנייה
                        Console.WriteLine("4. " + ques.WrongAnswer3);//מציג את התשובה הלא נכונה השלישית
                       

                    }
                    else if (ans==2)//בודק אם התשובה הנכונה המקום השני
                    {
                        Console.WriteLine("1. " + ques.WrongAnswer1);//מציג את התשובה הלא נכונה הראשונה
                        Console.WriteLine("2. " + ques.CorrectAnswer);//מציג את התשובה הנכונה
                        Console.WriteLine("3. " + ques.WrongAnswer2);//מציג את התשובה הלא נכונה השנייה
                        Console.WriteLine("4. " + ques.WrongAnswer3);//מציג את התשובה הלא נכונה השלישית

                }
                    else if (ans == 3)//בודק אם התשובה הנכונה המקום השלישי
                    {
                        Console.WriteLine("1. " + ques.WrongAnswer1);//מציג את התשובה הלא נכונה הראשונה
                        Console.WriteLine("2. " + ques.WrongAnswer2);//מציג את התשובה הלא נכונה השנייה
                        Console.WriteLine("3. " + ques.CorrectAnswer);//מציג את התשובה הנכונה
                        Console.WriteLine("4. " + ques.WrongAnswer3);//מציג את התשובה הלא נכונה השלישית
                       
                    }
                    else if (ans == 4)//בודק אם התשובה הנכונה המקום הרביעי
                    {
                        Console.WriteLine("1. " + ques.WrongAnswer1);//מציג את התשובה הלא נכונה הראשונה
                        Console.WriteLine("2. " + ques.WrongAnswer2);//מציג את התשובה הלא נכונה השנייה
                        Console.WriteLine("3. " + ques.WrongAnswer3);//מציג את התשובה הלא נכונה השלישית
                        Console.WriteLine("4. " + ques.CorrectAnswer);//מציג את התשובה הנכונה
                        
                    }
                    
                    Console.WriteLine("Press the number of the answer you think is correct or press 5 to skip question or press 0 to exit the game");//שואל את השחקן איזה תשובה הוא בוחר או אם רוצה לצאת מהמשחק או לדלג על השאלה
                    int ch = int.Parse(Console.ReadLine());
                    if (ch == ans)//בודק אם השחקן בחר בתשובה הנכונה
                    {
                        try
                        {
                            currentPlayer.NumOfPoints += 10;//מנסה להוסיף לשחקן עוד עשר נקודות
                            db.UpdatePlayer(currentPlayer);//משנה את מספר הנקודות של המשתמש בבסיס הנתונים בעזרת פעולת עזר
                           Console.WriteLine("GOOD JOB!! YOU CHOSE THE RIGHT ANSWER!");//אם השחקן צדק בשאלה זה מראה הודעה מתאימה
                    }
                    catch (Exception ex)
                        {
                            Console.WriteLine("Couldn't add points to player!");
                        }
                    }

                    else if (ch == 0)//בודק אם המשתמש רוצה לצאת מהמשחק
                    {
                        
                        break;//עוצר את המשחק

                    }
                    else if (ch == 5)//בודק אם השחקן רוצה לדלג על השאלה
                    {
                       Console.WriteLine("Press any other key to continue...");
                      
                    }
                    else//בודק אם המשתמש לא בחר בתשובה הנכונה ולא רצה לצאת מהמשחק או לדלג על השאלה
                    {
                         try
                         {
                            Console.WriteLine("WRONG ANSWER!!");//אם השחקן טעה בשאלה זה מראה הודעה מתאימה
                              if (currentPlayer.NumOfPoints < 5)//בודק אם יש לשחקן פחות מחמש נקודות
                              {
                                  currentPlayer.NumOfPoints =0;//אם יש לשחקן פחות מחמש נקודות והוא טעה בשאלה אז על מנת שלא יהיו לו מינוס נקודות נאפס את מספר הנקודות שלו 
                              }
                              else
                              {
                                  currentPlayer.NumOfPoints -= 5;//מוריד חמש נקודות לשחקן
                              }
                           
                             db.UpdatePlayer(currentPlayer);//מעדכן בבסיס הנתונים את מספר הנקודות שיש לשחקן בעזרת פעולה עזר
                             Console.WriteLine("Press any key to continue...");
                         }
                         catch (Exception ex)
                         {
                             Console.WriteLine("Couldn't take off points from player!");
                         }
                    }

            }
        }
        public void ShowProfile()
        {
            CleareAndTtile("Profile");
           
                Console.WriteLine("Email: " + currentPlayer.Email + '\n' + "Name: " + currentPlayer.PlayerName + '\n' + "Passowrd: " + currentPlayer.Pass + '\n' + "Player Type:" + currentPlayer.NumPlayerTypeNavigation.PlayerType + '\n' + "Points: " + currentPlayer.NumOfPoints);//מציג את השחקן
                Console.WriteLine("Press y if you want to update your email, password or name or any other key if you don't want to update");//שואל את המשתמש אם הוא רוצה לשנות את השם, אימייל או סיסמא שלו
                char ch = char.Parse(Console.ReadLine());
                if (ch == 'y')//בודק אם המשתמש רוצה לשנות את השם, אימייל או סיסמא שלו
                {
                    Console.WriteLine("Press e if you want to update your email or any other key if you don't want to update");//שואל את השחקן אם הוא רוצה לשנות את האימייל שלו
                    char update=char.Parse(Console.ReadLine());
                    if (update == 'e')//בודק אם המשתמש בחר לשנות את האימייל שלו
                    {
                        Console.WriteLine("Please enter your new email");
                        string email=Console.ReadLine();//נותן למשתמש להכניס את האימייל המעודכן שלו
                    while (!IsEmailValid(email))// בודק אם האימייל שהשמשתמש הכניס לא תקינה
                        {
                            Console.Write("Bad email format! Please try again:");
                            email = Console.ReadLine();
                        }
                        currentPlayer.Email = email;//מעדכן את האימייל של המשתמש
            

                    }

                    Console.WriteLine("Press p if you want to update your password or any other key if you don't want to update");//שואל את המשתמש אם הוא רוצה לשנות את הסיסמא שלו
                     update = char.Parse(Console.ReadLine());
                    if (update == 'p')//בודק אם השחקן בחר לשנות את הסיסמא שלו
                    {
                        Console.WriteLine("Please enter your new password");
                        string pass = Console.ReadLine();//נותן למשתמש להכניס סיסמא חדשה
                        while (!IsPasswordValid(pass))//בודק אם הסיסמא שהכניס השחקן לא תקינה
                        {
                            Console.Write("Bad password format! Please try again:");
                            pass = Console.ReadLine();
                        }
                        currentPlayer.Pass= pass;//מעדכן את הסיסמא של המשתמש
                    }

                    Console.WriteLine("Press n if you want to update your name or any other key if you don't want to update");//שואל את המשתמש אם הוא רוצה לשנות את השם שלו
                     update = char.Parse(Console.ReadLine());
                    if (update == 'n')//בודק אם המשתמש בחר לשנות את השם שלו
                    {
                        Console.WriteLine("Please enter your new name");
                        string name = Console.ReadLine();//נותן לשחקן להכניס את השם מעודכן שלו
                        while (!IsNameValid(name))//בודק אם השם שהשחקן הכניס לא תקינה
                        {
                            Console.Write("Bad name format! Please try again:");
                            name = Console.ReadLine();
                        }
                        currentPlayer.PlayerName = name;//משנה את השם של השחקן
                    }
                    try
                    {
                        TriviaDbContext db = new TriviaDbContext();
                        db.UpdatePlayer(currentPlayer);//מעדכן את פרטי השחקן בבסיס הנתונים בעזרת פעולת עזר
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("You're wrong");
                    }
                   
                }
                else//אם המשתמש לא רוצה לעדכן את הפרטים שלו
                {
                    Console.WriteLine("Press (B)ack to go back or any other key to signup again...");

                }

           
            Console.ReadKey(true);
        }

        //Private helper methods down here...
        private void CleareAndTtile(string title)
        {
            Console.Clear();
            Console.WriteLine($"\t\t\t\t\t{title}");
            Console.WriteLine();
        }

        private bool IsEmailValid(string emailAddress)
        {
            var pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }

        private bool IsPasswordValid(string password)
        {
            return password != null && password.Length >= 3;
        }

        private bool IsNameValid(string name)
        {
            return name != null && name.Length >= 3;
        }


    }
}
