using System.Collections;
using System.Collections.Generic;
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

        if ((transform.position.x + transform.localScale.x /2 >=
            _paddle.transform.position.x - _paddle.transform.localScale.x / 2f)
            && 
            (transform.position.x - transform.localScale.x /2 <=
            _paddle.transform.position.x + _paddle.transform.localScale.x / 2f)
            && 
            (transform.position.y + transform.localScale.y /2 >=
            _paddle.transform.position.y - _paddle.transform.localScale.y / 2f)
            &&
            (transform.position.y - transform.localScale.y /2 <=
            _paddle.transform.position.y + _paddle.transform.localScale.y / 2f))
        {

            _velocity.y *= -1;
            float random = Random.Range(-_maxHorizontalSpeed, _maxHorizontalSpeed);
            print(random);
            _velocity.x = random;
        }

        if (((transform.position.x - transform.localScale.x /2) <= _paddle.MinPositionX.transform.position.x)
            ||
            ((transform.position.x + transform.localScale.x /2) >= _paddle.MaxPositionX.transform.position.x)) {
            _velocity.x *= -1;
        }

        if (((transform.position.y + transform.localScale.y / 2) >= _paddle.MaxPositionX.transform.position.y)) {
            _velocity.y *= -1;
        }

    }
}
