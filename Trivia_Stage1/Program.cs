using Microsoft.EntityFrameworkCore;
using Trivia_Stage1.UI;

namespace Trivia_Stage1
{
    public class Program
    {
        public static UIMain ui;
        static void Main(string[] args)
        {
            LoginMenu loginMenu = new LoginMenu();
            TriviaScreensImp screens = new TriviaScreensImp();
            ui = new UIMain(loginMenu, screens);
            ui.ApplicationStart();

            //scaffold-DbContext "Server = LAB2-7\SQLEXPRESS; Database=Trivia; Trusted_Connection = True; TrustServerCertificate = True; " Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context TriviaDbContext -DataAnnotations -force
            //LAB2-7\SQLEXPRESS;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False;Command Timeout=0
        }
    }
}