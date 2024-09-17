using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier 
{
    public static Vector3 cubicbezierCurve(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4,float t) { 
        Vector3 L0=(1-t)*p1 + t*p2;
        Vector3 L1=(1-t)*p2 + t*p3;
        Vector3 L2=(1-t)*p3 + t*p4;
        Vector3 Q1 = (1 - t) * L0 + t * L1;
        Vector3 Q2 = (1 - t)*L1 + t * L2;
        return (1-t)*Q1 + t*Q2;
    
    }
    public static Vector2 cubicBezierCurve(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4,float t)
    {
        Vector2 L0 = (1 - t) * p1 + t * p2;
        Vector2 L1 = (1 - t) * p2 + t * p3;
        Vector2 L2 = (1 - t) * p3 + t * p4;
        Vector2 Q1 = (1 - t) * L0 + t * L1;
        Vector2 Q2 = (1 - t) * L1 + t * L2;
        return (1 - t) * Q1 + t * Q2;

    }
}
