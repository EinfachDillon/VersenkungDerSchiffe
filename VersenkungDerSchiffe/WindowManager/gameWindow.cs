using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using VersenkungDerSchiffe.WindowManager;


class gameWindow
{
    public Button[,] buttonArray;
    private FensterManager manager;
    Grid buttongrid = new Grid();

    public gameWindow(FensterManager manager)
    {
        this.manager = manager;
        intialsiereSpielfenster();
    }

    public void intialsiereSpielfenster()
    {
        manager.resetGrid();

        manager.addManyRows(2,50);
        manager.addManyRows(10);
        manager.addManyRows(1, 50);

        manager.addManyColumns(1,50);
        manager.addManyColumns(10);
        manager.addManyColumns(1,50);

        manager.addTextToWindow("SP-1", 0, 0, name: "Spieler");
        manager.addTextToWindow("Platziere Boote!", 0, 1,1,8, "Anweisung");
        manager.addTextToWindow("Boote übrig: 1xSchlachtschiff, 2xZerstörer, 3xKreuzer, 2xU-Boote", 0, 7 ,1,5, "Boote");

        createGrid10x10();
        fillButtonsToGrid();
        addFinishTurnButton();
        addSeeOwnBrettButton();

        manager.AddElementToWindow(buttongrid, 2, 1,10,10);
        addIndications();

    }


    public void addFinishTurnButton()
    {
        Button buttonfinish = new Button() { Content = "Fertig?" };
        buttonfinish.Click += Button_Click;
        manager.AddElementToWindow(buttonfinish, 12, 11);
    }

    public void Button_Click(object sender,RoutedEventArgs e)
    {

        manager.spielmanager.finishButtonPressed();
    }

    public void addSeeOwnBrettButton()
    {
        Button buttonfinish = new Button() { Content = "Schuss-Brett anzeigen" };
        buttonfinish.Click += Button_Click1;
        manager.AddElementToWindow(buttonfinish, 12, 10);
    }
    public void Button_Click1(object sender, RoutedEventArgs e)
    {
        if(sender is Button button) { manager.spielmanager.showOwnBrettButtonPressed(button); }

    }


    public void addIndications()
    {
       for(int i =0; i<10; i++)
        {
            manager.addTextToWindow((i+1).ToString(), i+2, 0);
        }
        int z = 65;
        for (int i = 0; i < 10; i++)
        {
            manager.addTextToWindow(((char)z).ToString(), 1, i+1);
            z = z+1;
        } 
    } 








    public void createGrid10x10()
    {

        for (int i = 0; i < 10; i++)
        {
            buttongrid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition());
            buttongrid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition());
          
        }
    }

    public void fillButtonsToGrid()
    {
        buttonArray = new Button[10, 10];
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Button button = new Button() { Content = 0, Tag = x + "-" + y };
                button.PreviewMouseLeftButtonDown += Button_MouseLeftButtonDown;
                buttonArray[x, y] = button;
                Grid.SetRow(button, x);
                Grid.SetColumn(button, y);
                buttongrid.Children.Add(button);
            }
        }
    }

    private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Button button = sender as Button;

        string[] pos = button.Tag.ToString().Split('-');

        //Gibt Methode buttoPressed bei Spieler die Position des gedrückten Buttons mit

        manager.spielmanager.buttonPressed(Int32.Parse(pos[0]), Int32.Parse(pos[1]));
    }




    // update alle Buttons mit den neuen Values des Arrays
    public void refreshButtons(int[,] brettarray)
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                buttonArray[x, y].Content = brettarray[x, y].ToString();
            }
        }
    }




}


