using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Level")]
public class Levels : ScriptableObject
{
    public Automata solution;
    public string levelName;
    public string levelDescription;
    public int alfabetlargestNumber = 2;
}
