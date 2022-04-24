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

    void Start()
    {
        gameData = Instantiate(dataManagerPrefab);
        gameData.Load();
        wayManager = Instantiate(wayManagerPrefab);
        wayManager.Setup(pointsArrayPrefabs,chipPrefabs,listSpawnPoints,gameData.Data);
    }
}
