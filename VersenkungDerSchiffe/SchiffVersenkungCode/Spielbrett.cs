using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenkungDerSchiffe.SchiffVersenkungCode
{
    class Spielbrett
    {
        public int[,] brett;


        public Spielbrett() {
            brett = new int[10,10];
            for(int x = 0; x < 10; x++)
            {
                for(int y=0;y<10;y++)
                brett[x,y] = 0;
            }
        }
        public int[,] getField()
        {
            return brett;
        }

        public int getFieldInfo(int x,int y)
        {
            
            return brett[x,y];
        }

        // 1 = Boot gesetzet; 0 = leeres Feld; 2=kaputtes Schiff;
        public void setFieldInfo(int x,int y,int info)
        {
            this.brett[x,y] = info;
        }

        


    }
}
