using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    Vector2 rotation = new Vector2 (0, 0);
	public float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      	rotation.y += Input.GetAxis ("Mouse X");
		rotation.x += -Input.GetAxis ("Mouse Y");
		transform.eulerAngles = (Vector2)rotation * speed;    
    }
}
