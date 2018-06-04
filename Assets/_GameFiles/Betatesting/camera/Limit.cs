using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public enum LimitBehaviour
    {
        hard,
        soft,
        occultHard
    }

    public enum LimitType
    {
        verticalR,
        verticalL,
        horizontalU,
        horizontalD,
        depth
    }

    public class Limit : MonoBehaviour
    {
        public LimitBehaviour behaviour;
        public LimitType type;

        public bool isColliding(Vector3 center, float width, float heigth)
        {
            switch(type)
            {
                case LimitType.verticalR:
                    if (center.x - width / 2 <= transform.position.y)
                        return true;
                    else return false;
                case LimitType.verticalL:
                    if (center.x + width / 2 >= transform.position.y)
                        return true;
                    else return false;
                case LimitType.horizontalU:
                    if (center.y + heigth / 2 >= transform.position.x)
                        return true;
                    else return false;
                case LimitType.horizontalD:
                    if (center.y - heigth / 2 <= transform.position.x)
                        return true;
                    else return false;
                case LimitType.depth:
                    if (center.z < transform.z)
                        return true;
                    else return false;
                default:
                    break;
            }
            return false;
        }
    }
}