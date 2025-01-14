

using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows;
using VersenkungDerSchiffe.SchiffVersenkungCode;


class KreuzManager
    {

    //richtungkreuz bestimmt die richtung der letzten kruezes -1 => x-1 1 => x+1 -2 => y-1 2=>2

    public Boats boote;
    TextUpdater textUpdater;
    Spielbrett brett;


    public KreuzManager(Spielbrett brett,Boats boote, TextUpdater textUpdater)
    {
        this.brett = brett;
        this.boote = boote;
        this.textUpdater = textUpdater;
    }

   
    public Boolean placementGueltig(int x,int y)
    {
        /*kein Kreuz auf Kreuz?
        kein diagonales umliegendes Kreuz?
        darf man das Boot weiterführen?*/

        if (kreuzAufKreuz(x,y)) { return false; }
        if (kreuzDiagonal(x, y)) { return false; }
        else if (boote.allBoatsPlaced() == true) { return false; }
        else if (!bootWeiterführenMoeglich(x, y)) { return false; }  
        else { return true; }

    }
    public Boolean kreuzEntfernbar(int x, int y)
    {
        //ist das Kreuz entfernbar, also ob das Boot Array noch Sinn macht wenn das eine Boot entfernt wird

        if (kreuzAufKreuz(x, y))
        {
            (int, int, int) bootlaenge = getBootLaengen(x, y);

            if (bootlaenge.Item1 < 2 || bootlaenge.Item2 < 2) { return true; }

            if (boote.isBoatAvailable(bootlaenge.Item1))
            {
                boote.switchBoatActive(bootlaenge.Item1, false);
                if (boote.isBoatAvailable(bootlaenge.Item2))
                {
                    boote.switchBoatActive(bootlaenge.Item1, true);
                    return true;
                }
                else
                {
                    boote.switchBoatActive(bootlaenge.Item1, true);
                }
            }
        }
        return false;
    }


    public Boolean alleKreuzeGültig()
    {
        //checkt ob alle Kreuze gültig sind, keine zusaätlichen Boote und keine alleinstehende Kreuze vorhanden sind

        Boolean allePlatziert = boote.allBoatsPlaced();
        Boolean alleinstehend = alleinstehendeKreuze();

        if (allePlatziert && !alleinstehend)
        {
            return true;
        }
        else
        {
            if (!allePlatziert) { textUpdater.updateAnweisungBootFinished(0); }
            if (alleinstehend) { textUpdater.updateAnweisungBootFinished(1); }
            return false;
        }

    }


    private Boolean kreuzAufKreuz(int x,int y)
    {
        if (brett.getFieldInfo(x, y) == 1) { return true; }
        else { return false; }
    }

    private Boolean kreuzDiagonal(int x,int y)
    {
        //returned true wenn kreuz diagonal zum zu setzenden kreuz ist

        if (x!=0&&y!=0&&brett.getFieldInfo(x-1, y-1) == 1) { return true; } //C1 & C2 c4 =>C3

        else if (x!=9&&y!=9&&brett.getFieldInfo(x + 1, y+1) == 1) { return true; } //C3 & C2 c4 => C1

        else if (x!=0&&y!=9&&brett.getFieldInfo(x-1, y + 1) == 1) { return true; } //C2 & C1 c3  =>C4

        else if (x!=9&&y!=0&&brett.getFieldInfo(x+1, y-1) == 1) { return true; } //C4 & C3 c1 =>C2
        else { return false; }
    }

    private (int,int) getRichtung(int x, int y)
    {
        //gibt die x,y Richtung des direkt umliegenden Kreuzes an

        int richtungx = 0;
        int richtungy = 0;

        if (x!=0&&brett.getFieldInfo(x - 1, y) == 1)
        {
            richtungx = -1;
 ;
        }
        else if (x!=9&&brett.getFieldInfo(x + 1, y) == 1)
        {
            richtungx = 1;

        }
        else if (y!=0&&brett.getFieldInfo(x, y - 1) == 1)
        {
            richtungy = -1;

        }
        else if (y!=9&&brett.getFieldInfo(x, y + 1) == 1)
        {
            richtungy = 1;

        }
        return (richtungx, richtungy);
    }


    public (int,int,int) getBootLaengen(int x,int y)
    {
        //gibt die Bootlänge wieder unteilt in laenge seite1 vom Kreuz, laenge seite2 von Kreuz und insegesamte länge mit dem zuplatzierenden Kreuz

        (int, int, int) laenge = (0, 0, 0);

        (int, int) richtungen = getRichtung(x, y);

        int richtungx = richtungen.Item1;
        int richtungy = richtungen.Item2;

        if (richtungx == 0 && richtungy == 0) { return laenge; }

        laenge.Item1 = getBootLaengeHelper(x, y, richtungx, richtungy);

        //Richtung invertieren
        richtungx = richtungx * -1;
        richtungy = richtungy * -1;

        //wenn das Kreuz an keinem Border ist dann die Länge der anderes Seite überprüfen
        if (x+richtungx!=-1 && x+richtungx!=10 && y+richtungy!=-1 && y+richtungy!=10)
        {
            laenge.Item2 = getBootLaengeHelper(x, y, richtungx, richtungy);
        }

        laenge.Item3 = laenge.Item1 + laenge.Item2 + 1;


        return laenge;
    }


    private int getBootLaengeHelper(int x, int y,int richtungx, int richtungy)
    {
        //hilfs Klasse um die Länge der Reihe an Kreuzen zu bestimmen die am zulegenden Kreuz ist

        int laenge = 0;

        while (brett.getFieldInfo(x + richtungx, y + richtungy) == 1)
        {
            laenge++;
            x = x + richtungx;
            y = y + richtungy;

            if (x + richtungx < 0 || y + richtungy < 0 || x + richtungx > 9 || y + richtungy > 9) { break; }
        }

        return laenge;
    }

    private Boolean bootWeiterführenMoeglich(int x,int y)
    {
        //checkt ob die weiterführung des Boot möglich ist, die Länge darf nicht über 5 sein, das Boot muss verfügbar sein zum platzieren, anschließend wird das Boot das BootArray verändert

        (int,int,int) bootlaenge = getBootLaengen(x, y);
        int gesamtlaenge = bootlaenge.Item3;


        if ( gesamtlaenge <= 1) { return true; }

        else if ( gesamtlaenge <= 5) {
            if (boote.isBoatAvailable(gesamtlaenge))
            {
                return true;
            }
            else {
                textUpdater.maxBootLaengeErreicht(boote.getBoatName(gesamtlaenge));
                return false; }
        }
        else {
            
            return false; }
    }








    private Boolean alleinstehendeKreuze()
    {
        (int, int) richtung = (0,0);
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
                if((brett.getFieldInfo(x,y)==1)&&(getRichtung(x, y) == richtung))
                {
                    return true;
                }
        }
        return false;
    }



 



}

