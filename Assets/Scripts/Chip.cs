using System;
using UnityEngine;


public class Chip : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float smoothTime = 0.1F;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    private GameObject selectedObject;
    private bool isSelectChip;
    private bool isCollisionStay2D;
    private string tagOfCurrentPoint;
    private Action<string> findEmptyPointsAction;

    public void Setup(Action<string> findEmptyPointsAction)
    {
        this.findEmptyPointsAction = findEmptyPointsAction;
    }
    
    private void Start()
    {
    }

    private void Update()
    {
        Move();
        FindFreePoints();
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
                if (targetObject.transform.gameObject == transform.gameObject && !isSelectChip)
                {
                    SelectObject();
                    transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            DeselectObject();
        }
    }

    private void SelectObject()
    {
        transform.localScale = new Vector3(55, 55, 1);
        isSelectChip = true;
    }

    private void DeselectObject()
    {
        transform.localScale = new Vector3(50, 50, 1);
        isSelectChip = false;
    }
 

    private void FindFreePoints()
    {
        if (isSelectChip && Input.GetMouseButtonDown(0))
        {
            if (isCollisionStay2D)
            {
                //TODO отправляем в вэй менеджер инфу о поиске свободных слотов
                findEmptyPointsAction?.Invoke(transform.tag);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        isCollisionStay2D = true;
        tagOfCurrentPoint = other.gameObject.tag;
        //TODO отправляем запрос,что эта фишка находится на этой точке
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isCollisionStay2D = false;
    }
}