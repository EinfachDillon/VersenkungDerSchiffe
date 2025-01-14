using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VersenkungDerSchiffe.WindowManager;


internal class endWindow
    {
        FensterManager manager;
        public endWindow(FensterManager manager) {
        this.manager = manager;
        intialisiereEndSzene();
    }

    public void intialisiereEndSzene()
    {
        manager.resetGrid();

        manager.addManyRows(1, 100);
        manager.addManyRows(1, 100);
        manager.addManyRows(1, 10);
        manager.addManyRows(1, 100);
        manager.addManyRows(1, 10);

        manager.addManyColumns(1, 10);
        manager.addManyColumns(1, 400);
        manager.addManyColumns(1, 10);

        manager.addTextToWindow("Das Spiel ist beendet!",0,1);
        addRematchButton();
        addEndButton(); 
    }

    public void addRematchButton()
    {
        Button buttonfinish = new Button() { Content = "Erneut spielen", FontFamily = new FontFamily("Garamond"), FontWeight = FontWeights.Bold, Background = Brushes.White, };
        buttonfinish.Click += RematchButton_Click;
        manager.AddElementToWindow(buttonfinish, 1, 1);
    }

    public void addEndButton()
    {
        Button buttonfinish = new Button() { Content = "Beenden", FontFamily = new FontFamily("Garamond"), FontWeight = FontWeights.Bold, Background = Brushes.White,  };
        buttonfinish.Click += Button_Click1;
        manager.AddElementToWindow(buttonfinish, 3, 1);
    }

    //BUTTON EVENTS
    public void RematchButton_Click(object sender, RoutedEventArgs e)
    {
        manager.spielmanager.intializeSpiel();
        manager.switchGamewindow();
    }

    public void Button_Click1(object sender, RoutedEventArgs e)
    {
        manager.fenster.Close();
    }



}

