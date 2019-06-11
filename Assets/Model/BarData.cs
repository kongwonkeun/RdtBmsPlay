using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarData : MonoBehaviour {

    public int bar; // bar number
    public int channel; // channel number
    public List<Dictionary<int, float>> noteDataList; // list of note data

    void Awake() {
        Debug.Log("----BarData:Awake()----");
        bar = 0;
        channel = 0;
        noteDataList = new List<Dictionary<int, float>>();
    }

    public int  getBar() { return bar; }
    public void setBar(int bar) { this.bar = bar; }
    public int  getChannel() { return channel; }
    public void setChannel(int channel) { this.channel = channel; }
    public List<Dictionary<int, float>> getNoteDataList() { return noteDataList; }
    public void setNoteDataList(List<Dictionary<int, float>> noteDataList) { this.noteDataList = noteDataList; }

    public void debug() {
        Debug.Log("bar = " + bar);
        Debug.Log("channel = " + channel);
        foreach (Dictionary<int, float> noteData in noteDataList) {
            foreach (int key in noteData.Keys) {
                if (key != 0) {
                    Debug.Log("note key = " + key + ", time = " + noteData[key]);
                }
            }
        }
    }

}