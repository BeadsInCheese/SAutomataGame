using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamps : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stampPrefab;
    public Transform stampTransform;
    public void createStamps( List<int> colors) {
        for (int i = 0; i < colors.Count; i++)
        {
            var x = Instantiate(stampPrefab);
            x.transform.position = stampTransform.position+new Vector3(0,i*0.3f,0);
            x.transform.rotation = stampTransform.rotation;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
