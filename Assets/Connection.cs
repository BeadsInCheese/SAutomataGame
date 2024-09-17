using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection:MonoBehaviour
{
    // Start is called before the first frame update
    public int condition;
    public State state;
    bool selecting = false;
    public Connection(State state,int condition) {  this.state = state;this.condition = condition; }
    public void advanceCondition()
    {
        condition = (condition + 1) % GameObject.Find("LevelInfo").GetComponent<LevelInfo>().alfabetlargestNumber;
    }
}
