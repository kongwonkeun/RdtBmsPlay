using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bms : MonoBehaviour {

    public string title;
    public string artist;
    public double bpm;
    public List<BarData> barDataList;
    public int lnType;

    public int totalBarCount;
    public int totalNoteCount;
    public float totalPlayTime;

    void Awake() {
        Debug.Log("----Bms:Awake()----");
        title = "";
        artist = "";
        bpm = 0;
        barDataList = new List<BarData>();
        totalNoteCount = 0;
        totalPlayTime = 0;
        lnType = 0;
    }

    public string getTitle() { return title; }
    public void   setTitle(string title) { this.title = title; }
    public string getArtist() { return artist; }
    public void   setArtist(string artist) { this.artist = artist; }
    public List<BarData> getBarDataList() { return barDataList; }
    public void   setBarDataList(List<BarData> barDataList) { this.barDataList = barDataList; }
    public double getBpm() { return bpm; }
    public void   setBpm(double bpm) { this.bpm = bpm; }
    public int    getTotalNoteCount() { return totalNoteCount; }
    public void   setTotalNoteCount(int totalCount) { this.totalNoteCount = totalCount; }
    public int    getLnType() { return lnType; }
    public void   setLnType(int lnType) { this.lnType = lnType; }
    public float  getTotalPlayTime() { return totalPlayTime; }
    public void   setTotalPlayTime(float totalPlayTime) { this.totalPlayTime = totalPlayTime; }
    public void   addBarData(BarData bar) { barDataList.Add(bar); }
    public void   sumTotalNoteCount() { this.totalNoteCount++; }

    public void debug() {
        Debug.Log("title = " + title);
        Debug.Log("artist = " + artist);
        Debug.Log("bpm = " + bpm);
        Debug.Log("long note type = " + lnType);
        Debug.Log("total bar Count = " + barDataList.Count);
        Debug.Log("total note Count = " + totalNoteCount);
        Debug.Log("total play time = " + totalPlayTime);
    }

}