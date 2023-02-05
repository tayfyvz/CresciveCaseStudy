using System;
using UnityEngine;

namespace _GameFiles.Scripts.Utilities
{
    public static class AngleUtility
    {
        public static float GetAngleOfLineBetweenTwoPoints(Vector2 p1, Vector3 p2)
        {
            float xDiff = p2.x - p1.x;
            float yDiff = p2.y - p1.y;
            return (float)(Math.Atan2(yDiff, xDiff) * (180 / Math.PI));
        }
       
    }
}