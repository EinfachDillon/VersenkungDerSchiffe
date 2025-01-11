using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using VersenkungDerSchiffe.WindowManager;


class gameWindow
{
    public Button[,] buttonArray;
    private FensterManager manager;
    Grid buttongrid = new Grid();
    ImageManager imageManager;

    public gameWindow(FensterManager manager)
    {
        this.manager = manager;
        imageManager = new ImageManager();
        intialsiereSpielfenster();
    }

    public void intialsiereSpielfenster()
    {
        manager.resetGrid();

        manager.addManyRows(2,100);
        manager.addManyRows(10);
        manager.addManyRows(1, 100);

        manager.addManyColumns(1,100);
        manager.addManyColumns(10);
        manager.addManyColumns(1,100);

        manager.addTextToWindow("SP-1", 0, 1, name: "Spieler");
        manager.addTextToWindow("Platziere Boote!", 0, 0,1,12, "Anweisung");
        manager.addTextToWindow("", 13, 0 ,1,12, "Boote");
        manager.addTextToWindow("", 0, 7, 1, 5, "letzteAktion");

        createGrid10x10();
        fillButtonsToGrid();
        addFinishTurnButton();

        manager.AddElementToWindow(buttongrid, 2, 1,10,10);
        addIndications();


    }


    public void addFinishTurnButton()
    {
        Button buttonfinish = new Button() { Content = imageManager.getHaeckchen(), Background = Brushes.Transparent, };
        buttonfinish.Click += FinishButton_Click;
        manager.AddElementToWindow(buttonfinish, 12, 11);
    }



    public void addswitchSichtbaresButton()
    {
        Button buttonownbrett = new Button() { Content = "", Background = Brushes.White, FontFamily = new FontFamily("Garamond"), FontWeight = FontWeights.Bold, FontSize = 25 };
        buttonownbrett.Click += switchSichtbaresBrettButton_Click;
        manager.AddElementToWindow(buttonownbrett, 12, 2,1,8, "switchSichtbaresBrett");
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
            buttongrid.RowDefinitions.Add(new RowDefinition());
            buttongrid.ColumnDefinitions.Add(new ColumnDefinition());
          
        }
    }

    public void fillButtonsToGrid()
    {
        buttonArray = new Button[10, 10];
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Button button = new Button()
                {
                    Content = imageManager.giveButtonImage(0),
                    Tag = x + "-" + y,
                    Background = Brushes.Transparent,

                };
                button.PreviewMouseLeftButtonDown += BrettButton_MouseLeftButtonDown;
                buttonArray[x, y] = button;
                Grid.SetRow(button, x);
                Grid.SetColumn(button, y);
                buttongrid.Children.Add(button);


                button.MouseEnter += (s, e) =>
                {
                    button.Opacity = 0.5;
                };

                button.MouseLeave += (s, e) =>
                {
                    button.Opacity = 1;
                }; 

            }
        }
        
    }


    // update alle Buttons mit den neuen Values des Arrays
    public void refreshButtons(int[,] brettarray)
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {

                buttonArray[x, y].Content = imageManager.giveButtonImage(brettarray[x, y]);
            }
        }
    }

    public void addSichtschutz()
    {
        Button button = new Button() { Content = "Spielerwechsel, klicken zum fortfahren", FontFamily = new FontFamily("Garamond"), FontWeight = FontWeights.Bold, FontSize = 80 };
        button.Click += SichtschutzButton_Click;
        manager.registerName(button,"Sichtschutz");
        manager.AddElementToWindow(button, 0, 0,13,12);
    }

    public void removeSichtschutz()
    {
        manager.removeElementfromWindow("Sichtschutz");
    }

    //BUTTON EVENTS

    public void switchSichtbaresBrettButton_Click(object sender, RoutedEventArgs e)
    {
        manager.spielmanager.switchSichtbaresButtonPressed();

    }

    public void FinishButton_Click(object sender, RoutedEventArgs e)
    {

        manager.spielmanager.finishButtonPressed();
    }

    private void BrettButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Button button = sender as Button;

        string[] pos = button.Tag.ToString().Split('-');

        //Gibt Methode buttoPressed bei Spieler die Position des gedrückten Buttons mit

        manager.spielmanager.buttonPressed(Int32.Parse(pos[0]), Int32.Parse(pos[1]));
    }

    public void SichtschutzButton_Click(object sender, RoutedEventArgs e)
    {
        removeSichtschutz();
    }



    }







