using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using KryptonEngine.Pools;
using Microsoft.Xna.Framework.Graphics;

namespace KryptonEngine.Entities
{
    public class ParallaxSpinePlane : ParallaxPlane<SpineObject>
    {

        #region Methods

        /// <summary>
        /// Füllt die ParallaxPlane mit Sprites anhand einer TileMap(RGBMap).
        /// </summary>
        /// <param name="pRGBMap">TileMap für die Ebene</param>
        /// <param name="pInterpreter">Zuweisung: string RGB -> string TextureName</param>
        /// <param name="pTileSize">Maßstab der TileMap</param>
        public void Generate(string[,] pRGBMap, Dictionary<String, SpinePool> pInterpreter, int pTileSize)
        {
            mSize = new Vector2(pRGBMap.GetLength(0) * pTileSize, pRGBMap.GetLength(1) * pTileSize);
            for (int x = 0; x < pRGBMap.GetLength(0); x++)
            {
                for (int y = 0; y < pRGBMap.GetLength(1); y++)
                {
                    if (pInterpreter.ContainsKey(pRGBMap[x,y])) //Zuweisung für diesen Farbwert vorhanden?
                    {
                        SpineObject TmpSpineObject = pInterpreter[pRGBMap[x,y]].GetObject(); //SpineObject von zugewiesenem Pool holen
                        TmpSpineObject.Load();
                        TmpSpineObject.PositionX = x * pTileSize;
                        TmpSpineObject.PositionY = y * pTileSize;
                        mTiles.Add(TmpSpineObject); //SpineObject zur ParallaxPlane hinzufügen
                    }
                }
            }
        }

        public override void Update(CameraGame pCamera)
        {
            base.Update(pCamera);
            foreach (SpineObject TmpSpineObject in mTiles)
                TmpSpineObject.Update();
        }

        public void Draw(SpriteBatch pSpriteBatch, CameraGame pCamera)
        {
            foreach (SpineObject TmpSpineObject in mTiles)
                TmpSpineObject.Draw(pSpriteBatch, pCamera, Position);
        }

        #endregion
    }
}
