using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public GameObject connectionPrefab;
    // Update is called once per frame
    DrawBezier r;
    GameObject connectionSource;
    GameObject selected;
    public Camera cam;
    void Update()
    {

        var screenpos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        bool mousedown = Input.GetMouseButton(0);
        cam.orthographicSize = Mathf.Min(Mathf.Max((cam.orthographicSize-  Input.mouseScrollDelta.y),2f),30);
        if ((Input.GetMouseButton(2)&&(Input.mousePosition-new Vector3(cam.pixelWidth,cam.scaledPixelHeight,0)/2).magnitude > 300))
        {
            var temp = cam.transform.position;
            temp=cam.transform.position+cam.orthographicSize*(Input.mousePosition - new Vector3(cam.pixelWidth, cam.scaledPixelHeight, 0) / 2).normalized*Time.deltaTime ;
            temp.z = -10;
            cam.transform.position = temp;
        }
        if (Input.GetMouseButtonUp(0))
        {
            selected = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name.Equals("Output"))
                {
                    if (r != null)
                    {
                        var con = r.transform.gameObject.GetComponent<Connection>();
                        Debug.Log(hit.collider.gameObject.name);
                        connectionSource.transform.parent.gameObject.GetComponent<State>().connections.Add(con);
                        con.state = hit.collider.transform.parent.gameObject.GetComponent<State>();
                    }
                }
                else
                {
                    if (r != null)
                    {
                        Destroy(r.gameObject);
                    }
                }

            }
            else
            {
                if (r != null)
                { Destroy(r.gameObject); } }
            r = null;

        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag.Equals("State"))
                {
                    selected = hit.collider.gameObject;
                }
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name.Equals("Input")) {
                    var node = hit.collider.gameObject;
                    connectionSource = node;
                    var connection = Instantiate(connectionPrefab).GetComponent<Connection>();
                    connection.condition = 1;
                    r = connection.GetComponent<DrawBezier>();
                    r.startPos = node.transform;
                    connection.transform.position = node.transform.position;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag.Equals("State"))
                {
                    Destroy(hit.collider.gameObject);
                }
    
            }
        }
        if (mousedown)
        {

            if (r != null)
            {
                r.transform.GetChild(0).transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (selected != null)
            {
                selected.transform.position=new Vector3(screenpos.x,screenpos.y,0);
                
            }
        }

    }
}
