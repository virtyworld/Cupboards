using UnityEngine;

public class Meta : MonoBehaviour
{
    private DataManager gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = new DataManager();
        gameData.Load();
        Debug.Log(gameData.Data.numberOfChip);
    }
}
