using System.Printing;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Controls;
using VersenkungDerSchiffe.WindowManager;

class GameManager
    {
    //theroretsich muss Spieler ´private sein
    public Spieler spieler1;
    public Spieler spieler2;
    FensterManager wmanager;
    Boolean spielzug; //True Spieler 1 | False Spieler 2
    Boolean bootesetzten; //Treue Boote platzieren | False normalspielen
    Boolean ownBrett;
    
    public GameManager() {
        spielzug = true;
        bootesetzten = true;
        ownBrett = false;
    }

    public void setFenstermanager(FensterManager wmanager)
    {
        this.wmanager = wmanager;
    }

    public void intializeSpieler()
    {
        spieler1 = new Spieler(wmanager);
        spieler2 = new Spieler(wmanager); 
    }


    public void switchPlayers()
    {
        spielzug = !spielzug;
        if (spielzug)
        {
            wmanager.updateTextBlock("SP-1", "Spieler");
            spieler1.spielzuguebrig = true;
        }
        else
        {
            wmanager.updateTextBlock("SP-2", "Spieler");
            spieler2.spielzuguebrig= true;
            
        }
    }

    // Dafür verantwortlich das richtige Array anzuzeigen
    public void finishButtonPressed()
    {
        switch ((bootesetzten, spielzug))
        {
            case (true, true):
                if (/*spieler1.kreuzhelper.alleKreuzeGültig()*/true) {
                    switchPlayers();
                    wmanager.gameWindow.refreshButtons(spieler2.eigenbrett.getField());
                }
                break;
            case (true, false):
                if (/*spieler2.kreuzhelper.alleKreuzeGültig()*/true)
                {
                    bootesetzten = false;
                    switchPlayers();
                    wmanager.gameWindow.refreshButtons(spieler1.schussbrett.getField());
                }
                    break;
                
            case (false, true):
                switchPlayers();
                wmanager.gameWindow.refreshButtons(spieler2.schussbrett.getField());
                break;
            case (false, false):
                switchPlayers();
                wmanager.gameWindow.refreshButtons(spieler1.schussbrett.getField());
                break;
        }

    }

    public void showOwnBrettButtonPressed(Button button)
    {
        

        switch ((spielzug,ownBrett))
        {
            case (true, true):
                ownBrett = false;
                spieler1.refreshSchussbrett();
                button.Content = "Eigenbrett anzeigen!";
                break;
            case (true, false):
                ownBrett = true;
                spieler1.refreshEigenbrett();
                button.Content = "Schussbrett anzeigen!";
                break;
            case (false, true):
                ownBrett = false;
                spieler2.refreshSchussbrett();
                button.Content = "Eigenbrett anzeigen!";
                break;
            case (false, false):
                ownBrett = true;
                spieler2.refreshEigenbrett();
                button.Content = "Schussbrett anzeigen!";
                break;
        }
    }

    //navigiert den Button Press zur richtigen Methode
    public void buttonPressed(int x, int y)
    {
        if (ownBrett == false)
        {
            Boolean hit;
            switch ((bootesetzten, spielzug))
            {
                case (true, true):
                    spieler1.setzeBoot(x, y);
                    break;
                case (true, false):
                    spieler2.setzeBoot(x, y);
                    break;
                case (false, true):
                    spielerszug(x, y, spieler1, spieler2);
                    break;
                case (false, false):
                    spielerszug(x, y, spieler2, spieler1);
                    break;
            }
        }

    }

    public void spielerszug(int x, int y,Spieler eigen,Spieler gegner)
    {
        Boolean hit;
        hit = schiffGetroffen(x, y, gegner);
        eigen.spielzug(x, y, hit);
        if (hit) { gegner.eigenbrett.setFieldInfo(x, y, 2); }
        if (playerWonGegen(gegner)) { MessageBox.Show("Spieler hat gewonnen!"); }
    }

    public Boolean schiffGetroffen(int x ,int y,Spieler gegner)
    {
        if (gegner.eigenbrett.getFieldInfo(x, y)==1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    //sagt wenn alle Boote zerstört sind wird treue zurückgegeben
    public Boolean playerWonGegen(Spieler spieler)
    {
        for(int x = 0; x < 10; x++)
        {
            for(int y = 0; y < 10; y++)
            {
                if (spieler.eigenbrett.getFieldInfo(x,y) == 1)
                {
                    return false;
                }
            }
        }    
        return true;
    }

    public void endGame()
    {
        MessageBox.Show("Spiel wird nun beendet");
    }







}

