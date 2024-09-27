using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public Color BallColor;
    [SerializeField] [Range(0, 50)] private float _verticalSpeed;
    [SerializeField] [Range(0, 50)]  private float _maxHorizontalSpeed;
    public Transform MinBounds, MaxBounds;
    private Vector3 _velocity = Vector3.down;
    private PaddleController _paddle;

    void Update()
    {
        _paddle = PaddleController.Instance;
        transform.position += new Vector3(_velocity.x, _velocity.y * _verticalSpeed) * Time.deltaTime;

        if (CollidesWithGameObject(PaddleController.Instance.gameObject))
        {
            _velocity.y *= -1;
            Vector3 paddleToBall = transform.position - PaddleController.Instance.gameObject.transform.position;
            _velocity.x = paddleToBall.x * _maxHorizontalSpeed;
        }
        for(int i = 0; i < GameManager.Instance.Bricks.Length; i++)
        {
            if (GameManager.Instance.Bricks[i] != null && CollidesWithGameObject(GameManager.Instance.Bricks[i]))
            {
                Destroy(GameManager.Instance.Bricks[i]);
                GameManager.Instance.Bricks[i] = null;
                float random = Random.Range(-_maxHorizontalSpeed, _maxHorizontalSpeed);
                print(random);
                _velocity.x = random;
                _velocity.y = -_velocity.y;
                GameManager.Instance.CheckForWin(gameObject);
                break;
            }
        }


        if (((transform.position.x - transform.localScale.x /2) <= _paddle.MinPositionX.transform.position.x)
            ||
            ((transform.position.x + transform.localScale.x /2) >= _paddle.MaxPositionX.transform.position.x)) {
            _velocity.x *= -1;
        }

        if (((transform.position.y + transform.localScale.y / 2) >= _paddle.MaxPositionX.transform.position.y)) {
            _velocity.y *= -1;
        }

        //Checks if the ball went below bottom of screen
        if (((transform.position.y - transform.localScale.y / 2) < _paddle.MinPositionX.transform.position.y))
        {
            GameManager.Instance.BallOutOfBounds(gameObject);
        }

    }

    private bool CollidesWithGameObject(GameObject other)
    {
        return
        ((transform.position.x + transform.localScale.x / 2 >=
        other.transform.position.x - other.transform.localScale.x / 2f)
        &&
        (transform.position.x - transform.localScale.x / 2 <=
        other.transform.position.x + other.transform.localScale.x / 2f)
        &&
        (transform.position.y + transform.localScale.y / 2 >=
        other.transform.position.y - other.transform.localScale.y / 2f)
        &&
        (transform.position.y - transform.localScale.y / 2 <=
        other.transform.position.y + other.transform.localScale.y / 2f));
    }
}
