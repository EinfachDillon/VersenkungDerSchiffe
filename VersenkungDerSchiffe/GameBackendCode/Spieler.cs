﻿
using System.Windows;
using VersenkungDerSchiffe.SchiffVersenkungCode;
using VersenkungDerSchiffe.WindowManager;

class Spieler
    {
    FensterManager wmanager;
    public Spielbrett eigenbrett = new Spielbrett();
    public Spielbrett schussbrett = new Spielbrett();
    TextUpdater textUpdater;
    public KreuzManager kreuzhelper;
    public Boolean spielzuguebrig = true;


    public Spieler(FensterManager wmanager,TextUpdater textUpdater) {
    
        this.wmanager = wmanager;
        this.textUpdater = textUpdater;
        kreuzhelper = new KreuzManager(eigenbrett, textUpdater);
        
    }

    public void spielzug(int x, int y,Boolean getroffen)
    {
        if (spielzuguebrig&&schussbrett.getFieldInfo(x,y)==0)
        {
            if (getroffen)
            {
                schussbrett.setFieldInfo(x, y, 2);
                textUpdater.updateletzteAktionSpielzug(x,y,0);
                
            }
            else
            {
                schussbrett.setFieldInfo(x, y, 1);
                spielzuguebrig = false;
                textUpdater.updateletzteAktionSpielzug(x, y, 1);
                textUpdater.updateletzteAktionSpielzug(x, y, 2);
            }
            refreshSchussbrett();
        }
    }

    public void setzeBoot(int x, int y)
    {

        if (kreuzhelper.kreuzEntfernbar(x,y)== true)
        {

            kreuzhelper.entferneKreuz(x,y);
            eigenbrett.setFieldInfo(x, y, 0);
            
            textUpdater.updateletzteAktionPlacement(x, y, 0);
        }
        else if (kreuzhelper.placementGueltig(x, y))
        {
            eigenbrett.setFieldInfo(x, y, 1);
            textUpdater.updateletzteAktionPlacement(x, y, 1);

        }
        else
        {
            textUpdater.updateletzteAktionPlacement(x, y, 2);

        }
        refreshEigenbrett();
        textUpdater.refreshBootCounterText(kreuzhelper.bootZaehler.boatsToText());
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
