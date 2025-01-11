using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VersenkungDerSchiffe.WindowManager;

class startWindow
    {
    private FensterManager manager;
    ImageManager imageManager;

    public startWindow(FensterManager manager)
    {
        this.manager = manager;
        imageManager = new ImageManager();
        initialisiereSzene();
        
    }
    public void initialisiereSzene()
    {

        manager.resetGrid();

        setBackground();

        manager.addManyRows(1, 100);
        manager.addManyRows(1,100);
        manager.addManyRows(1, 10);
        manager.addManyRows(1,100);
        manager.addManyRows(1, 10);

        manager.addManyColumns(1, 10);
        manager.addManyColumns(1,400);
        manager.addManyColumns(1, 10);

        manager.addTextToWindow("Versenkung der Schiffe!", 0, 1);
        addStartButton();
        addEndButton();
    }
    public void setBackground()
    {
        manager.fenster.raster.Background = imageManager.getHintergrund();
        }
    public void addStartButton()
    {
        Button buttonfinish = new Button() { Content = "Start", FontFamily = new FontFamily("Garamond") ,Background = Brushes.White, FontWeight = FontWeights.Bold };
        buttonfinish.Click += StartButton_Click;
        manager.AddElementToWindow(buttonfinish, 1, 1);
    }

    public void addEndButton()
    {
        Button buttonfinish = new Button() { Content = "Beenden", FontFamily = new FontFamily("Garamond"), Background = Brushes.White, FontWeight = FontWeights.Bold };
        buttonfinish.Click += EndButton_Click;

        manager.AddElementToWindow(buttonfinish, 3, 1);
    }

    //BUTTON EVENTS

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        manager.spielmanager.initialzeVars();
        manager.switchGamewindow();
    }

    private void EndButton_Click(object sender, RoutedEventArgs e)
    {
        manager.fenster.Close();
    }

}

