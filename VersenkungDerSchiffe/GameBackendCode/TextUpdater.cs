
using VersenkungDerSchiffe.WindowManager;

class TextUpdater
{
    FensterManager wmanager;
    public TextUpdater(FensterManager wmanager)
    {
        this.wmanager = wmanager;

    }

    public void updateAnweisung(Boolean bootesetzten)
    {
        if (bootesetzten)
        {
            wmanager.updateTextBlock("Setze deine Boote!", "Anweisung");
        }
        else
        {
            wmanager.updateTextBlock("Klicke Feld zum abschießen!", "Anweisung");
        }

    }

    public void updateSpielertext(Boolean spielzug)
    {
        if (spielzug)
        {
            wmanager.updateTextBlock("SP-1", "Spieler");
        }
        else
        {
            wmanager.updateTextBlock("SP-2", "Spieler");
        }
        wmanager.updateTextBlock("", "letzteAktion");
        wmanager.updateTextBlock("", "Boote");
    }

    public void refreshBootCounterText(string remaining)
    {
        wmanager.updateTextBlock(remaining + " übrig", "Boote");
    }

    public void updateletzteAktionPlacement(int x,int y,int state)
    {
        string cords = xyToBrettCords(x, y);
        switch (state)
        {
            case 0:
                wmanager.updateTextBlock("Kreuz auf " + cords + " entfernt", "letzteAktion");
                break;
            case 1:
                wmanager.updateTextBlock("Kreuz auf " + cords + " gesetzt", "letzteAktion");
                break;
            case 2:
                wmanager.updateTextBlock("Aktion auf " + cords + " ungültig", "letzteAktion");
                break;
        }
    }

    public void updateletzteAktionSpielzug(int x,int y,int state)
    {
        string cords = xyToBrettCords(x, y);
        switch (state)
        {
            case 0:
                wmanager.updateTextBlock("Schuss " + cords + " getroffen", "letzteAktion");
                break;
            case 1:
                wmanager.updateTextBlock("Schuss " + xyToBrettCords(x, y) + " daneben", "letzteAktion");
                break;
            case 2:
                wmanager.updateTextBlock("Kein Schuss mehr übrig!", "Anweisung");
                break;
        }

    }


    public string xyToBrettCords(int x, int y)
    {
        return (x + 1).ToString() + "-" + ((char)(y + 65)).ToString();
    }

    public void updateswitchSichtbaresButtonContent(Boolean ownBrett)
    {
        if (ownBrett)
        {
            wmanager.updateButtonContent("Eigenes Brett wird angezeigt", "switchSichtbaresBrett");
        }
        else
        {
            wmanager.updateButtonContent("Schussbrett wird angezigt", "switchSichtbaresBrett");
        }

    }

    public void maxBootLaengeErreicht(String bootname)
    {
        wmanager.updateTextBlock("Kein " + bootname + " mehr platzierbar! ", "Anweisung");
    }

    public void updateAnweisungBootFinished(int state)
    {
        switch (state)
        {
            case 0:
                wmanager.updateTextBlock("Platziere alle Boote!", "Anweisung");
                break ;
                case 1:
                wmanager.updateTextBlock("Entferne alle alleinestehenden Kreuze", "Anweisung");
                break ;
        }
    }


}
    


    

