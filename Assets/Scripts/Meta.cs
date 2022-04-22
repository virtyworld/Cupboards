using System;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [SerializeField] private Point[] pointsArrayPrefabs;
    [SerializeField] private Chip[] chipPrefabs;
    [SerializeField] private Transform[] listSpawnPoints;
    [SerializeField] private WayManager wayManagerPrefab;
    [SerializeField] private DataManager dataManagerPrefab;
    
    private DataManager gameData;
    private WayManager wayManager;
    // private Action<string> findEmptyPointsAction;
    // private Action stopBlinkingPointsAction;

    void Start()
    {
        // findEmptyPointsAction += FindEmptyPoints;
        //stopBlinkingPointsAction += StopBlinkPoints;
        
        gameData = Instantiate(dataManagerPrefab);
        gameData.Load();
      
        wayManager = Instantiate(wayManagerPrefab);
        
        // PrefabInitialize();

        wayManager.Setup(pointsArrayPrefabs,chipPrefabs,listSpawnPoints,gameData.Data);

       
    }
  

    // private void FindEmptyPoints(string text)
    // {
    //     if (text =="Point1") wayManager.FindEmptyWayPoint(Points.Point1);
    //     if (text =="Point2") wayManager.FindEmptyWayPoint(Points.Point2);
    //     if (text =="Point3") wayManager.FindEmptyWayPoint(Points.Point3);
    //     if (text =="Point4") wayManager.FindEmptyWayPoint(Points.Point4);
    //     if (text =="Point5") wayManager.FindEmptyWayPoint(Points.Point5);
    //     if (text =="Point6") wayManager.FindEmptyWayPoint(Points.Point6);
    //     if (text =="Point7") wayManager.FindEmptyWayPoint(Points.Point7);
    //     if (text =="Point8") wayManager.FindEmptyWayPoint(Points.Point8);
    //     if (text =="Point9") wayManager.FindEmptyWayPoint(Points.Point9);
    // }

    // private void StopBlinkPoints()
    // {
    //     Debug.Log("meta stopBlinkingPointsAction?.Invoke()");
    //     wayManager.StopBlink();
    // }
}
