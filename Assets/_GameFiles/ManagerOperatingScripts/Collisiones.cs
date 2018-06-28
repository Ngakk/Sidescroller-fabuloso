using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos{
	public class Collisiones : MonoBehaviour {

		public static bool Point_v_Rect(float x1, float y1, float x2, float y2, float w2, float h2){
			if(x1 >= x2 && x1 <= x2 + w2 && y1 <= y2 && y1 >= y2-h2)
				return true;
			else
				return false;
		}

		public static bool Rect_v_Rect(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2)
		{                                                                                                                                                                                 ///-----
			if( (Point_v_Rect(x1, y1, x2, y2, w2, h2)
				|| Point_v_Rect(x1+w1, y1, x2, y2, w2, h2)
				|| Point_v_Rect(x1+w1, y1-h1, x2, y2, w2, h2)
				|| Point_v_Rect(x1, y1-h1, x2, y2, w2, h2))
				|| ( Point_v_Rect(x2, y2, x1, y1, w1, h1)
				|| Point_v_Rect(x2+w2, y2, x1, y1, w1, h1)
				|| Point_v_Rect(x2+w2, y2-h2, x1, y1, w1, h1)
				|| Point_v_Rect(x2, y2-h2, x1, y1, w1, h1) )
				|| ( Point_v_Rect(x1+w1/2, y1-h1/2, x2, y2, w2, h2)
				|| Point_v_Rect(x2+w2/2, y2-h2/2, x1, y1, w1, h1)))
				return true;
			else
				return false;
		}

		public static bool Circle_v_Circle(float x1, float y1, float r1, float x2, float y2, float r2)
		{
			if (Vector2.Distance (new Vector2 (x1, y1), new Vector2 (x2, y2)) <= r1 + r2) {
				return true;
			}
			else
				return false;
		}
	}
}
