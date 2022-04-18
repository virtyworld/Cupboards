using System.Collections.Generic;
using UnityEngine;

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
    private bool isPointEmpty;
    private List<Point> listPoints = new List<Point>();

    public void Setup(List<Point> listPoints)
    {
        this.listPoints = listPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FindEmptyWayPoint(Points pointNumber)
    {
        if (pointNumber == Points.Point1)
        {
            GetNearestFromPoint(Points.Point4);
        }
        else if (pointNumber == Points.Point5)
        {
            GetNearestFromPoint(Points.Point4);
        }
        else if (pointNumber == Points.Point3)
        {
            GetNearestFromPoint(Points.Point6);
        }
        else if (pointNumber == Points.Point4)
        {
            GetNearestFromPoint(Points.Point1);
            GetNearestFromPoint(Points.Point5);
            GetNearestFromPoint(Points.Point7);
        }
        else if (pointNumber == Points.Point5)
        {
            GetNearestFromPoint(Points.Point2);
            GetNearestFromPoint(Points.Point4);
            GetNearestFromPoint(Points.Point6);
            GetNearestFromPoint(Points.Point8);
        }
        else if (pointNumber == Points.Point6)
        {
            GetNearestFromPoint(Points.Point3);
            GetNearestFromPoint(Points.Point5);
            GetNearestFromPoint(Points.Point9);
        }
        else if (pointNumber == Points.Point7)
        {
            GetNearestFromPoint(Points.Point4);
        }
        else if (pointNumber == Points.Point8)
        {
            GetNearestFromPoint(Points.Point5);
        }
        else if (pointNumber == Points.Point9)
        {
            GetNearestFromPoint(Points.Point6);
        }
    }

    private void GetNearestFromPoint(Points pointNumber)
    {
        //nearest point is 4
        foreach (Point point in listPoints)
        {
            if (point.tag.Equals(pointNumber))
            {
                if (point.IsEmpty)
                {
                    //TODO miganie
                    point.StartBlinking();
                }
            }
        }
    }
}