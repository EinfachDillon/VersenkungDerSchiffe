
using System.Windows;
using VersenkungDerSchiffe.SchiffVersenkungCode;
using VersenkungDerSchiffe.WindowManager;

class Spieler
    {
    FensterManager wmanager;
    public Spielbrett eigenbrett = new Spielbrett();
    public Spielbrett schussbrett = new Spielbrett();
    public KreuzHelper kreuzhelper;
    public Boolean spielzuguebrig = true;


    public Spieler(FensterManager wmanager) {
    
        this.wmanager = wmanager;
        kreuzhelper = new KreuzHelper(eigenbrett);
    }

    public void spielzug(int x, int y,Boolean getroffen)
    {
        if (spielzuguebrig)
        {
            if (getroffen)
            {
                schussbrett.setFieldInfo(x, y, 2);
            }
            else
            {
                schussbrett.setFieldInfo(x, y, 1);
                spielzuguebrig = false;
            }
            refreshSchussbrett();
        }
        else
        {
            MessageBox.Show("kein Spielzug übrig");
        }

        //Spieler wählt gegnerisches Feld aus zum attakieren

    }

    public void setzeBoot(int x, int y)
    {

        if (kreuzhelper.kreuzAufKreuz(x,y) == true)
        {
            kreuzhelper.entferneKreuz(x,y);
            eigenbrett.setFieldInfo(x, y, 0);
        }
        else if (kreuzhelper.placementGueltig(x, y))
        {
            eigenbrett.setFieldInfo(x, y, 1);

           wmanager.updateTextBlock("Kreuz wurde auf Position" + x + "-" + y + "gesetzt.","Anweisung");
        }
        else
        {
            wmanager.updateTextBlock("Kreuzplatzierung ungültig", "Anweisung");
        }
        refreshEigenbrett();
    }

    public void refreshSchussbrett()
    {

        wmanager.gameWindow.refreshButtons(schussbrett.getField());


    }

    public void refreshEigenbrett()
    {
            wmanager.gameWindow.refreshButtons(eigenbrett.getField());
    }
}

