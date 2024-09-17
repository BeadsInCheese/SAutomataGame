using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class Automata : MonoBehaviour
{
    // Start is called before the first frame update
    public List<State> states= new List<State>();
    public GameObject statePrefab;
    public Automata solution;
    void Start()
    {
        
    }
    public void addState()
    {
        var state=Instantiate(statePrefab).GetComponent<State>();
        states.Add(state);
        state.transform.parent = transform;

    }
    public LevelInfo levelInfo;
    public List<List<int>> generatePermutations(List<int> n,int num)
    {

        List<List<int>> words = new List<List<int>>();
        int alfabet = levelInfo.alfabetlargestNumber;

        if (num == 0) {
            return words;
        }
        else {
            for (int i=0; i<alfabet; i++){
                List<int> temp = new List<int>(n); ;
                temp.Add(i);


                words.AddRange( generatePermutations(temp,num - 1));


                words.Add(temp);
                    
                    }
         }
        return words;

    }
    public void TestPrint(List<List<int>> l)
    {
        foreach (var perm in l) {
            string print = "";
            foreach (var item in perm)
            {
                print += item;
            }
            Debug.Log(print);
            
        }
    }
    public void TestSolution() {
        int n = 10;

        var solutions = generatePermutations(new List<int>(), n);
        solutions.Add(new List<int>());
        foreach (List<int> perm in solutions) {
            if (run(perm))
            {
                string print = "";
                foreach (var item in perm)
                {
                    print += item;
                }
                Debug.Log(print+" succeeds");
            }
        }
        StartCoroutine(runSlowly(new List<int> { 1, 1, 1 }));
    }
    public bool run(List<int> word)
    {
        foreach(int letter in word)
        {
            foreach (State state in states)
            {
                state.triggered = false;
            }
            foreach (State state in states)
            {
                state.evaluate(letter,false);
                
            }
        }
        bool accepted = false;
        foreach (State state in states)
        {

            if (state.accept())
            {
                accepted = true;
            }
        }
        return accepted;
    }
    IEnumerator runSlowly(List<int> word) {
        foreach (int letter in word)
        {
            foreach (State state in states)
            {
                state.triggered = false;
                state.wasRun = false;
            }
            foreach (State state in states)
            {
                state.evaluate(letter,true);
                
            }
            yield return new WaitForSeconds(0.5f); 
        }
        bool accepted = false;
        foreach (State state in states)
        {
            if (state.accept())
            {
                accepted = true;
            }
        }
        Debug.Log(accepted);
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i] == null)
            {
                states.RemoveAt(i);
                i--;
            }
        }
    }
}
