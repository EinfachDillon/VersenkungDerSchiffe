
using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Media.Animation;

class Boats
{
    //Item1 für Länge des Bootes, Item2 für ist das Boot aktiv auf dem Feld oder nicht, Item3 für den Namen des Bootes
    (int, Boolean, String)[] boote = new (int, Boolean, String)[10];

    public Boats()
    {
        befülleBoote();
    }

    private void befülleBoote()
    {
        //Befüllt Array mit den Booten 1x5er,2x4er,3x3er,4x2er
        for (int i = 0; i < 10; i++)
        {
            if (i < 1) boote[i] = (5, true, "Schlachtschiff"); //"Schlachtschiff"
            if (i < 3 && i >= 1) boote[i] = (4, true, "Kreuzer"); //"Kreuzer"
            if (i < 6 && i >= 3) boote[i] = (3, true, "Zerstörer"); //"Zerstörer"
            if (i < 10 && i >= 6) boote[i] = (2, true, "U-Boot"); //"U-Boot"
        }
    }

    public void switchBoatActive(int fields, Boolean active)
    {
        //Länge des Feldes gibt an welches Boot aus dem Array aktiviert oder deaktiviert wird
        for (int i = 0; i < 10; i++)
        {
            if (boote[i].Item1 == fields && boote[i].Item2 !=active) { boote[i].Item2 = active; break; }
        }
    }

    public String getBoatName(int fields)
    {
        //gibt Name des Boots wieder
        for (int i = 0; i < 10; i++)
        {
            if (boote[i].Item1 == fields) { return boote[i].Item3; }
        }
        return null;
    }

    public Boolean isBoatAvailable(int fields)
        {
        //Dursucht das Array nach einem Boot der Länge fields ob dieses aktiv ist 
        for (int i = 0; i < 10; i++)
        {
            if (boote[i].Item1 == fields && boote[i].Item2 ) { return true; }
        }
        return false;
    }

    public Boolean allBoatsPlaced()
    {
        //checkt ob alle Boote platziert worden sind
        for (int i = 0; i < 10; i++)
        {
            if (boote[i].Item2==true) { return false; }
        }
        return true;
    }

    public void entferneBoot((int, int, int) bootlaenge, int x, int y)
    {
        boatSwitchHelper(bootlaenge, true);
    }

    public void platziereBoot((int, int, int) bootlaenge, int x, int y)
    {
        boatSwitchHelper(bootlaenge, false);
    }

    private void boatSwitchHelper((int, int, int) bootlaenge, Boolean activate)
    {

        switchBoatActive(bootlaenge.Item3, activate);

        switchBoatActive(bootlaenge.Item1, !activate);
        switchBoatActive(bootlaenge.Item2, !activate);

    }


    public string boatsToText()
    {
        //zeigt in einem String na wie viele Boote noch aktiv sind
        string remainingboats = "";
        int schlachtschiffe = 0;
        int kreuzer = 0;
        int zerstoerer = 0;
        int uboot = 0;
        foreach(var boot in boote)
        {
            if (boot.Item2)
            {
                if (boot.Item3== "Schlachtschiff")
                {
                    schlachtschiffe = schlachtschiffe + 1;
                }else if (boot.Item3== "Kreuzer")
                {
                    kreuzer = kreuzer + 1;
                }else if (boot.Item3== "Zerstörer")
                {
                    zerstoerer= zerstoerer + 1;
                }else if(boot.Item3== "U-Boot")
                {
                    uboot = uboot + 1;
                }
            }
        }

        remainingboats = schlachtschiffe.ToString() + " x " + "Schlachtschiff, " + kreuzer.ToString() + " x " + "Kreuzer, " + zerstoerer.ToString() + " x " + "Zerstörer, " + uboot.ToString() + " x " + "U-Boot";

        return remainingboats;
    }



    }



