

using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows;
using VersenkungDerSchiffe.SchiffVersenkungCode;


class KreuzManager
    {

    //richtungkreuz bestimmt die richtung der letzten kruezes -1 => x-1 1 => x+1 -2 => y-1 2=>2

    public BoatCounter bootZaehler;
    TextUpdater textUpdater;

    Spielbrett brett;
    public KreuzManager(Spielbrett brett,TextUpdater textUpdater)
    {
        this.brett = brett;
        bootZaehler = new BoatCounter();
        this.textUpdater = textUpdater;
    }

    //checkt ob das Kreuz placement gültig, noch genug kreuze vorhanden, kein kreuz neben 
    public Boolean placementGueltig(int x,int y)
    {
        //kein Kreuz auf Kreuz?
        //kein diagonales umliegendes Kreuz?
        //keine nebenliegende Kreuze?
        if (kreuzAufKreuz(x,y)) { return false; }
        if (kreuzDiagonal(x, y)) { return false; }
        else if (bootZaehler.allBoatsPlaced() == true) { return false; }
        else if (!bootWeiterführenMoeglich(x, y)) { return false; }  
        else { return true; }

    }

    //Kreuzhelper
    public Boolean kreuzAufKreuz(int x,int y)
    {
        if (brett.getFieldInfo(x, y) == 1) { return true; }
        else { return false; }
    }



    //returned true wenn kreuz diagonal zum zu setzenden kreuz ist
    //Kreuzhelper
    private Boolean kreuzDiagonal(int x,int y)
    {

        if (x!=0&&y!=0&&brett.getFieldInfo(x-1, y-1) == 1) { return true; } //C1 & C2 c4 =>C3

        else if (x!=9&&y!=9&&brett.getFieldInfo(x + 1, y+1) == 1) { return true; } //C3 & C2 c4 => C1

        else if (x!=0&&y!=9&&brett.getFieldInfo(x-1, y + 1) == 1) { return true; } //C2 & C1 c3  =>C4

        else if (x!=9&&y!=0&&brett.getFieldInfo(x+1, y-1) == 1) { return true; } //C4 & C3 c1 =>C2
        else { return false; }
    }

    //wenn kreuz neben pos, dann Boot weiterführen
    //hilfreich für Boot aber wig. Kreuzhelper
    private (int,int) getRichtung(int x, int y)
    {
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
        (int, int, int) laenge = (0, 0, 0);

        (int, int) richtungen = getRichtung(x, y);

        int richtungx = richtungen.Item1;
        int richtungy = richtungen.Item2;

        if (richtungx == 0 && richtungy == 0) { return laenge; }

        laenge.Item1 = getBootLaengeHelper(x, y, richtungx, richtungy);

        //Richtung invertieren
        richtungx = richtungx * -1;
        richtungy = richtungy * -1;
        if (x+richtungx!=-1 && x+richtungx!=10 && y+richtungy!=-1 && y+richtungy!=10)
        {
            laenge.Item2 = getBootLaengeHelper(x, y, richtungx, richtungy);
        }

        laenge.Item3 = laenge.Item1 + laenge.Item2 + 1;


        return laenge;
    }


    public int getBootLaengeHelper(int x, int y,int richtungx, int richtungy)
    {
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

    //zählt die Boote mit und sagt ob dieser noch verfügbar ist
    //Boot Klase
    public Boolean bootWeiterführenMoeglich(int x,int y)
    {
        

        (int,int,int) bootlaenge = getBootLaengen(x, y);
        int gesamtlaenge = bootlaenge.Item3;


        if ( gesamtlaenge <= 1) { return true; }

        else if ( gesamtlaenge <= 5) {

            

            if (bootZaehler.isBoatAvailable(gesamtlaenge))
            {
                boatSwitchHelper(bootlaenge, false);

                return true;
            }
            else {
                textUpdater.maxBootLaengeErreicht(bootZaehler.getBoatName(gesamtlaenge));
                return false; }
            

        }
        else {
            
            return false; }

    }

    public Boolean kreuzEntfernbar(int x,int y)
    {
        if (kreuzAufKreuz(x, y))
        {
            (int, int, int) bootlaenge = getBootLaengen(x, y);

            if (bootlaenge.Item1<2||bootlaenge.Item2<2) { return true; }

            if (bootZaehler.isBoatAvailable(bootlaenge.Item1)) {
                bootZaehler.switchBoatActive(bootlaenge.Item1, false);
                if (bootZaehler.isBoatAvailable(bootlaenge.Item2)) {
                    bootZaehler.switchBoatActive(bootlaenge.Item1, true);
                    return true;
                }
                else
                {
                    bootZaehler.switchBoatActive(bootlaenge.Item1, true);
                }
            }
        }
        return false;
    }

    public void boatSwitchHelper((int,int,int) bootlaenge,Boolean activate)
    {
        int gesamtlaenge = bootlaenge.Item3;

        bootZaehler.switchBoatActive(gesamtlaenge, activate);

        bootZaehler.switchBoatActive(bootlaenge.Item1, !activate);
        bootZaehler.switchBoatActive(bootlaenge.Item2, !activate);

    }

    //kinda Boot
    public void entferneKreuz(int x, int y)
    {

        (int, int,int) bootlaenge = getBootLaengen(x, y);
        int gesamtlaenge = bootlaenge.Item3;


            boatSwitchHelper(bootlaenge, true);
        


    }

    //Kreuzhelp
    public Boolean alleKreuzeGültig()
    {
        Boolean allePlatziert = bootZaehler.allBoatsPlaced();
        Boolean alleinstehend = alleinstehendeKreuze();

        

        if (allePlatziert&&!alleinstehend)
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

    //Kreuzhelper
    public Boolean alleinstehendeKreuze()
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

