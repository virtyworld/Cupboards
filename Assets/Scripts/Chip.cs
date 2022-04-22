using System;
using UnityEngine;


public class Chip : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float smoothTime = 0.1F;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    
    private bool isSelectChip;
    private string tagOfCurrentPoint;
    private Action<string> findEmptyPointsAction;
    private Action stopBlinkingPointsAction;
    private string pointNumber;
    private WayManager wayManager;
    private Vector3 currentPosition;
    private Collider2D underThePoint;
   

    public void Setup(Action<string> findPoints,Action stopBlink,WayManager wayManager)
    {
        findEmptyPointsAction = findPoints;
        stopBlinkingPointsAction = stopBlink;
        this.wayManager = wayManager;
    }
    
    private void Start()
    {
        currentPosition = new Vector3(transform.position.x,transform.position.y,0);
        //TODO get a start position from file from data
       
    }

    private void Update()
    {
        Move();
        FindFreePoints();
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        underThePoint = other;
        ChangePosition(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        underThePoint = null;
    }
  

    private void ChangePosition(Collider2D other)
    {
        if (other)
        {
            foreach (Point point in wayManager.ListPoints)
            {
                if (point.CompareTag(other.tag))
                {
                    if (!point.IsContainChip && point.IsBlinking)
                    {
                        transform.position = new Vector3(other.transform.position.x, other.transform.position.y, 0);
                        currentPosition = transform.position;
                        tagOfCurrentPoint = other.gameObject.tag;
                        Debug.Log(other.tag + " "+transform.position +" currentPosition "+currentPosition+" tagOfCurrentPoint "+tagOfCurrentPoint);
                    }
                }
            } 
        }
    }
    private void Move()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (isSelectChip) transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                SelectObject(targetObject);
                
                if (targetObject.transform.gameObject == transform.gameObject && !isSelectChip)
                {
                    transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            DeselectObject();
            stopBlinkingPointsAction?.Invoke();
            ChangePosition(underThePoint);
        }
    }

    private void SelectObject(Collider2D targetObject)
    {
        if (targetObject.CompareTag(tag))
        {
            transform.localScale = new Vector3(55, 55, 1);
            isSelectChip = true;
        }
       
    }

    private void DeselectObject()
    {
        transform.localScale = new Vector3(50, 50, 1);
        isSelectChip = false;
        transform.position = currentPosition;
    }
 

    private void FindFreePoints()
    {
        if (isSelectChip && Input.GetMouseButtonDown(0))
        {
            findEmptyPointsAction?.Invoke(tagOfCurrentPoint);
        }
    }

    
}