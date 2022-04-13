using UnityEngine;
using UnityEngine.UI;

public class Meta : MonoBehaviour
{
    private DataManager gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = new DataManager();
        gameData.Load();
    }
}
