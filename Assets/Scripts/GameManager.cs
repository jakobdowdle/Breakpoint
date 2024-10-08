using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject BallPrefab;
    public GameObject[] Bricks;
    public float RespawnTimer;
    [HideInInspector] public bool GameRunning;
    [SerializeField] private GameObject _failedPanel;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private TextMeshProUGUI _ballText;
    [SerializeField, Range(0,15)] private int _totalRespawns;
    private int _respawnCount;

    // Start is called before the first frame update
    void Start()
    {
        _failedPanel.SetActive(false);
        _victoryPanel.SetActive(false);
        GameRunning = true;
        Instance = this;
        Instantiate(BallPrefab);
        UpdateBallText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BallOutOfBounds(GameObject ball)
    {
        Destroy(ball);
        if (_respawnCount < _totalRespawns)
        {
            StartCoroutine(WaitAndRespawn());
        } else {
            GameRunning = false;
            _failedPanel.SetActive(true);
        }
    }

    public void CheckForWin(GameObject ball)
    {
        foreach(GameObject brick in Bricks)
        {
            if (brick != null) return;
        }
        Destroy(ball);
        GameRunning = false;
        _victoryPanel.SetActive(true);
    }

    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(RespawnTimer);
        Instantiate(BallPrefab);
        _respawnCount += 1;
        UpdateBallText();
    }

    private void UpdateBallText() {
        _ballText.text = "Balls Remaining: " + (_totalRespawns - _respawnCount) + "/" + _totalRespawns;
    }
}
