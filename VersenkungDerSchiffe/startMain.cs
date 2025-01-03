
using System.Windows;
using VersenkungDerSchiffe;
using VersenkungDerSchiffe.SchiffVersenkungCode;
using VersenkungDerSchiffe.WindowManager;

 class startMain
    {
    [STAThread]
    public static void Main()
    {
        Application game = new Application();
        GameManager gmanager = new GameManager();
        FensterManager wmanager = new FensterManager();

        gmanager.setFenstermanager(wmanager);
        wmanager.setGamemanager(gmanager);
        gmanager.intializeSpieler();


        game.Run();

        


    }

    }

