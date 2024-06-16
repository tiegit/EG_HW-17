using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    private const string VelocityX = "VelocityX";
    private const string VelocityY = "VelocityY";
    private const string Grounded = "Grounded";
    private const string Idle = "Idle";
    private const string Horizontal = "Horizontal";
    private const float FlipLimit = 0.05f;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _idleAnimationTime = 2f;

    [SerializeField, Space(20)] private Transform _groundCheck;
    [SerializeField] private float _groundedRadius = .2f;
    [SerializeField] private LayerMask _whatIsGround;

    private Rigidbody2D _rigidbody2D;
    private float _horizontalMove;
    private bool _grounded;
    private Vector3 _localScale = Vector3.one;
    private bool _facingRight;
    private float _timer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxis(Horizontal);

        _timer += Time.deltaTime;

        if (_horizontalMove != 0)
        {
            _timer = 0;
        }

        if (_timer > _idleAnimationTime)
        {
            _animator.SetTrigger(Idle);
            _timer = 0;
        }

        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (_grounded)
            {
                _rigidbody2D.velocity = new Vector2(0, _jumpSpeed);
            }
        }

        float velocityX = _rigidbody2D.velocity.x;
        float velocityY = _rigidbody2D.velocity.y;

        _animator.SetBool(Grounded, _grounded);
        _animator.SetFloat(VelocityY, velocityY);
        _animator.SetFloat(VelocityX, Mathf.Abs(velocityX));

        if (velocityX > FlipLimit && !_facingRight)
        {
            Flip();
        }
        else if (velocityX < -FlipLimit && _facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_horizontalMove * _speed, _rigidbody2D.velocity.y);
    }

    private void CheckGround()
    {
        bool wasGrounded = _grounded;
        _grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _grounded = true;
            }
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}