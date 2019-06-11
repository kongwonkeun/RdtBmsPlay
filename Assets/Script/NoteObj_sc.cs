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

    void Start () {
        Debug.Log("----NodeObj:Start()----");
    }

    void OnTriggerEnter(Collider col) {
        if (col.transform.tag == "LineJudgment") {
            gameObject.GetComponent<Renderer>().material.color = Color.green;

            Transform effect = (Transform)Instantiate(Resources.Load("Particle/Effect1", typeof(Transform)), new Vector3(transform.position.x, -29f, -1), Quaternion.identity);
            particle = (ParticleSystem)effect.GetComponent(typeof(ParticleSystem));
            particle.Play();
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
