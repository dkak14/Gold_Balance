using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOPortrait", menuName = "SO/Portrait", order = 1)]
public class SOPortrait : ScriptableObject {
    public List<PortraitData> portraitDatas;
}
[System.Serializable]
public struct PortraitData {
    public Sprite portrait;
    public int id;
}
