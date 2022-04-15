using System;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    // [SerializeField] private Point point1Prefab,
    //     point2Prefab,
    //     point3Prefab,
    //     point4Prefab,
    //     point5Prefab,
    //     point6Prefab,
    //     point7Prefab,
    //     point8Prefab;

    [SerializeField] private Point[] pointsArrayPrefabs;
    [SerializeField] private Chip[] chipPrefabs;
    [SerializeField] private WayManager wayManagerPrefab;
    [SerializeField] private DataManager dataManagerPrefab;
    
    private DataManager gameData;
    private WayManager wayManager;
    private List<Point> points;
    private List<Chip> chips;

    private Action<string> findEmptyPointsAction;

    void Start()
    {
        gameData = Instantiate(dataManagerPrefab);
        gameData.Load();
        Debug.Log(gameData.Data.numberOfChip);

        wayManager = Instantiate(wayManagerPrefab);
        wayManager.Setup();

        findEmptyPointsAction += FindEmptyPoints;
    }

    private void PrefabInitialize()
    {
        foreach (Point pointPrefab in pointsArrayPrefabs)
        {
            Point point = Instantiate(pointPrefab);
            points.Add(point);
        }
        
        foreach (Chip chipPrefab in chipPrefabs)
        {
            Chip chip = Instantiate(chipPrefab);
            chips.Add(chip);
            chip.Setup(findEmptyPointsAction);
        }
    }

    private void FindEmptyPoints(string text)
    {
        if (text =="Point1")
        {
            wayManager.FindEmptyWayPoint1();
        }
    }
}
