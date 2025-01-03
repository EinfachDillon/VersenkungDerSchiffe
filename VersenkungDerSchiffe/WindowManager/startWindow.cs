using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VersenkungDerSchiffe.WindowManager;

class startWindow
    {
    private FensterManager manager;

    public startWindow(FensterManager manager)
    {
        this.manager = manager;
        initialisiereSzene();
    }
    public void initialisiereSzene()
    {
        RowDefinition rowDef = new RowDefinition();
        rowDef.Height = new GridLength(1, GridUnitType.Auto);
        manager.AddRowToWindow(rowDef);

        RowDefinition rowDef2 = new RowDefinition();
        rowDef.Height = new GridLength(1, GridUnitType.Auto);
        manager.AddRowToWindow(rowDef2);






        manager.addTextToWindow("Klicke um zu starten!", 0, 0);

        Button buttonstart = new Button() { Content = "Start" };
        buttonstart.Click += Button_Click;
        manager.AddElementToWindow(buttonstart,1,0);

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        manager.switchGamewindow();
    }


}

