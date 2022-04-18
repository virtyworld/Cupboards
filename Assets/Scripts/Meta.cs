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
    private List<Chip> chips = new List<Chip>();
    private List<Point> listPoints = new List<Point>();
    private Action<string> findEmptyPointsAction;

    void Start()
    {
        gameData = Instantiate(dataManagerPrefab);
        gameData.Load();
      

        wayManager = Instantiate(wayManagerPrefab);
        PrefabInitialize();
        wayManager.Setup(listPoints);

        findEmptyPointsAction += FindEmptyPoints;
    }

    private void PrefabInitialize()
    {
        for (int i = 0; i < pointsArrayPrefabs.Length; i++)
        {
            Point point = Instantiate(pointsArrayPrefabs[i],listSpawnPoints[i].transform);
            listPoints.Add(point);
        }

        for (int i = 0; i < chipPrefabs.Length; i++)
        {
            Chip chip = Instantiate(chipPrefabs[i],listSpawnPoints[i].transform);
            chip.Setup(findEmptyPointsAction);
            chips.Add(chip);
        }
       
    }

    private void FindEmptyPoints(string text)
    {
        if (text =="Point1") wayManager.FindEmptyWayPoint(Points.Point1);
        if (text =="Point2") wayManager.FindEmptyWayPoint(Points.Point2);
        if (text =="Point3") wayManager.FindEmptyWayPoint(Points.Point3);
        if (text =="Point4") wayManager.FindEmptyWayPoint(Points.Point4);
        if (text =="Point5") wayManager.FindEmptyWayPoint(Points.Point5);
        if (text =="Point6") wayManager.FindEmptyWayPoint(Points.Point6);
    }
}
