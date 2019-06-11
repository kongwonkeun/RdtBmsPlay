using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager_sc : MonoBehaviour {

    public GameObject notePrefab; // prefab object by Inspector
    public GameObject barPrefab; // prefab object by Inspector
    public string bmsName = "Lovely_Summer"; // bms file by Inspector
    public bool isTic; // enable tic play by Inspector

    float beatPerBar = 32f; // beats per bar
    int defaultSpeed = 10;
    int timeRateBySpeed = 2; 

    GameObject note;
    NoteObj_sc note_sc;

    //IEnumerator Start () {
    void Start () {
        
        Debug.Log("----GameManager:Start()----");

        // bms file
        string[] lineData = File.ReadAllLines("Assets/Resources/BmsFiles/Lovely_Summer.bms");

        // bms file parsing
        BmsLoader_sc bmsLoader = GameObject.Find("BmsLoader").GetComponent<BmsLoader_sc>();
        bmsLoader.BmsLoad(lineData);
        Bms bms = bmsLoader.getBms();

        // get y position of start line
        GameObject plane_Top = GameObject.Find("Plane_Top");
        float startPositionY = plane_Top.transform.position.y;

        // get y position of judgment line
        GameObject lineJudgment = GameObject.Find("LineJudgment");
        float judgmentPositionY = lineJudgment.transform.position.y;

        // get center line
        GameObject lineCenter = GameObject.Find("LineCenter");

        // set y position of destroying note
        float destroyDelayPositionY = 35.04f; // distance between top line to judgment line

        // set width rate of note
        float noteWidthRate = 1.8f;

        List<NoteObj_sc> noteObj_Line_1 = new List<NoteObj_sc>();
        List<NoteObj_sc> noteObj_Line_2 = new List<NoteObj_sc>();
        List<NoteObj_sc> noteObj_Line_3 = new List<NoteObj_sc>();
        List<NoteObj_sc> noteObj_Line_4 = new List<NoteObj_sc>();
        List<NoteObj_sc> noteObj_Line_5 = new List<NoteObj_sc>();
        List<NoteObj_sc> bar_Line = new List<NoteObj_sc>();

        bool isLongNoteStart_1 = true;
        bool isLongNoteStart_2 = true;
        bool isLongNoteStart_3 = true;
        bool isLongNoteStart_4 = true;
        bool isLongNoteStart_5 = true;

        float preNoteTime_Ln1 = 0f;
        float preNoteTime_Ln2 = 0f;
        float preNoteTime_Ln3 = 0f;
        float preNoteTime_Ln4 = 0f;
        float preNoteTime_Ln5 = 0f;

        float destroyDelayTime = bms.getTotalPlayTime() + 1;
        float secondPerBar = 60.0f / (float)bms.getBpm() * 4.0f; // seconds per bar
        int barCount = 0;

        // create bars
        for (int i = 0; i < bms.totalBarCount; i++) {
            float barTime = barCount * secondPerBar;
            note = (GameObject)Instantiate(barPrefab, new Vector3(0, startPositionY, 0), Quaternion.identity);
            note_sc = note.GetComponent<NoteObj_sc>();
            note_sc.speed = defaultSpeed;
            note_sc.destroyPositionY = startPositionY - destroyDelayPositionY;
            note_sc.destroyDelayTime = destroyDelayTime;
            note_sc.noteTime = barTime;
            bar_Line.Add(note_sc);
            barCount++;
        }

        // create notes
        foreach (BarData barData in bms.getBarDataList()) {

            int channel = barData.getChannel();

            float linePositionX = lineCenter.transform.position.x;
            if      (channel == 11 || channel == 51) { linePositionX = lineCenter.transform.position.x - 2; }
            else if (channel == 12 || channel == 52) { linePositionX = lineCenter.transform.position.x - 1; }
            else if (channel == 13 || channel == 53) { linePositionX = lineCenter.transform.position.x; }
            else if (channel == 14 || channel == 54) { linePositionX = lineCenter.transform.position.x + 1; }
            else if (channel == 15 || channel == 55) { linePositionX = lineCenter.transform.position.x + 2; }

            bool isLongChannel = false;
            if (channel == 51 || channel == 52 || channel == 53 || channel == 54 || channel == 55) {
                isLongChannel = true;
            }

            foreach (Dictionary<int, float> noteData in barData.getNoteDataList()) {
                foreach (int key in noteData.Keys) {

                    // for normal notes
                    if (isLongChannel == false && key != 0 && channel != 16) {

                        float noteTime = noteData[key];

                        note = (GameObject)Instantiate(notePrefab, new Vector3(linePositionX * noteWidthRate, startPositionY, 0), Quaternion.identity);
                        note_sc = note.GetComponent<NoteObj_sc>();
                        note_sc.speed = defaultSpeed;
                        note_sc.destroyPositionY = startPositionY - destroyDelayPositionY;
                        note_sc.destroyDelayTime = destroyDelayTime;
                        note_sc.noteTime = noteTime;
                        note_sc.channel = channel;

                        if      (channel == 11) { noteObj_Line_1.Add(note_sc); }
                        else if (channel == 12) { noteObj_Line_2.Add(note_sc); }
                        else if (channel == 13) { noteObj_Line_3.Add(note_sc); }
                        else if (channel == 14) { noteObj_Line_4.Add(note_sc); }
                        else if (channel == 15) { noteObj_Line_5.Add(note_sc); }
                    }

                    // for long notes
                    if (isLongChannel == true && key != 0) {

                        float secondPerBeat = 60.0f / (float)bms.getBpm() * 4.0f / beatPerBar; // seconds per beat
                        float longHeightRate = 0f;
                        bool isLongNoteStart = false;

                        if      (channel == 51) { isLongNoteStart = isLongNoteStart_1; }
                        else if (channel == 52) { isLongNoteStart = isLongNoteStart_2; }
                        else if (channel == 53) { isLongNoteStart = isLongNoteStart_3; }
                        else if (channel == 54) { isLongNoteStart = isLongNoteStart_4; }
                        else if (channel == 55) { isLongNoteStart = isLongNoteStart_5; }

                        if (isLongNoteStart == true) {
                            if      (channel == 51) { preNoteTime_Ln1 = noteData[key]; isLongNoteStart_1 = false; }
                            else if (channel == 52) { preNoteTime_Ln2 = noteData[key]; isLongNoteStart_2 = false; }
                            else if (channel == 53) { preNoteTime_Ln3 = noteData[key]; isLongNoteStart_3 = false; }
                            else if (channel == 54) { preNoteTime_Ln4 = noteData[key]; isLongNoteStart_4 = false; }
                            else if (channel == 55) { preNoteTime_Ln5 = noteData[key]; isLongNoteStart_5 = false; }
                        }
                        else if (isLongNoteStart == false) {
                            float noteTime = noteData[key];
                            float preNoteTime_Ln = 0f;
                            if      (channel == 51) { preNoteTime_Ln = preNoteTime_Ln1; }
                            else if (channel == 52) { preNoteTime_Ln = preNoteTime_Ln2; }
                            else if (channel == 53) { preNoteTime_Ln = preNoteTime_Ln3; }
                            else if (channel == 54) { preNoteTime_Ln = preNoteTime_Ln4; }
                            else if (channel == 55) { preNoteTime_Ln = preNoteTime_Ln5; }

                            longHeightRate = (noteTime - preNoteTime_Ln) / secondPerBeat;

                            note = (GameObject)Instantiate(notePrefab, new Vector3(linePositionX * noteWidthRate, startPositionY, 0), Quaternion.identity);
                            float originalScaleX = note.transform.localScale.x;
                            float originalScaleY = note.transform.localScale.y;
                            float originalScaleZ = note.transform.localScale.z;
                            note.transform.localScale = new Vector3(originalScaleX, originalScaleY + originalScaleY * Mathf.Round(longHeightRate), originalScaleZ);
                            note_sc = note.GetComponent<NoteObj_sc>();
                            note_sc.destroyPositionY = startPositionY - destroyDelayPositionY;
                            note_sc.destroyDelayTime = destroyDelayTime;
                            note_sc.noteTime = preNoteTime_Ln;

                            if      (channel == 51) { noteObj_Line_1.Add(note_sc); preNoteTime_Ln1 = 0; isLongNoteStart_1 = true; }
                            else if (channel == 52) { noteObj_Line_2.Add(note_sc); preNoteTime_Ln2 = 0; isLongNoteStart_2 = true; }
                            else if (channel == 53) { noteObj_Line_3.Add(note_sc); preNoteTime_Ln3 = 0; isLongNoteStart_3 = true; }
                            else if (channel == 54) { noteObj_Line_4.Add(note_sc); preNoteTime_Ln4 = 0; isLongNoteStart_4 = true; }
                            else if (channel == 55) { noteObj_Line_5.Add(note_sc); preNoteTime_Ln5 = 0; isLongNoteStart_5 = true; }
                        }
                    }
                }
            }
        }
        noteObj_Line_1.Sort(delegate (NoteObj_sc a, NoteObj_sc b) { return a.noteTime.CompareTo(b.noteTime); });
        noteObj_Line_2.Sort(delegate (NoteObj_sc a, NoteObj_sc b) { return a.noteTime.CompareTo(b.noteTime); });
        noteObj_Line_3.Sort(delegate (NoteObj_sc a, NoteObj_sc b) { return a.noteTime.CompareTo(b.noteTime); });
        noteObj_Line_4.Sort(delegate (NoteObj_sc a, NoteObj_sc b) { return a.noteTime.CompareTo(b.noteTime); });
        noteObj_Line_5.Sort(delegate (NoteObj_sc a, NoteObj_sc b) { return a.noteTime.CompareTo(b.noteTime); });

        bar_Line.Sort(delegate (NoteObj_sc a, NoteObj_sc b) { return a.noteTime.CompareTo(b.noteTime); });

        Debug.Log("noteObj_Line_1 = " + noteObj_Line_1.Count);
        Debug.Log("noteObj_Line_2 = " + noteObj_Line_2.Count);
        Debug.Log("noteObj_Line_3 = " + noteObj_Line_3.Count);
        Debug.Log("noteObj_Line_4 = " + noteObj_Line_4.Count);
        Debug.Log("noteObj_Line_5 = " + noteObj_Line_5.Count);
        Debug.Log("bar_Line = " + bar_Line.Count);

        // setup for the creation of beats
        BeatCreator_sc beatCreator = GameObject.Find("BeatCreator").GetComponent<BeatCreator_sc>();
        beatCreator.noteObj_Line_1 = noteObj_Line_1;
        beatCreator.noteObj_Line_2 = noteObj_Line_2;
        beatCreator.noteObj_Line_3 = noteObj_Line_3;
        beatCreator.noteObj_Line_4 = noteObj_Line_4;
        beatCreator.noteObj_Line_5 = noteObj_Line_5;
        beatCreator.bar_Line = bar_Line;

        beatCreator.bpm = (float)bms.getBpm();
        beatCreator.beatPerBar = beatPerBar;
        beatCreator.timeRateBySpeed = timeRateBySpeed;
        //AudioClip bgm = Resources.Load("Sound/" + bmsName) as AudioClip;
        //beatCreator.bgmSound = bgm;
        beatCreator.isTic = isTic;
        //yield return new WaitForEndOfFrame(); // wait for a frame is ready
        beatCreator.isStart = true; // start beat creation
    }
	
	void Update () {
    }

}
