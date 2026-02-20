using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform playerParent;
    public GameObject unemployedManPrefab;
    public GameObject averageManPrefab;
    public GameObject richManPrefab;

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        var user = SessionManager.Instance.LoggedUser;

        if (user == null)
        {
            Debug.LogError("NO LOGGED USER!");
            return;
        }

        GameObject prefabToSpawn = null;

        if (user.income <= 0)
            prefabToSpawn = unemployedManPrefab;
        else if (user.income < 10000)
            prefabToSpawn = averageManPrefab;
        else
            prefabToSpawn = richManPrefab;

        GameObject player = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation, playerParent);

        CameraScript cam = Camera.main.GetComponent<CameraScript>();
        cam.target = player.transform;

        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        pm.cameraTransform = Camera.main.transform;
    }
}