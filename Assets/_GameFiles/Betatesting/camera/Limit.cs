using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Limit : MonoBehaviour
    {
        public LimitType type;
        public bool DebugMode;
		Color color;
        public float width, height;
        public float[] padding = new float[4];

        private void Start()
        {
			SetDebugColor ();
			if (type == LimitType.highlight)
				height = width;
        }

		void SetDebugColor(){
			switch(type)
			{
			case LimitType.hard:
				color = Color.red;
				break;
			case LimitType.highlight:
				color = Color.white;
				break;
			case LimitType.screen:
				color = Color.blue;
				break;
			case LimitType.soft:
				color = Color.magenta;
				break;
			default:
				color = Color.black;
				break;
			}
		}

		void Update(){
			if (DebugMode)
				DrawLines();
		}
								//Pide el x, y, w, y h de la camara
		public bool[] checkBounds(float x, float y, float w, float h) //Funcion que regresa true si se esta saliendo por algun lado
        {
			bool[] b = new bool[4];
			switch (type) {
			case LimitType.hard:
				for (int i = 0; i < 4; i++)
					b [i] = false;
				if (x + w / 2f >= transform.position.x + width)
					b[(int)Dir.right] = true;
				if (x - w / 2f <= transform.position.x)
					b[(int)Dir.left] = true;
				if (y + h / 2f >= transform.position.y)
					b [(int)Dir.up] = true;
				if (y - h / 2f <= transform.position.y - height)
					b [(int)Dir.down] = true;
				break;
			case LimitType.soft:
				if (Collisiones.Rect_v_Rect (x - w / 2f, y + h / 2f, w, h, transform.position.x, transform.position.y, width, height)) {
					color = Color.green;
					for (int i = 0; i < 4; i++)
						b [i] = false;
					if (x + w / 2f >= transform.position.x + width)
						b[(int)Dir.right] = true;
					if (x - w / 2f <= transform.position.x)
						b[(int)Dir.left] = true;
					if (y + h / 2f >= transform.position.y)
						b [(int)Dir.up] = true;
					if (y - h / 2f <= transform.position.y - height)
						b [(int)Dir.down] = true;
				} else {
					SetDebugColor ();
					for (int i = 0; i < 4; i++)
						b [i] = true;
				}
				break;
			default:
				break;
			}
				

            return b;
        }

		public Vector3 checkProximity(float x, float y, float radius){
			if (Collisiones.Circle_v_Circle (x, y, radius, transform.position.x, transform.position.y, width)) {
				return new Vector3 (transform.position.x - x,transform.position.y - y, 0f);
			} else
				return Vector3.zero;
		}

        public void DrawLines()
        {
			if (type != LimitType.highlight) {
				Vector3 upRight = new Vector3 (transform.position.x + width, transform.position.y, 0);
				Vector3 upLeft = new Vector3 (transform.position.x, transform.position.y, 0);
				Vector3 downRight = new Vector3 (transform.position.x + width, transform.position.y - height, 0);
				Vector3 downLeft = new Vector3 (transform.position.x, transform.position.y - height, 0);

				Debug.DrawLine (upRight, upLeft, color, Time.deltaTime, false);
				Debug.DrawLine (upLeft, downLeft, color, Time.deltaTime, false);
				Debug.DrawLine (upRight, downRight, color, Time.deltaTime, false);
				Debug.DrawLine (downLeft, downRight, color, Time.deltaTime, false);
			} else {
				for (int i = 0; i < 16; i++) {
					float angle = i * (360 / 16);
					float nextAngle = (i + 1) * (360 / 16);

					Vector3 point1 = new Vector3 (
						transform.position.x + width * Mathf.Cos (angle * Mathf.Deg2Rad), 
						transform.position.y + width * Mathf.Sin (angle * Mathf.Deg2Rad), 
						0f
					);
					Vector3 point2 = new Vector3 (
						transform.position.x + width * Mathf.Cos (nextAngle * Mathf.Deg2Rad), 
						transform.position.y + width * Mathf.Sin (nextAngle * Mathf.Deg2Rad),
						0f
					);
						
					Debug.DrawLine (point1, point2, color, Time.deltaTime, false);
				}
			}
        }
    }
	


}
