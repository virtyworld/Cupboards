using UnityEngine;

public class Point : MonoBehaviour
{
    private bool isEmpty;
    private string chipTag;

    public bool IsEmpty => isEmpty;
    public string ChipTag => chipTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        isEmpty = false;
        chipTag = other.gameObject.tag;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isEmpty = true;
        chipTag = "";
    }

    public void StartBlinking()
    {
        //TODO start animation
    }

  
}
