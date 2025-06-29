using System.Drawing;
using System.Threading;

namespace EveHQ.CoreLib;

public class G15Lcd
{

    public delegate void UpdateAPIEventHandler();
    event  UpdateAPIEventHandler UpdateAPI; 

    //public static Timer TmrLcdChar = new Timer();

    public static bool StartAPIUpdate { get; set; } = false;
    public static bool SplashFlag { get; set; } = false;

    public static bool InitLcd()
    {
        //TODO: HQ.IsG15LcdActive = true;
        return true;
    }
    
    public void CloseLcd() {}

    public static Image IntroScreenImage()
    {
        return new Bitmap(1,1);
    }

    public static Image DrawSkillTrainingInfo(string lcdPilot)
    {
        return new Bitmap(1,1);
    }

    public static Image DrawCharacterInfo(string lcdPilot)
    {
        return new Bitmap(1,1);
    }
   
}