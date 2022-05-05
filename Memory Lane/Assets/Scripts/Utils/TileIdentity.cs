using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class TileIdentity
    {
        public Color Color { get; set; }
        public Texture Symbol { get; set; }

        public TileIdentity(Color color, Texture symbol)
        {
            Color = color;
            Symbol = symbol;
        }
    }
}
