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
        private Player currentPlayer;

        //Implememnt interface here
        public bool ShowLogin()
        {
            //Console.WriteLine("Please enter your name");
            //string name=Console.ReadLine();
            char c = ' ';
            while (c != 'B' && c != 'b' && this.currentPlayer == null)
            {
                //Clear screen
                CleareAndTtile("Login");
                Console.WriteLine("Please enter your email");
                string email = Console.ReadLine();
                Console.WriteLine("Please enter your password");
                string password = Console.ReadLine();


                Console.WriteLine("Connecting to Server...");
                //Create instance of Business Logic and call the signup method
                // *For example:
                try
                {
                    TriviaDbContext db = new TriviaDbContext();
                    this.currentPlayer = db.Login(email, password);
                    Console.WriteLine("YOU SUCCESSFULLY LOGGED IN");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to login!");
                }
                //Provide a proper message for example:
                Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                //Get another input from user
                c = Console.ReadKey(true).KeyChar;
                return true;
            }

        
            return (false);
        }
        public bool ShowSignup()
        {
            //Logout user if anyone is logged in!
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            this.currentPlayer = null;
            bool success = true;
            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu
            char c = ' ';
            while (c != 'B' && c != 'b' && this.currentPlayer == null)
            {
                //Clear screen
                CleareAndTtile("Signup");

                Console.Write("Please Type your email: ");
                string email = Console.ReadLine();
                while (!IsEmailValid(email))
                {
                    Console.Write("Bad Email Format! Please try again:");
                    email = Console.ReadLine();
                }

                Console.Write("Please Type your password: ");
                string password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    password = Console.ReadLine();
                }

                Console.Write("Please Type your Name: ");
                string name = Console.ReadLine();
                while (!IsNameValid(name))
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
                    this.currentPlayer = db.SignUp(name,  email, password);
                    Console.WriteLine("YOU SUCCESSFULLY SIGNED UP");
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                    success = false;
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
            return (success);
        }

        public void ShowAddQuestion()
        {
            CleareAndTtile("Add Questions");
            char c = ' ';
            //while (c != 'B' && c != 'b')
            //{
                if (this.currentPlayer.NumOfPoints >= 100 || this.currentPlayer.NumPlayerType == 1)
                {
                    Console.WriteLine("Enter x if the subject of your question is: " + '\n' + "1- sports" + '\n' + "2- politics" + '\n' + "3- history" + '\n' + "4- science" + '\n' + "5- ramon high school");
                    int subject = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter your question");
                    string ques = Console.ReadLine();

                    Console.WriteLine("Please enter the answer to your question");
                    string ans = Console.ReadLine();

                    Console.WriteLine("Please enter 3 wrong answers");
                    string wrong1 = Console.ReadLine();
                    string wrong2 = Console.ReadLine();
                    string wrong3 = Console.ReadLine();

                    Question q = new Question()
                    {
                        PlayerId = this.currentPlayer.PlayerId,
                        SubId = subject,
                        QuestionContent = ques,
                        CorrectAnswer = ans,
                        WrongAnswer1 = wrong1,
                        WrongAnswer2 = wrong2,
                        WrongAnswer3 = wrong3,
                        StatusIdquestion = 1
                    };
                    try
                    {
                        TriviaDbContext db = new TriviaDbContext();
                        db.AddQuestion(q);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Couldn't add question!");
                    }

                }
                else
                {
                    Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                }
                c = Console.ReadKey(true).KeyChar;
           // }
        }

        public void ShowPendingQuestions()
        {
            if(currentPlayer.NumPlayerType==2 || currentPlayer.NumPlayerType == 1)
            {
                TriviaDbContext db = new TriviaDbContext();
                List<Question> q = db.GetPendingQuestions();
                foreach (Question ques in q)
                {
                    //CleareAndTtile("Question #"+ques.QuestionNum);
                    Console.WriteLine(ques.QuestionContent);
                    Console.WriteLine("Correct answers: " + ques.CorrectAnswer + '\n' + '\n' + "Wrong answers: " + '\n' + ques.WrongAnswer1 + '\n' + ques.WrongAnswer2 + '\n' + ques.WrongAnswer3);

                    Console.WriteLine("If you want to:" + '\n' + " approve this question click a, " + '\n' + "disapprove this question click d" + '\n' + "skip click s");
                    char ch = char.Parse(Console.ReadLine());
                    if (ch == 'a')
                    {
                        try
                        {
                            ques.StatusIdquestion = 2;
                            db.UpdateStatusQuestion(ques);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Couldn't add question!");
                        }

                    }
                    else if (ch == 'd')
                    {
                        try
                        {
                            ques.StatusIdquestion = 3;
                            db.UpdateStatusQuestion(ques);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Couldn't add question!");
                        }
                    }
                    else if (ch == 's')
                    {
                        Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                    }

                }
            }
           
            Console.ReadKey(true);
        }
        public void ShowGame()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
        public void ShowProfile()
        {
            CleareAndTtile("Profile");
            char c = ' ';

            //while (c != 'B' && c != 'b')
            //{
                //TriviaDbContext db = new TriviaDbContext();
                //this.currentPlayer = db.Profile(currentPlayer.Email);
                Console.WriteLine("Email: " + currentPlayer.Email + '\n' + "Name: " + currentPlayer.PlayerName + '\n' + "Passowrd: " + currentPlayer.Pass + '\n' + "Player Type:" + currentPlayer.NumPlayerTypeNavigation.PlayerType + '\n' + "Points: " + currentPlayer.NumOfPoints);
                Console.WriteLine("Press y if you want to update your email, password or name");
                char ch = char.Parse(Console.ReadLine());
                if (ch == 'y')
                {
                    Console.WriteLine("Press e if you want to update your email");
                    char update=char.Parse(Console.ReadLine());
                    if (update == 'e')
                    {
                        Console.WriteLine("Please enter your new email");
                        string email=Console.ReadLine();
                        while (!IsEmailValid(email))
                        {
                            Console.Write("Bad email format! Please try again:");
                            email = Console.ReadLine();
                        }
                        currentPlayer.Email = email;
            

                    }

                    Console.WriteLine("Press p if you want to update your password");
                     update = char.Parse(Console.ReadLine());
                    if (update == 'p')
                    {
                        Console.WriteLine("Please enter your new password");
                        string pass = Console.ReadLine();
                        while (!IsPasswordValid(pass))
                        {
                            Console.Write("Bad password format! Please try again:");
                            pass = Console.ReadLine();
                        }
                        currentPlayer.Pass= pass;
                    }

                    Console.WriteLine("Press n if you want to update your name");
                     update = char.Parse(Console.ReadLine());
                    if (update == 'n')
                    {
                        Console.WriteLine("Please enter your new name");
                        string name = Console.ReadLine();
                        while (!IsNameValid(name))
                        {
                            Console.Write("Bad name format! Please try again:");
                            name = Console.ReadLine();
                        }
                        currentPlayer.PlayerName = name;
                    }
                    try
                    {
                        TriviaDbContext db = new TriviaDbContext();
                        db.UpdatePlayer(currentPlayer);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("You're wrong");
                    }
                   
                }
                else
                {
                    Console.WriteLine("Press (B)ack to go back or any other key to signup again...");

                }

               // c = Console.ReadKey(true).KeyChar;

           // }
            c = Console.ReadKey(true).KeyChar;
        }

        //Private helper methodfs down here...
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
