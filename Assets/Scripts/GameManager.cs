using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject BallPrefab;
    public float RespawnTimer;
    [SerializeField, Range(0,15)] private int _maxRespawns;
    private int _respawnCount;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Instantiate(BallPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BallOutOfBounds(GameObject ball)
    {
        Destroy(ball);
        _respawnCount += 1;
        if (_respawnCount < _maxRespawns)
        {
            StartCoroutine(WaitAndRespawn());
        }  
    }

    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(RespawnTimer);
        Instantiate(BallPrefab);
    }
}
