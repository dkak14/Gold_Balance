using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
[CustomEditor(typeof(SODialogData), true), CanEditMultipleObjects]
public class SODialogDataEditor : Editor
{
    SODialogData soDialogData;
    GUIContent dialogUpdateContent;
    Queue<List<DialogData>> threadDialogData = new Queue<List<DialogData>>();
    Queue<List<CharacterData>> threadCharcterData = new Queue<List<CharacterData>>();
    private void OnEnable() {
        soDialogData = (SODialogData)target;
        dialogUpdateContent = new GUIContent("대사 업데이트");
    }
    public override void OnInspectorGUI() {
        if (GUILayout.Button(dialogUpdateContent)) {
            Undo.RecordObject(soDialogData, "GetData");
            MyGetData();
        }

        base.OnInspectorGUI();
        if(threadDialogData.Count > 0) {
            soDialogData.dialogData = threadDialogData.Dequeue();
            soDialogData.DataUpdate();
        }
        if(threadCharcterData.Count > 0) {
            soDialogData.characterDatas = threadCharcterData.Dequeue();
            soDialogData.DataUpdate();
        }
        serializedObject.ApplyModifiedProperties();
    }
    void MyGetData() {
        Thread thread = new Thread(new ThreadStart(DataThread));
        thread.Start();
    }
    void DataThread() {
        Debug.Log("시트 데이터 받아오는 중");
        string url = @"https://docs.google.com/spreadsheets/d/1H66O9KmH1oSseOOYMEjTvuUi9ayygQtm/export?format=csv";
        WebClient wc = new WebClient();
        wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
        wc.Headers.Add("DNT", "1");
        wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        wc.Headers.Add("Accept-Encoding", "deflate");
        wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");

        var data = wc.DownloadString(url);
        Debug.Log(data);
        string[] row = data.Split('\n');
        List<DialogData> dialogData = new List<DialogData>();
        for (int i = 2; i < row.Length; i++) {
            string[] column = row[i].Split(',');
            int dialogID = int.Parse(column[0]);
            int charID = int.Parse(column[1]);
            int imageID;
            imageID = column[2] != "" ? int.Parse(column[2]) : -1;
            string dialog = column[3];
            dialogData.Add(new DialogData(dialogID, charID, imageID, dialog));
        }
        threadDialogData.Enqueue(dialogData);
        url = @"https://docs.google.com/spreadsheets/d/1H66O9KmH1oSseOOYMEjTvuUi9ayygQtm/export?format=csv&gid=786931313";
        wc = new WebClient();
        wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
        wc.Headers.Add("DNT", "1");
        wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        wc.Headers.Add("Accept-Encoding", "deflate");
        wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");

        data = wc.DownloadString(url);
        Debug.Log(data);
        row = data.Split('\n');
        List<CharacterData> characterDatas = new List<CharacterData>();
        for (int i = 2; i < row.Length; i++) {
            string[] column = row[i].Split(',');
            string name = column[0];
            int id = int.Parse(column[1]);
            characterDatas.Add(new CharacterData(id, name));
        }
        threadCharcterData.Enqueue(characterDatas);
    }
}
