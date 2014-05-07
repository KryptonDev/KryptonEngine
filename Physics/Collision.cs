/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine;
using Microsoft.Xna.Framework;

namespace KryptonEngine.Physics
{
    public class Collision
    {
        #region Methods
        
        public static Vector2 CollisionCheckedVector(Rectangle pBody ,int pDeltaX, int pDeltaY, List<Rectangle> pBodiesToCheck)
        {
            Vector2 TmpTotalMove = new Vector2(pDeltaX, pDeltaY);
            Rectangle TmpCollisionBody;
            Vector2 TmpMove;
            int TmpStep;
            bool TmpSlide;
            do
            { //An Bodies entlang sliden möglich machen.
                TmpSlide = false;
                TmpCollisionBody = pBody;
                TmpMove = Vector2.Zero;
                //Größere Koordinate als Iteration nehmen
                if (Math.Abs(TmpTotalMove.X) > Math.Abs(TmpTotalMove.Y))
                {
                    TmpStep = (int)Math.Abs(TmpTotalMove.X);
                }
                else
                {
                    TmpStep = (int)Math.Abs(TmpTotalMove.Y);
                }
                //Iteration
                for (int i = 1; i <= TmpStep; i++)
                {
                    //Box für nächsten Iterationsschritt berechnen
                    TmpCollisionBody.X = (int)(pBody.X + ((TmpTotalMove.X / TmpStep) * i));
                    TmpCollisionBody.Y = (int)(pBody.Y + ((TmpTotalMove.Y / TmpStep) * i));
                    if (CollisionCheck(TmpCollisionBody, pBodiesToCheck)) //Bei Kollision: Kollisionsabfrage mit letztem kollisionsfreien Zustand beenden
                    {
                        if (i == 1) //Testen ob Sliden möglich ist
                        {
                            if (!CollisionCheck(new Rectangle(pBody.X, TmpCollisionBody.Y, TmpCollisionBody.Width, TmpCollisionBody.Height), pBodiesToCheck))
                            { //Vertikales Sliden
                                TmpTotalMove.X = 0;
                                TmpSlide = true;
                            }
                            else if (!CollisionCheck(new Rectangle(TmpCollisionBody.X, pBody.Y, TmpCollisionBody.Width, TmpCollisionBody.Height), pBodiesToCheck))
                            { //Horizontales Sliden
                                TmpTotalMove.Y = 0;
                                TmpSlide = true;
                            }
                            if (TmpTotalMove.X == 0 && TmpTotalMove.Y == 0)
                                TmpSlide = false; //Endlosschleife verhindern
                        }
                        break;
                    }
                    else //Kollisionsfreien Fortschritt speichern
                    {
                        TmpMove.X = TmpCollisionBody.X - pBody.X;
                        TmpMove.Y = TmpCollisionBody.Y - pBody.Y;
                    }
                }
            } while (TmpSlide);
            return TmpMove;
        }

        protected static bool CollisionCheck(Rectangle pBodyToCheck, List<Rectangle> pBodiesToCheck)
        {
            bool TmpCollision = false;
            //Gehe die Blöcke der Liste durch
            foreach (Rectangle TmpBodyToCheck in pBodiesToCheck)
            {
                //Wenn Kollision vorliegt: Keinen weiteren Block abfragen
                if (pBodyToCheck.Intersects(TmpBodyToCheck))
                {
                    TmpCollision = true;
                    break;
                }
            }
            return TmpCollision;
        }

        #endregion
    }
}
