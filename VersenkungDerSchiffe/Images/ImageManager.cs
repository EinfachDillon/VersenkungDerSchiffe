using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

 class ImageManager
    {



   // BitmapImage bitmap2 = new BitmapImage(new Uri("Images/image1.png", UriKind.Relative));

    public ImageManager() {

    }

    public Image getEmpty()
    {
        BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/Images/empty.jpg", UriKind.Absolute));
        Image white = new Image
        {
            Source = bitmap,
            Stretch = System.Windows.Media.Stretch.Fill,
            Opacity = 0,
            
        };
        return white;
    }

    public Image getCross()
    {
        BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/Images/cross.png", UriKind.Absolute));
        Image cross = new Image
        {
            Source = bitmap,
            Stretch = Stretch.Fill
        };
        return cross;
    }

    public Image getDestroyed()
    {
        BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/Images/destroyed.png", UriKind.Absolute));
        Image destroyed = new Image
        {
            Source = bitmap,
            Stretch = Stretch.Fill
        };
        return destroyed;
    }

    public Image getHaeckchen()
    {
        BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/Images/fertig.png", UriKind.Absolute));
        Image haeckchen = new Image
        {
            Source = bitmap,
            Stretch = System.Windows.Media.Stretch.Fill
        };
        return haeckchen;
    }




    public Image giveButtonImage(int i)
    {


        if(i == 2){

            return getDestroyed();

        }else if( i == 1)
        {
            return getCross();
        }
        else
        {
            return getEmpty();
        }
    }

    public ImageBrush getHintergrund()
    {
        BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/Images/background.jpg", UriKind.Absolute));
        ImageBrush background = new ImageBrush
        {
            ImageSource = bitmap,
            Opacity=0.7

        };
        return background;
    }

 

    }

