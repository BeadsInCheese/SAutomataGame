using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSolutionTester : MonoBehaviour
{
    // Start is called before the first frame update
   public Automata playerAutomata;
   public Automata solutionAutomata;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void validate()
    {
        int n = 10;

        var solutions = playerAutomata.generatePermutations(new List<int>(), n);
        solutions.Add(new List<int>());
        foreach (List<int> perm in solutions)
        {
            if (playerAutomata.run(perm) == solutionAutomata.run(perm))
            {
                string print = "";
                foreach (var item in perm)
                {
                    print += item;
                }
               // Debug.Log(print + " succeeds");
            }
            else
            {
                Debug.Log("Automata failed");
                StartCoroutine(playerAutomata.runSlowly(perm));
                return;
            }
        }
    }
}
