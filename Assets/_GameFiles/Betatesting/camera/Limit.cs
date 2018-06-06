using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Limit : MonoBehaviour
    {
        public LimitType type;
        public bool DebugMode;
        public float width, height;
        public float[] padding = new float[4];

        private void Start()
        {
            if (DebugMode)
                DrawLines();
        }

        public bool[] checkBounds(float x, float y, float w, float h)
        {
            bool[] b = new bool[4];
            if (x + w / 2f >= transform.position.x + width)
                b[(int)Dir.right] = true;
            if (x - w / 2f <= transform.position.x)
                b[(int)Dir.left] = true;
            if (y - h / 2f <= transform.position.y)
                b[(int)Dir.up] = true;
            if (y + h / 2f >= transform.position.y + height)
                b[(int)Dir.down] = true;

            return b;
        }

        public void DrawLines()
        {
            Vector3 upRight = new Vector3(transform.position.x + width, transform.position.y, 0);
            Vector3 upLeft = new Vector3(transform.position.x, transform.position.y, 0);
            Vector3 downRight = new Vector3(transform.position.x + width, transform.position.y + height, 0);
            Vector3 downLeft = new Vector3(transform.position.x, transform.position.y + height, 0);

            Color color;

            switch(type)
            {
                case LimitType.hard:
                    color = Color.red;
                    break;
                case LimitType.highlight:
                    color = Color.magenta;
                    break;
                case LimitType.screen:
                    color = Color.blue;
                    break;
                default:
                    color = Color.black;
                    break;
            }

            Debug.DrawLine(upRight, upLeft, color, 360f, false);
            Debug.DrawLine(upLeft, downLeft, color, 360f, false);
            Debug.DrawLine(upRight, downRight, color, 360f, false);
            Debug.DrawLine(downLeft, downRight, color, 360f, false);
        }
    }
}
