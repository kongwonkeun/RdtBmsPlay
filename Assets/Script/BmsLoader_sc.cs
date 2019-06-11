using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class BmsLoader_sc : MonoBehaviour {

    public string bmsFileName;
    public bool isFinishLoad;
    public Bms bms;

    public Bms getBms() {
        Debug.Log("----BmsLoader:getBms()----");
        return bms;
    }

    public void BmsLoad(string[] lineData) {

        Debug.Log("----BmsLoader:BmsLoad()----");
        bms = gameObject.AddComponent<Bms>();
        BarData barData;

        foreach (string line in lineData) {
            if (line.StartsWith("#")) {
                string[] data = line.Split(' ');
                if (data[0].IndexOf(":") == -1 && data.Length == 1) { continue; }
                if (data[0].Equals("#TITLE")) { bms.setTitle(data[1]); }
                else if (data[0].Equals("#ARTIST")) { bms.setArtist(data[1]); }
                else if (data[0].Equals("#BPM")) { bms.setBpm(double.Parse(data[1])); }
                else if (data[0].Equals("#PLAYER")) { }
                else if (data[0].Equals("#GENRE")) { }
                else if (data[0].Equals("#PLAYLEVEL")) { }
                else if (data[0].Equals("#RANK")) { }
                else if (data[0].Equals("#TOTAL")) { }
                else if (data[0].Equals("#VOLWAV")) { }
                else if (data[0].Equals("#MIDIFILE")) { }
                else if (data[0].Substring(0,4).Equals("#WAV")) { }
                else if (data[0].Equals("#BMP")) { }
                else if (data[0].Equals("#STAGEFILE")) { }
                else if (data[0].Equals("#VIDEOFILE")) { }
                else if (data[0].Equals("#BGA")) { }
                else if (data[0].Equals("#STOP")) { }
                else if (data[0].Equals("#LNTYPE")) { bms.setLnType(int.Parse(data[1])); }
                else if (data[0].Equals("#LNOBJ")) { }
                else if (data[0].IndexOf(":") != -1) {
                    int bar = 0;
                    int channel = 0;
                    Int32.TryParse(data[0].Trim().Substring(1, 3), out bar);
                    Int32.TryParse(data[0].Trim().Substring(4, 2), out channel);
                    string noteStr = data[0].Trim().Substring(7);
                    List<Dictionary<int, float>> noteData = getNoteDataOfStr(noteStr, bar, bms.getBpm()); // create note data
                    barData = gameObject.AddComponent<BarData>();
                    barData.setBar(bar);
                    barData.setChannel(channel);
                    barData.setNoteDataList(noteData);
                    bms.addBarData(barData);
                }
            }
        }
        if (bms.getBarDataList().Count != 0) {
            isFinishLoad = true;
        }
    }

    private List<Dictionary<int, float>> getNoteDataOfStr(string str, int bar, double bpm) {

        string tempStr = str.Trim();
        List<Dictionary<int, float>> noteDataList = new List<Dictionary<int, float>>();

        float barCount = (float)bar;
        float totalBeatOfBar = 0;
        if (tempStr.Length != 0) {
            totalBeatOfBar = tempStr.Length / 2; // note size is 2
        }
        float secondPerBar = 60.0f / (float)bpm * 4.0f; // seconds per bar
        float preSecond = barCount * secondPerBar; // sum of previous bar time
        float beatCount = 0;

        while (true) {
            int key = 0;
            float time = 0;
            Int32.TryParse(tempStr.Substring(0, 2), out key);
            if (key != 0) {
                time = preSecond + (secondPerBar / totalBeatOfBar * beatCount);
                bms.setTotalPlayTime(time);
            } else {
                time = 0;
            }
            Dictionary<int, float> noteData = new Dictionary<int, float>();
            noteData.Add(key, time);
            noteDataList.Add(noteData);
            bms.totalBarCount = bar;
            if (tempStr.Length > 2) {
                tempStr = tempStr.Substring(2);
            } else {
                break;
            }
            beatCount++;
        }
        foreach (Dictionary <int, float> noteData in noteDataList) {
            foreach (int key in noteData.Values) {
                if (key != 0) {
                    bms.sumTotalNoteCount();
                }
            }
        }
        return noteDataList;
    }

}