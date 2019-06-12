using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_sc : MonoBehaviour
{
    public bool selected = false;
    Renderer render;

    void Start() {
        render = gameObject.GetComponent<Renderer>();
    }

    void Update() {
        if (selected) {
            render.material.color = Color.green;
        } else {
            render.material.color = Color.white;
        }
    }
}
