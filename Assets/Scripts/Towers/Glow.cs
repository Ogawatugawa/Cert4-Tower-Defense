using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    public Material mat;
	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mat.color.a <= 0.4)
        {
            if (mat.color.a >= 0.2)
            {
                //mat.color.a += 0.1f * Time.deltaTime;
            }
        }

        if (mat.color.a >= 0.4)
        {
            //mat.color.a -= 0.1f * Time.deltaTime;
        }
	}
}
