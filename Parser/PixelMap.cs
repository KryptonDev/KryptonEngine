/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KryptonEngine;
using KryptonEngine.Entities;

namespace KryptonEngine.Parser
{
    public static class PixelMap
    {

        #region Methods

        /// <summary>
        /// Erzeugt eine PixelMap(byte[x,y,rgba]=0-255) aus einem Sprite.
        /// </summary>
        public static byte[, ,] GetPixelMap(Texture2D pTexture)
        {
            Color[] TmpPixelColors = new Color[pTexture.Width * pTexture.Height];
            pTexture.GetData<Color>(TmpPixelColors);
            byte[, ,] TmpPixelRGBA = new byte[pTexture.Width, pTexture.Height, 4];
            for (int i = 0; i < pTexture.Height; i++)
            {
                for (int t = 0; t < pTexture.Width; t++)
                {
                    TmpPixelRGBA[t, i, 0] = TmpPixelColors[i * pTexture.Width + t].R;
                    TmpPixelRGBA[t, i, 1] = TmpPixelColors[i * pTexture.Width + t].G;
                    TmpPixelRGBA[t, i, 2] = TmpPixelColors[i * pTexture.Width + t].B;
                    TmpPixelRGBA[t, i, 3] = TmpPixelColors[i * pTexture.Width + t].A;
                }
            }
            return TmpPixelRGBA;
        }

        /// <summary>
        /// Erzeugt eine RGBMap(string[x,y]="255,255,255") aus einem Sprite.
        /// </summary>
        public static string[,] GetRGBMap(Texture2D pTexture)
        {
            Color[] TmpPixelColors = new Color[pTexture.Width * pTexture.Height];
            pTexture.GetData<Color>(TmpPixelColors);
            string[,] TmpRGBMap = new String[pTexture.Width, pTexture.Height];
            for (int i = 0; i < pTexture.Height; i++)
            {
                for (int t = 0; t < pTexture.Width; t++)
                {
                    TmpRGBMap[t, i] = TmpPixelColors[i * pTexture.Width + t].R.ToString() + "," + TmpPixelColors[i * pTexture.Width + t].G.ToString() + "," + TmpPixelColors[i * pTexture.Width + t].B.ToString();
                }
            }
            return TmpRGBMap;
        }

        /// <summary>
        /// Erzeugt einen RGB-String("255,255,255") aus einer PixelMap(byte[x,y,rgba]=0-255) an der Position (pPosX, pPosY).
        /// </summary>
        public static string GetRGBFromPixelMap(byte[, ,] pPixelMap, int pPosX, int pPosY)
        {
            return pPixelMap[pPosX, pPosY, 0].ToString() + "," + pPixelMap[pPosX, pPosY, 1].ToString() + "," + pPixelMap[pPosX, pPosY, 2].ToString();
        }

        /// <summary>
        /// Erzeugt eine RGBMap(string[x,y]="255,255,255") aus einer PixelMap(byte[x,y,rgba]=0-255).
        /// </summary>
        public static string[,] GetRGBMapFromPixelMap(byte[, ,] pPixelMap)
        {
            string[,] TmpRGBMap = new String[pPixelMap.GetLength(0),pPixelMap.GetLength(1)];
            for (int i = 0; i < TmpRGBMap.GetLength(0); i++)
            {
                for (int t = 0; t < TmpRGBMap.GetLength(1); t++)
                {
                    TmpRGBMap[i,t] = pPixelMap[i, t, 0].ToString() + "," + pPixelMap[i, t, 1].ToString() + "," + pPixelMap[i, t, 2].ToString();
                }

            }
            return TmpRGBMap;
        }

        #endregion
    }
}
