using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class State : MonoBehaviour
{
    // Start is called before the first frame update
    public void toggleFinal()
    {

        finalState = !finalState;
        model.transform.Rotate(0, 0, 45);
        
    }
    
    public List<Connection> connections = new List<Connection>();
    public bool active=false;
    public bool finalState = false;
    public bool triggered = false;
    public bool source = false;
    public bool wasRun=false;
    public bool setAfterRun = false;
    public GameObject model;

    int l = 0;
    public void evaluate(int letter, bool visual)
    {
        l=letter;
        if (!active || triggered)
        {

            return;
        }
        active = false;
        for (int connection = 0; connection < connections.Count; connection++)
        {
            if (connections[connection] == null)
            {
                connections.RemoveAt(connection);
                connection--;
                continue;
            }
            if (connections[connection].state == null)
            {
                Destroy(connections[connection].gameObject);
                break;
            }
            if (connections[connection].condition == letter)
            {
                if (!connections[connection].state.active)
                {
                    connections[connection].state.triggered = true;
                    connections[connection].state.active = true;
                }
                else
                {
                    
                        connections[connection].state.setAfterRun = true;
                    
                }
            }
        }
        if (visual)
        {
            StartCoroutine(visualizeActivation());
        }
        wasRun = true;
        if (setAfterRun)
        {
            active = true;
            setAfterRun= false;
        }
    }
    IEnumerator visualizeActivation()
    {
        float time = 0;
        Vector3 original = model.transform.localScale;
        while (time < 0.2)
        {


            model.transform.localScale = original * Mathf.Lerp(Mathf.Lerp(1,1.5f,time * 5),1.5f,time*5);
            time += Time.deltaTime;
            yield return 0;
        }
        time = 0;
        while (time < 0.2)
        {


            model.transform.localScale = original * Mathf.Lerp(1.5f,Mathf.Lerp(1.5f, 1, time*5) , time*5);
            time += Time.deltaTime;
            yield return 0;
        }
        model.transform.localScale = original;

    }
    public bool accept()
    {
        var temp = active;
        active = source;
        return (finalState && temp);

    }
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (active)
        {
            this.model.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", UnityEngine.Color.HSVToRGB(0.1f * l, 1f, 50 * 1f));
        }
        else {

            this.model.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", UnityEngine.Color.grey);
        }
    }
}
