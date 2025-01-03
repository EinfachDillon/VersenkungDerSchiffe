using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Markup;

namespace VersenkungDerSchiffe.WindowManager
{
    class FensterManager
    {
        public MainWindow fenster;
        public startWindow startWindow;
        public gameWindow gameWindow;
        public GameManager spielmanager;


        //Erstellt Fenster und zeigt es an
        public FensterManager()
        {

            fenster = new MainWindow();
            fenster.Show();
            startWindow = new startWindow(this);

        }

        public void setGamemanager(GameManager spielmanager)
        {
            this.spielmanager = spielmanager;
        }
        public void switchGamewindow()
        {
            gameWindow = new gameWindow(this);
        }



        //fügt Knopf auf bestimmte Position im Raster hinzu
        public void AddElementToWindow(UIElement element, int row, int column, int rowspan = 1, int colspan = 1)
        {

            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
            Grid.SetRowSpan(element, rowspan);
            Grid.SetColumnSpan(element, colspan);


            fenster.raster.Children.Add(element);

        }

        //fügt Zeile hinzu
        public void AddRowToWindow(RowDefinition rowDef = null)
        {
            if (rowDef == null)
            {
                rowDef = new RowDefinition();
            }
            fenster.raster.RowDefinitions.Add(rowDef);
        }

        //fügt Spalte hinzu
        public void AddColumnToWindow(ColumnDefinition colDef = null)
        {
            if (colDef == null)
            {
                colDef = new ColumnDefinition();
            }
            fenster.raster.ColumnDefinitions.Add(colDef);
        }

        //clear das Raster
        public void resetGrid()
        {

            fenster.raster.Children.Clear();
            fenster.raster.RowDefinitions.Clear();
            fenster.raster.ColumnDefinitions.Clear();
        }

        public void addTextToWindow(string text, int row, int column,int rowspan = 1, int columnspan =1, string name = null)
        {
            TextBlock textBlock = new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                Text = text,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 10)
            };

            if (name != null) { fenster.raster.RegisterName(name, textBlock); }

            AddElementToWindow(textBlock, row, column,rowspan,columnspan);
        }

        public void updateTextBlock(string text, string name)
        {
            TextBlock textBlock = (TextBlock)fenster.raster.FindName(name);
            textBlock.Text = text;
        }

        public void addManyColumns(int count, int widht=0)
        {
            if (widht == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    AddColumnToWindow();
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(widht);
                    AddColumnToWindow(column);
                }
            }
        }

        public void addManyRows(int count, int height = 0)
        {
            if (height == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    AddRowToWindow();
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(height);    
                    AddRowToWindow(row);
                }
            }
        }



    }
}
