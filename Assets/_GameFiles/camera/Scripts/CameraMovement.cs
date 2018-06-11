using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class CameraMovement : MonoBehaviour
    {
		public GameObject target;
		public bool DebugMode;
        Limit[] limites;
		bool[] limitedHard, limitedSoft;
		public float[] speeds;
		[Range (0f, 1f)]
		public float offsetSpeed;
		Vector3[] DirVec;
		Vector3 desiredTarget;
		Vector3 offset;
		public float width; 
		float height, radius;

        private void Start()
        {
            limites = FindObjectsOfType<Limit>();	//Todos los objetos limite de la escena
			limitedHard = new bool[4];				//Lo que va a guardar un bool que dice true si la camara esta saliendo de un limite tipo hard
			limitedSoft = new bool[4];				//lo mismo pero para limites tipo soft
			DirVec = new Vector3[4];				//Vector para utilizar menos lineas a la hora de mover la camara
			DirVec [0] = Vector3.up;				
			DirVec [1] = Vector3.right;				//Todos los arreglos de tamaño 4 (no solo DirVec) estan ordenados como se ordenarian en cuanquier codigo de layout (xml, html etc.): [up, right, left, down]]
			DirVec [2] = Vector3.down;
			DirVec [3] = Vector3.left;
			height = (width / 16) * 9;
			radius = height * 0.675f;
        }

        private void Update()
        {
            CheckBounds();
			SetDesiredTarget ();
			Move ();
			if (DebugMode)
				DrawLines ();
        }

        void CheckBounds()
        {
			for (int i = 0; i < 4; i++) {		//Limpio los arreglos para volverlos a llenar
				limitedHard [i] = false;
				limitedSoft [i] = true;
			}
			for (int i = 0; i < limites.Length; i++) {		//i es para recorrer todos los limites, j sera para reccorrer los arreglos de direcciones (de tamaño 4)
				if (limites [i].type != LimitType.highlight) {	
					bool[] inOrOut = limites [i].checkBounds (transform.position.x, transform.position.y, width, height); //Pregunta si el cuadro de la camara esta saliendo de los cuadros de limite y regresa yn arreglo tamaño 4, diciendo en que direccion se esta saliendo ([up, right, down, left])
					//Apecto 16:9

					switch (limites [i].type) {
					case LimitType.hard:					//Los limites tipo hard siempre detienen la camara cuando esta intenta salir de ellos, puede haber varios en escena, pero la camara solo podra andar en la interseccion de los limites
						for (int j = 0; j < 4; j++) {
							if (inOrOut [j])
								limitedHard [j] = true;
						}
						break;
					case LimitType.soft:					//De estos puede haber varios en la escena, la camara se podra mover en la union de los limites
						for (int j = 0; j < 4; j++) {		
							if (!inOrOut [j])
								limitedSoft [j] = false;
						}
						break;
					default:
						break;
					}
				}
			}

			for (int i = 0; i < 4; i++) {
				if (limitedSoft [i])
					Debug.Log ("LimitedSoft[" + i + "] is true");
			}
        }   

		private void SetDesiredTarget(){
			desiredTarget = Vector3.zero;
			bool beingOffseted = false;
			//Falta ajustar el dt con respecto a la direccion a la que esta mirando el jugador
			for (int i = 0; i < limites.Length; i++) {		//i es para recorrer todos los limites, j sera para reccorrer los arreglos de direcciones (de tamaño 4)
				if (limites [i].type == LimitType.highlight) {
					Vector3 temp = limites [i].checkProximity (target.transform.position.x, target.transform.position.y, radius);
					if (temp != Vector3.zero) {
						offset += temp * offsetSpeed * Time.deltaTime;
						beingOffseted = true;
					}
				}
			}
			if (!beingOffseted)
				offset -= offset.normalized * offsetSpeed * Time.deltaTime * radius;
				

			if (offset.magnitude <= 0.01f)
				offset = Vector3.zero;

			if (offset.magnitude > radius*0.6f) {
				offset = offset.normalized * radius*0.6f;
			}

			desiredTarget = target.transform.position + offset;
		}

		private void Move(){
			for (int i = 0; i < 4; i++) {
				if (!limitedHard [i] && !limitedSoft[i]) {
					Vector3 moving = Vector3.Scale ((desiredTarget - transform.position), DirVec [i]);
					if (moving [(i+1)%2] > 0) {
						transform.position += Vector3.Scale ((desiredTarget- transform.position), DirVec [i % 2]) * speeds [i] * Time.deltaTime;
					}
				}
			}
		}

        private void FixedUpdate()
        {
            
        }

		public void DrawLines()
		{

			Vector3 upRight = new Vector3(transform.position.x + width/2, transform.position.y + height/2, 0);
			Vector3 upLeft = new Vector3(transform.position.x - width/2, transform.position.y + height/2, 0);
			Vector3 downRight = new Vector3(transform.position.x + width/2, transform.position.y - height/2, 0);
			Vector3 downLeft = new Vector3(transform.position.x - width/2, transform.position.y - height/2, 0);

			Color color;

			color = Color.black;

			Debug.DrawLine(upRight, upLeft, color, Time.deltaTime, false);
			Debug.DrawLine(upLeft, downLeft, color, Time.deltaTime, false);
			Debug.DrawLine(upRight, downRight, color, Time.deltaTime, false);
			Debug.DrawLine(downLeft, downRight, color, Time.deltaTime, false);


			for (int i = 0; i < 16; i++) {
				float angle = i * (360 / 16);
				float nextAngle = (i + 1) * (360 / 16);

				Vector3 point1 = new Vector3 (
					target.transform.position.x + radius * Mathf.Cos (angle * Mathf.Deg2Rad), 
					target.transform.position.y + radius * Mathf.Sin (angle * Mathf.Deg2Rad), 
				    0f
				);
				Vector3 point2 = new Vector3 (
					target.transform.position.x + radius * Mathf.Cos (nextAngle * Mathf.Deg2Rad), 
					target.transform.position.y + radius * Mathf.Sin (nextAngle * Mathf.Deg2Rad),
				    0f
				);

				Debug.DrawLine (point1, point2, color, Time.deltaTime, false);
			}
		}
    }
}

