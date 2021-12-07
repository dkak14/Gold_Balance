using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : SingletonBehaviour<UnitManager>
{
    Dictionary<string, Dictionary<int, UnitControllerBase>> spawnUnitDic;
    int newID = 0;
    public override void Awake() {
        base.Awake();
        EventManager.Instance.SpawnUnit += SensingSpawnUnit;
        EventManager.Instance.DieUnit += SensingSpawnUnit;

        spawnUnitDic = new Dictionary<string, Dictionary<int, UnitControllerBase>>();
    }
    void SensingSpawnUnit(UnitControllerBase unit) {
        Debug.Log(unit.unitDataID + " ���� ���� ��û");
        if (!spawnUnitDic.ContainsKey(unit.unitDataID)) {
            spawnUnitDic.Add(unit.unitDataID, new Dictionary<int, UnitControllerBase>());
        }
        if (!spawnUnitDic[unit.unitDataID].ContainsKey(unit.unitFieldID)) {
            spawnUnitDic[unit.unitDataID].Add(unit.unitFieldID, unit);
            newID++;
            Debug.Log(unit.unitDataID + " ���� ���� ����");
        }
    }
    void SensingSpawnUnit(UnitControllerBase unit, int id) {
        if (spawnUnitDic.ContainsKey(unit.unitDataID)) {
            if (spawnUnitDic[unit.unitDataID].ContainsKey(unit.unitFieldID)) {
                spawnUnitDic[unit.unitDataID].Remove(id);
            }
        }
    }
    public UnitControllerBase GetUnit(string dataID) {
        if (spawnUnitDic.ContainsKey(dataID)) {
            Debug.Log(dataID + " ���� ��û");
            IEnumerator<UnitControllerBase> en = spawnUnitDic[dataID].Values.GetEnumerator();
            en.MoveNext();
            Debug.Log(en.Current);
            return en.Current;
        }
        Debug.Log(dataID + " ���� ��û ����");
        return null;
    }
}
