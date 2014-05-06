using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using KryptonEngine.Pools;
using Microsoft.Xna.Framework.Graphics;

namespace KryptonEngine.Entities
{
    public class ParallaxSpritePlane : ParallaxPlane<Sprite>
    {

        #region Methods

        /// <summary>
        /// Füllt die ParallaxPlane mit Sprites anhand einer TileMap(RGBMap).
        /// </summary>
        /// <param name="pRGBMap">TileMap für die Ebene</param>
        /// <param name="pInterpreter">Zuweisung: string RGB -> string TextureName</param>
        /// <param name="pTileSize">Maßstab der TileMap</param>
        public void Generate(string[,] pRGBMap, Dictionary<String, String> pInterpreter, int pTileSize)
        {
            mSize = new Vector2(pRGBMap.GetLength(0) * pTileSize, pRGBMap.GetLength(1) * pTileSize);
            for (int x = 0; x < pRGBMap.GetLength(0); x++)
            {
                for (int y = 0; y < pRGBMap.GetLength(1); y++)
                {
                    if (pInterpreter.ContainsKey(pRGBMap[x,y])) //Zuweisung für diesen Farbwert vorhanden?
                    {
                        Sprite TmpSprite = SpritePool.Instance.GetObject(); //Sprite vom Pool holen
                        TmpSprite.TextureName = pInterpreter[pRGBMap[x,y]]; //Sprite entsprechend Zuweisung definieren
                        TmpSprite.PositionX = x * pTileSize;
                        TmpSprite.PositionY = y * pTileSize;
                        mTiles.Add(TmpSprite); //Sprite zur ParallaxPlane hinzufügen
                    }
                }
            }
        }

      
        public void Draw(SpriteBatch pSpriteBatch)
        {
          foreach (Sprite TmpSprite in mTiles)
            TmpSprite.Draw(pSpriteBatch); //, Position); //Sprite.Draw braucht Überladung mit Offset um gezeichnet werden zu können.
        }                                 // Draw befehl wird mit einerm Spritebatch.Begin(...) aufgerufen welches eine Camera translations Matritze besitzt weswegen kein offset benötigt wird. 


        #endregion

    }
}
