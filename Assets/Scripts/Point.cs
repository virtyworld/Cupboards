using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private bool isContainChip;
    private string chipTag;
    private bool isBlinking;
   
    public bool IsBlinking => isBlinking;
    public bool IsContainChip => isContainChip;
    public string ChipTag => chipTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isNearest",isBlinking);
    }
  

    private void OnTriggerStay2D(Collider2D other)
    {
        isContainChip = true;
        chipTag = other.gameObject.tag;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isContainChip = false;
        chipTag = "";
    }

    public void StartBlinking()
    {
        isBlinking = true;
    }

    public void StopBlinking()
    {
        isBlinking = false;
    }  
    
}
