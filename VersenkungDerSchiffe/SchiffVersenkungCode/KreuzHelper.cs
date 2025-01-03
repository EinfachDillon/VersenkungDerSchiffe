

using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows;
using VersenkungDerSchiffe.SchiffVersenkungCode;

class KreuzHelper
    {

    //richtungkreuz bestimmt die richtung der letzten kruezes -1 => x-1 1 => x+1 -2 => y-1 2=>2

    boatCounter bootZaehler;

    Spielbrett brett;
    public KreuzHelper(Spielbrett brett)
    {
        this.brett = brett;
        bootZaehler = new boatCounter();
    }

    //checkt ob das Kreuz placement gültig, noch genug kreuze vorhanden, kein kreuz neben 
    public Boolean placementGueltig(int x,int y)
    {
        //kein Kreuz auf Kreuz?
        //kein diagonales umliegendes Kreuz?
        //keine nebenliegende Kreuze?
        
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

    //implementieren wenn Kreuz zwischen 2 Kreuze gesetzt wird
    //auch boot
    public int getBootLaenge(int x,int y)
    {
        (int,int) richtungen = getRichtung(x,y);

        int richtungx = richtungen.Item1;
        int richtungy = richtungen.Item2;

        int laenge = 0;
        if (richtungx == 0 && richtungy == 0) { return laenge; }
        else
        {

            while (brett.getFieldInfo(x + richtungx, y + richtungy) == 1)
            {
                laenge++;
                x = x + richtungx;
                y = y + richtungy;

                if (x + richtungx < 0 || y + richtungy < 0 || x + richtungx > 9 || y + richtungy > 9) { break; }
            }

            return laenge;
        }
    }

    //zählt die Boote mit und sagt ob dieser noch verfügbar ist
    //Boot Klase
    public Boolean bootWeiterführenMoeglich(int x,int y)
    {
        int bootlaenge = getBootLaenge(x, y);
        if( bootlaenge < 1 ) { return true; }
        else if ( bootlaenge < 5) {

            
            if (bootZaehler.isBoatAvailable(bootlaenge+1))
            {
                
                //vorheriges Boot aktivieren
                bootZaehler.switchBoatActive(bootlaenge, true);
                //neues Boot deaktivieren
                bootZaehler.switchBoatActive(bootlaenge+1, false);
               // bootZaehler.gibBoatArray();
                return true;
            }
            else {
                MessageBox.Show("Maximale Anzahl der Bootsorte " + bootZaehler.getBoatName(bootlaenge+1) + " erreicht!");
                return false; }

        }
        else {
            
            return false; }

    }

    //kinda Boot
    public void entferneKreuz(int x, int y)
    {
        int bootlaenge = getBootLaenge(x, y);

        if (bootlaenge >= 1)
        {
            bootZaehler.switchBoatActive(bootlaenge+1, true);
            MessageBox.Show("Boot " + bootZaehler.getBoatName(bootlaenge + 1) + " entfernt!");
        }
    }

    //Kreuzhelp
    public Boolean alleKreuzeGültig()
    {
        Boolean allePlatziert = bootZaehler.allBoatsPlaced();
        Boolean alleinstehend = alleinstehendeKreuze();

        if (allePlatziert&&!alleinstehend)
        {
            MessageBox.Show("Hier");
            return true;
        }
        else
        {
            if (!allePlatziert) { MessageBox.Show("Platziere alle Boote"); }
            if (alleinstehend) { MessageBox.Show("Entferne alle alleinestehenden Kreuze"); }
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

