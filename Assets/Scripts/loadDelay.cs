using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10000000; i++)
        {
            var tempObject = new GameObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
