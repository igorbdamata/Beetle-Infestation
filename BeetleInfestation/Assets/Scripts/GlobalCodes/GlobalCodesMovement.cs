using UnityEngine;

namespace GC
{
    public static class d
    {
        public static float GetDistance(Vector2 a, Vector2 b)
        {
            //Usa a fórmula V(ax-bx)² + (ay-by)²
            float distanceX = a.x - b.x;
            float distanceY = a.y - b.y;
            float distanceT = (distanceX * distanceX) + (distanceY * distanceY);
            distanceT = Mathf.Sqrt(distanceT);//Calcula a raiz quadrada
            return distanceT;
        }
        public static Vector2 VectorDistNoAbs(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
    }
}