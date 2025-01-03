
using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

class boatCounter
{
    //in doppel Int umwandeln Slot 1 für Feldergroßße und Slot 2 für aktiv=1 nichtaktiv = 0
    (int, Boolean, String)[] boote = new (int, Boolean, String)[10];

    public boatCounter()
    {

        //Befüllt Array mit den Booten 1x5er,2x4er,3x3er,4x2er
        for (int i = 0; i < 10; i++)
        {
            if (i < 1) boote[i] = (5,true,"Schlachtschiff"); //"Schlachtschiff"
            if (i < 3 && i >= 1) boote[i] = (4,true,"Kreuzer"); //"Kreuzer"
            if (i < 6 && i >= 3) boote[i] = (3,true,"Zerstörer"); //"Zerstörer"
            if (i < 10 && i >= 6) boote[i] = (2,true,"U-Boot"); //"U-Boot"
        }

    }

    public void switchBoatActive(int fields, Boolean active)
    {
        //Länge des Feldes gibt an welches Boot aus dem Array gelöscht wird 
        for (int i = 0; i < 10; i++)
        {
            if (boote[i].Item1 == fields && boote[i].Item2 !=active) { boote[i].Item2 = active; break; }
        }
    }

    public Boolean isBoatAvailable(int fields)
        {
        //Dursucht das Array nach einem Boot das Aktiv ist
        for (int i = 0; i < 10; i++)
        {
            if (boote[i].Item1 == fields && boote[i].Item2 ) { return true; }
        }
        return false;
    }

    public Boolean allBoatsPlaced()
    {
        for (int i = 0; i < 10; i++)
        {
            if (boote[i].Item2==true) { return false; }
        }
        return true;
    }

    public (int, Boolean, String)[] gibBoatArray()
    {
        return boote;
    }

    public String getBoatName(int fields)
    {
        for (int i = 0; i < 10; i++)
        {
            if (boote[i].Item1 == fields) { return boote[i].Item3; }
        }
        return null;
    }

    //Redunadaz vermeiden und findIndex Methode schreiben?



    }



