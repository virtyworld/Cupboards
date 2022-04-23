using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Points
{
    Point1,
    Point2,
    Point3,
    Point4,
    Point5,
    Point6,
    Point7,
    Point8,
    Point9
}

public class WayManager : MonoBehaviour
{
    private GameData gameData;
    private Point[] pointsArrayPrefabs;
    private Chip[] chipPrefabs;
    private Transform[] listSpawnPoints;
    private List<Point> listPoints = new List<Point>();
    private Action<string> findEmptyPointsAction;
    private Action stopBlinkingPointsAction;
    private List<Chip> chips = new List<Chip>();

    public List<Point> ListPoints => listPoints;


    public void Setup(Point[] pointsArrayPrefabs, Chip[] chipPrefabs, Transform[] listSpawnPoints, GameData gameData)
    {
        this.pointsArrayPrefabs = pointsArrayPrefabs;
        this.chipPrefabs = chipPrefabs;
        this.listSpawnPoints = listSpawnPoints;
        this.gameData = gameData;
    }

    // Start is called before the first frame update
    void Start()
    {
        findEmptyPointsAction += FindEmptyWayPoint;
        stopBlinkingPointsAction += StopBlink;
        PrefabInitialize();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void PrefabInitialize()
    {
        // Spawn points
        if (gameData.numberOfPoints != null)
        {
            int numberOfPoint;
            if (int.TryParse(gameData.numberOfPoints, out numberOfPoint))
            {
                for (int i = 0; i < numberOfPoint; i++)
                {
                    Point point = Instantiate(pointsArrayPrefabs[i], listSpawnPoints[i].transform);
                    listPoints.Add(point);

                    // Check listOfCoordinatesToPlacedTheChip for contain coordinates
                    // if (gameData.listOfCoordinatesToPlacedTheChip[i] != null)
                    // {
                    //     string[] sArray = gameData.listOfCoordinatesToPlacedTheChip[i].Split(',');
                    //
                    //     // store as a Vector3
                    //     Vector3 coordPoint = new Vector3(
                    //         float.Parse(sArray[0]),
                    //         float.Parse(sArray[1]),
                    //         0);
                    //
                    //     Point point = Instantiate(pointsArrayPrefabs[i], listSpawnPoints[i].transform);
                    //     point.transform.position = coordPoint;
                    //     listPoints.Add(point);
                    // }
                    // else
                    // {
                    //     Point point = Instantiate(pointsArrayPrefabs[i], listSpawnPoints[i].transform);
                    //     listPoints.Add(point);
                    // }
                }
            }
        }
        // else
        // {
        //     for (int i = 0; i < pointsArrayPrefabs.Length; i++)
        //     {
        //         Point point = Instantiate(pointsArrayPrefabs[i], listSpawnPoints[i].transform);
        //         listPoints.Add(point);
        //     }
        // }

        // Spawn chips
        if (gameData.numberOfChip != null)
        {
            //  TODO winningPointsOfChips, numberOfConnects, listOfConnectsBetweenCouplePoints
            int numberOfChip;
            if (int.TryParse(gameData.numberOfChip, out numberOfChip))
            {
                for (int i = 0; i < numberOfChip; i++)
                {
                    // Check initialPointsOfChips for contain points
                    if (gameData.initialPointsOfChips != null)
                    {
                        string[] sArray = gameData.initialPointsOfChips.Split(',');

                        if (!chipPrefabs[i] || !listSpawnPoints[i]) return;
                        Chip chip = Instantiate(chipPrefabs[i], listSpawnPoints[int.Parse(sArray[i]) - 1].transform);
                        chip.Setup(findEmptyPointsAction, stopBlinkingPointsAction, this, int.Parse(sArray[i]));
                        chips.Add(chip);
                    }
                    else
                    {
                        if (!chipPrefabs[i] || !listSpawnPoints[i]) return;
                        Chip chip = Instantiate(chipPrefabs[i], listSpawnPoints[i].transform);
                        chip.Setup(findEmptyPointsAction, stopBlinkingPointsAction, this, i);
                        chips.Add(chip);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < chipPrefabs.Length; i++)
            {
                Chip chip = Instantiate(chipPrefabs[i], listSpawnPoints[i].transform);
                chip.Setup(findEmptyPointsAction, stopBlinkingPointsAction, this, i);
                chips.Add(chip);
            }
        }
    }


    private void FindEmptyWayPoint(string pointNumber)
    {
        if (pointNumber == Points.Point1.ToString()) GetNearestFromPoint(Points.Point4);
        else if (pointNumber == Points.Point2.ToString()) GetNearestFromPoint(Points.Point5);
        else if (pointNumber == Points.Point3.ToString()) GetNearestFromPoint(Points.Point6);
        else if (pointNumber == Points.Point4.ToString())
            GetNearestFromPoint(Points.Point1, Points.Point5, Points.Point7);
        else if (pointNumber == Points.Point5.ToString())
            GetNearestFromPoint(Points.Point2, Points.Point4, Points.Point6, Points.Point8);
        else if (pointNumber == Points.Point6.ToString())
            GetNearestFromPoint(Points.Point3, Points.Point5, Points.Point9);
        else if (pointNumber == Points.Point7.ToString()) GetNearestFromPoint(Points.Point4);
        else if (pointNumber == Points.Point8.ToString()) GetNearestFromPoint(Points.Point5);
        else if (pointNumber == Points.Point9.ToString()) GetNearestFromPoint(Points.Point6);
    }

    private void GetNearestFromPoint(Points pointNumber1, Points? pointNumber2 = null, Points? pointNumber3 = null,
        Points? pointNumber4 = null)
    {
        foreach (Point point in listPoints)
        {
            if (point.tag == pointNumber1.ToString() ||
                point.tag == pointNumber2.ToString() ||
                point.tag == pointNumber3.ToString() ||
                point.tag == pointNumber4.ToString())
            {
                if (!point.IsContainChip && Input.GetMouseButtonDown(0))
                {
                    point.StartBlinking();
                }
            }
            else
            {
                if (point.IsBlinking)
                {
                    Debug.Log("point.tag " + point.tag + " StopBlinking");
                    point.StopBlinking();
                }
            }
        }
    }

    private void StopBlink()
    {
        foreach (Point point in listPoints)
        {
            point.StopBlinking();
        }

        CheckForWin();
    }

    private void CheckForWin()
    {
        if (Win()) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private bool Win()
    {
        StringBuilder s = new StringBuilder();

        foreach (var chip in chips)
        {
            s.Append(chip.TagOfCurrentPoint);
        }

        s.Replace("Point", ",");
        s.Remove(0, 1);

        if (s.Equals(gameData.winningPointsOfChips))
        {
            return true;
        }

        return false;
    }
}