using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatCreator_sc : MonoBehaviour {

    public AudioClip ticSound; // set by Inspector
    public AudioClip bgmSound; // set by GameManager

    public int keyMode; // reserved
    public float bpm; // set by GameManager
    public bool isStart = false; // set by GameManager
    public bool isTic = false; // set by GameManager
    public float beatPerBar; // set by GameManager
    public float timeRateBySpeed; // set by GameManager

    public List<NoteObj_sc> noteObj_Line_1; // set by GameManager
    public List<NoteObj_sc> noteObj_Line_2; // set by GameManager
    public List<NoteObj_sc> noteObj_Line_3; // set by GameManager
    public List<NoteObj_sc> noteObj_Line_4; // set by GameManager
    public List<NoteObj_sc> noteObj_Line_5; // set by GameManager
    public List<NoteObj_sc> bar_Line; // set by GameManager

    public AudioSource bgmPlayer; // set by Inspector
    AudioSource ticPlayer;

    float nextTime = 0f;
    float nextSample = 0f;
    float secondPerBar = 0f;
    float secondPerBeat = 0f;
    float samplePerBar = 0f;
    float samplePerBeat = 0f;

    int beatCount = 0;

    int noteIndex_1 = 0;
    int noteIndex_2 = 0;
    int noteIndex_3 = 0;
    int noteIndex_4 = 0;
    int noteIndex_5 = 0;
    int barIndex = 0;

    bool isBgmPlay = false;

    void Start () {
        Debug.Log("----BeatCreator:Start()----");
        ticPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        //ticPlayer.clip = ticSound;
        //bgmPlayer.clip = bgmSound;
        ticPlayer.clip = Resources.Load("Sound/tic") as AudioClip;
        bgmPlayer.clip = Resources.Load("Sound/Lovely_Summer") as AudioClip;
        secondPerBar = 60.0f / bpm * 4f;
        secondPerBeat = 60.0f / bpm * 4f / beatPerBar;
        samplePerBar = secondPerBar * bgmPlayer.clip.frequency;
        samplePerBeat = secondPerBeat * bgmPlayer.clip.frequency;
    }

    void Update() {
        if (isStart == true) {
            StartCoroutine( Create() );
        }
    }

    IEnumerator Create() {

        yield return null;

        if (Time.time >= (secondPerBar * (timeRateBySpeed - 1)) && isBgmPlay == false) {
            bgmPlayer.Play();
            isBgmPlay = true;
        }

        if (Time.time >= nextTime && isBgmPlay == false) {
            if (noteObj_Line_1.Count > noteIndex_1) {
                if (nextTime >= (noteObj_Line_1[noteIndex_1].noteTime)) {
                    noteObj_Line_1[noteIndex_1].isStart = true;
                    noteObj_Line_1[noteIndex_1].channel = 1;
                    noteIndex_1++;
                }
            }
            if (noteObj_Line_2.Count > noteIndex_2) {
                if (nextTime >= (noteObj_Line_2[noteIndex_2].noteTime)) {
                    noteObj_Line_2[noteIndex_2].isStart = true;
                    noteObj_Line_2[noteIndex_2].channel = 2;
                    noteIndex_2++;
                }
            }
            if (noteObj_Line_3.Count > noteIndex_3) {
                if (nextTime >= (noteObj_Line_3[noteIndex_3].noteTime)) {
                    noteObj_Line_3[noteIndex_3].isStart = true;
                    noteObj_Line_3[noteIndex_3].channel = 3;
                    noteIndex_3++;
                }
            }
            if (noteObj_Line_4.Count > noteIndex_4) {
                if (nextTime >= (noteObj_Line_4[noteIndex_4].noteTime)) {
                    noteObj_Line_4[noteIndex_4].isStart = true;
                    noteObj_Line_4[noteIndex_4].channel = 4;
                    noteIndex_4++;
                }
            }
            if (noteObj_Line_5.Count > noteIndex_5) {
                if (nextTime >= (noteObj_Line_5[noteIndex_5].noteTime)) {
                    noteObj_Line_5[noteIndex_5].isStart = true;
                    noteObj_Line_5[noteIndex_5].channel = 5;
                    noteIndex_5++;
                }
            }
            if (bar_Line.Count > barIndex) {
                if (nextTime >= bar_Line[barIndex].noteTime) {
                    bar_Line[barIndex].isStart = true;
                    barIndex++;
                }
            }
            nextTime += secondPerBeat;
        }

        if (bgmPlayer.timeSamples >= nextSample && isBgmPlay == true) {
            if (noteObj_Line_1.Count > noteIndex_1) {
                if (bgmPlayer.timeSamples >= (noteObj_Line_1[noteIndex_1].noteTime - (secondPerBar * (timeRateBySpeed -1)))  * bgmPlayer.clip.frequency) {
                    noteObj_Line_1[noteIndex_1].isStart = true;
                    noteObj_Line_1[noteIndex_1].channel = 1;
                    noteIndex_1++;
                }  
            }
            if (noteObj_Line_2.Count > noteIndex_2) {
                if (bgmPlayer.timeSamples >= (noteObj_Line_2[noteIndex_2].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency) {
                    noteObj_Line_2[noteIndex_2].isStart = true;
                    noteObj_Line_2[noteIndex_2].channel = 2;
                    noteIndex_2++;
                } 
            }
            if (noteObj_Line_3.Count > noteIndex_3) {
                if (bgmPlayer.timeSamples >= (noteObj_Line_3[noteIndex_3].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency) {
                    noteObj_Line_3[noteIndex_3].isStart = true;
                    noteObj_Line_3[noteIndex_3].channel = 3;
                    noteIndex_3++;
                }
            }
            if (noteObj_Line_4.Count > noteIndex_4) {
                if (bgmPlayer.timeSamples >= (noteObj_Line_4[noteIndex_4].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency) {
                    noteObj_Line_4[noteIndex_4].isStart = true;
                    noteObj_Line_4[noteIndex_4].channel = 4;
                    noteIndex_4++;
                }
            }
            if (noteObj_Line_5.Count > noteIndex_5) {
                if (bgmPlayer.timeSamples >= (noteObj_Line_5[noteIndex_5].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency) {
                    noteObj_Line_5[noteIndex_5].isStart = true;
                    noteObj_Line_5[noteIndex_5].channel = 5;
                    noteIndex_5++;
                }
            }
            if (bar_Line.Count > barIndex) {
                if (bgmPlayer.timeSamples >= (bar_Line[barIndex].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency) {
                    bar_Line[barIndex].isStart = true;
                    barIndex++;
                }
            }
            if (beatCount % (4 * beatPerBar/16) == 0) {
                if (isTic) {
                    ticPlayer.Play();
                }
            }
            nextSample += samplePerBeat;
            beatCount++;
        }
    }

}
