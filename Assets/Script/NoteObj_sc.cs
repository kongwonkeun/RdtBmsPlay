using UnityEngine;
using System.Collections;

public class NoteObj_sc : MonoBehaviour {

    public int speed;
    public bool isStart = false;
    public int channel;
    public float noteTime;
    public float destroyPositionY;
    public float destroyDelayTime;

    ParticleSystem particle = null;
    GameObject cubeObj;
    Cube_sc cubeScr;
    bool isSetup = false;

    void Start () {
        Debug.Log("----NodeObj:Start()----");
    }

    void OnTriggerEnter(Collider col) {
        if (col.transform.tag == "LineJudgment") {
            Debug.Log("----NodeObj:TiggerChannel=" + channel + "----");
            switch (channel) {
                case 1: cubeObj = GameObject.Find("Cube_1"); break;
                case 2: cubeObj = GameObject.Find("Cube_2"); break;
                case 3: cubeObj = GameObject.Find("Cube_3"); break;
                case 4: cubeObj = GameObject.Find("Cube_4"); break;
                case 5: cubeObj = GameObject.Find("Cube_5"); break;
            }
            cubeScr = (Cube_sc)cubeObj.GetComponent(typeof(Cube_sc));

            gameObject.GetComponent<Renderer>().material.color = Color.green;
            if (cubeScr.selected) {
                Transform effect = (Transform)Instantiate(Resources.Load("Particle/Effect1", typeof(Transform)), new Vector3(transform.position.x, -28.5f, -1), Quaternion.identity);
                particle = (ParticleSystem)effect.GetComponent(typeof(ParticleSystem));
                particle.Play();
            }
        }
    }

	void Update () {
        if (isStart == true) {
            StartCoroutine( move() );
        }
    }

    IEnumerator move() {
        if (transform.position.y > destroyPositionY) {
            transform.Translate(Vector3.down * speed * Time.smoothDeltaTime);
        } else {
            Destroy(gameObject);
        }
        yield return null;
    }

    public void setPosition(float x, float y, float z) {
        transform.position = new Vector3(x, y, z);
    }

}
