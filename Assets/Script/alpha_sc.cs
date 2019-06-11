using UnityEngine;
using System.Collections;

public class alpha_sc : MonoBehaviour {

	void Start () {
        Debug.Log("----alpha:Start()----");
        Color color = GetComponent<Renderer>().material.color;
        color.a = 0.5f;
        GetComponent<Renderer>().material.SetColor("_Color", color );
    }
	
	void Update () {
	}

}
