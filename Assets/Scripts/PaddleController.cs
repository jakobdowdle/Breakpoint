using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Color PressedColor;
    public Vector3 StartPosition, StartScale;
    private Color _startColor;
    public float Speed;
    public GameObject MaxPositionX, MinPositionX;
    public static PaddleController Instance;

    void Start()
    {
        Instance = this;
        _startColor = GetComponent<SpriteRenderer>().color;
        transform.position = StartPosition;
        transform.localScale = StartScale;
    }
    void Update()
    {
        if (!GameManager.Instance.GameRunning) return;

        if (Input.GetKey(KeyCode.LeftArrow)
            && transform.position.x - StartScale.x / 2f > MinPositionX.transform.position.x)
            transform.position += Vector3.left * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.RightArrow)
            && transform.position.x + StartScale.x / 2f < MaxPositionX.transform.position.x)
            transform.position += Vector3.right * Time.deltaTime * Speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().color = PressedColor;
            Debug.Log("Hello");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().color = _startColor;
        }
    }
}
