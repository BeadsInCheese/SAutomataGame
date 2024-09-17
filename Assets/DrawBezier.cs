using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DrawBezier : MonoBehaviour
{
    public int segments = 10;
    public LineRenderer lineRenderer;
    public Connection connection;
    public Transform debugT;
    public Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = segments+1;
    }
    bool connected = false;
    // Update is called once per frame
    public float minDistance(Vector2 P1,Vector2 P2,Vector2 P3)
    {
        var temp=P1-P3;
        float ASquared = temp.x*temp.x+temp.y*temp.y;
        temp = P2 - P3;
        float BSquared = temp.x * temp.x + temp.y * temp.y;
        temp = P1 - P2;
        float D = temp.magnitude;
        float t = (ASquared + D * D + BSquared) / (2 * D);
        if (t < 0)
        {
            return ASquared;
        }else if (t > D)
        {
            return BSquared;
        }else { 
            
            return ASquared-t*t;
        }
    }
    void Update()
    {
        if (connection.state != null)
        {
            connected = true;
        }
        else
        {
            if (connected)
            {
                Destroy(gameObject);
            }
        }
        if (startPos == null)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        lineRenderer.SetColors(Color.HSVToRGB(0.1f * connection.condition, 1f, 1f), Color.HSVToRGB(0.1f * connection.condition, 1f, 1f));
        float step = 1f / segments;
        float Cd = 100;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lastBez=Vector2.zero;
        bool swapped = false;
        for (int i = 0; i <= segments; i++)
        {

            if (connection.state != null)
            {
                var yoff=startPos.position.x> connection.state.transform.position.x?(1.0f - Mathf.Min(Mathf.Abs(startPos.position.y - connection.state.transform.position.y),1))*3f:0;
                Vector2 bez = Bezier.cubicBezierCurve(startPos.position, new Vector3(startPos.position.x + 3, startPos.position.y + yoff, startPos.position.z), new Vector3(connection.state.transform.position.x - 4, connection.state.transform.position.y + yoff, connection.state.transform.transform.position.z), new Vector3(connection.state.transform.position.x - 1, connection.state.transform.position.y, connection.state.transform.transform.position.z), i * (1.0f / segments));
                lineRenderer.SetPosition(i,bez );
                if (i>3&&!swapped) {
                    if (minDistance(bez, lastBez, mouse) < 0.2f)
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            Destroy(gameObject);
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            connection.advanceCondition();
                            swapped = true;
                        }
                        }
                }
                lastBez = bez;
                

            }
            else
            {
                var yoff = startPos.position.x > debugT.position.x ? (1.0f - Mathf.Min(Mathf.Abs(startPos.position.y - debugT.position.y), 1)) * 3f:0;
                Vector2 bez=Bezier.cubicBezierCurve(transform.position, new Vector3(startPos.position.x + 3, startPos.position.y + yoff, startPos.position.z), new Vector3(debugT.position.x - 3, debugT.position.y + yoff, debugT.position.z), debugT.position, i * (1.0f / segments));
                lineRenderer.SetPosition(i,bez );

            }
  
    }
    }
}
