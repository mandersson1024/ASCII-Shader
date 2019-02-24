using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kalaskod
{
    public class Tileset
    {
        readonly Dictionary<char, Texture2D> tiles = new Dictionary<char, Texture2D>();
        Sprite[] sprites;

        public Tileset(string spritesheetName)
        {
            sprites = Resources.LoadAll<Sprite>(spritesheetName);
            /* 
             * todo: load json file that contains names and other data. Possibly create several ways of accessing them.
             * 
             *  {
             *      [
             *          {
             *              "index": 0,
             *              "symbol": "#",
             *              "color": "#ff00ff"
             *              "luminosity": 0.8,
             *          },
             *          {
             *              ...
             *          }
             *      ]
             *  }
             */
        }

        void AddTile(char chr, Texture2D tex)
        {
            tiles[chr] = tex;
        }

        public Texture2D GetTile(char chr)
        {
            return tiles[chr];
        }

        public Sprite GetTile(int index)
        {
            return sprites[index];
        }

        public int Length
        {
            get { return sprites.Length; }
        }
    }
}
