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
    TextUpdater textUpdater;
    Boolean spielzug; //True Spieler 1 | False Spieler 2
    Boolean bootesetzten; //Treue Boote platzieren | False normalspielen
    Boolean ownBrett;
    

    public void setFenstermanager(FensterManager wmanager)
    {
        this.wmanager = wmanager;
    }

    public void intializeSpiel()
    {
        
        textUpdater = new TextUpdater(wmanager);
        intializeSpieler();
    }

    public void intializeSpieler()
    {
        spieler1 = new Spieler(wmanager,textUpdater);
        spieler2 = new Spieler(wmanager, textUpdater); 
    }

    public void initialzeVars()
    {    
        spielzug = true;
        bootesetzten = true;
        ownBrett = false;
        intializeSpieler();
    }


    public void switchPlayers()
    {
        spielzug = !spielzug;
        if (spielzug)
        {
            spieler1.spielzuguebrig = true;
        }
        else
        {
            spieler2.spielzuguebrig= true;           
        }
        textUpdater.updateSpielertext(spielzug);
        if (!bootesetzten)
        {
            ownBrett = false;
            textUpdater.updateswitchSichtbaresButtonContent(ownBrett);
        }
        wmanager.gameWindow.addSichtschutz();
        
    }

    // Dafür verantwortlich das richtige Array anzuzeigen
    public void finishButtonPressed()
    {
        textUpdater.updateAnweisung(bootesetzten);
        switch ((bootesetzten, spielzug))
        {
            case (true, true):
              //  if (true) {  
                    if (spieler1.kreuzhelper.alleKreuzeGültig()) {
                    switchPlayers();
                    wmanager.gameWindow.refreshButtons(spieler2.eigenbrett.getField());
                }
                break;
            case (true, false):
               // if (true) { 
                     if (spieler2.kreuzhelper.alleKreuzeGültig()) {
                    switchVonBootesetzenZuSpielzug();
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

    public void switchVonBootesetzenZuSpielzug()
    {
        bootesetzten = false;
        wmanager.gameWindow.addswitchSichtbaresButton();
        textUpdater.updateswitchSichtbaresButtonContent(false);
    }




    

    public void switchSichtbaresButtonPressed()
    {
        

        switch ((spielzug,ownBrett))
        {
            case (true, true):
                ownBrett = false;
                spieler1.refreshSchussbrett();
                break;
            case (true, false):
                ownBrett = true;
                spieler1.refreshEigenbrett();
                break;
            case (false, true):
                ownBrett = false;
                spieler2.refreshSchussbrett();
                break;
            case (false, false):
                ownBrett = true;
                spieler2.refreshEigenbrett();
                break;
        }

        textUpdater.updateswitchSichtbaresButtonContent(ownBrett);
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
                    if (playerWonGegen(spieler2))
                    {
                        MessageBox.Show("Spieler 1 hat gewonnen!");
                        wmanager.switchEndWindow();
                    }

                    break;
                case (false, false):
                    spielerszug(x, y, spieler2, spieler1);
                    if (playerWonGegen(spieler1))
                    {
                        MessageBox.Show("Spieler 2 hat gewonnen!");
                        wmanager.switchEndWindow();
                    }
                    break;
            }
        }

    }


    public void spielerszug(int x, int y, Spieler eigen, Spieler gegner)
    {
        Boolean hit;
        hit = schiffGetroffen(x, y, gegner);
        eigen.spielzug(x, y, hit);
        if (hit&&eigen.spielzuguebrig) { gegner.eigenbrett.setFieldInfo(x, y, 2); 
        }
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
        wmanager.switchEndWindow();
    }







}

