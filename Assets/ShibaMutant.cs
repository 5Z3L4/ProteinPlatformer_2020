using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShibaMutant : MonoBehaviour
{
    [Tooltip("true -> w prawo false -> w lewo")]
    public bool leftOrRight;
    public float startingMoveSpeed;
    public float maxMoveSpeed;
    public float increaseMoveSpeedStep;
    public float focusRange;
    public Rigidbody2D rb;
    public Transform wallCheckingCastPos;

    private PlayerMovement _player;
    private bool _playerOnRight;
    private bool _facingRight;
    private bool _isRunning = false;
    private bool _isAttacking = false;
    private bool _isDizzy = false;
    private float _changingMoveSpeed;
    private Vector3 _baseScale;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _playerLayer;
    private Animator _myAnim;
    private float baseCastDist = 0.35f;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _myAnim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _baseScale = transform.localScale;
        _facingRight = leftOrRight;
        if (!leftOrRight)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(_player.transform.position, transform.position)) <= focusRange && !_isDizzy)
        {
            RaycastHit2D hitGround = Physics2D.Linecast(transform.position, _player.transform.position, _groundLayer);
            if (hitGround.collider == null)
            {
                RaycastHit2D hitPlayer = Physics2D.Linecast(transform.position, _player.transform.position, _playerLayer);
                if (hitPlayer.collider != null)
                {
                    if (hitPlayer.collider.gameObject.CompareTag("Player"))
                    {
                        CheckPlayerPos(_player.transform.position);
                        if (!_isRunning && !_isAttacking && ((_playerOnRight && _facingRight)||(!_playerOnRight && !_facingRight)))
                        {
                            StartCoroutine(StartRunning());
                        }
                    }
                }
            }
            Debug.DrawLine(transform.position, _player.transform.position);
        }
        if (_changingMoveSpeed > 0 && !_isAttacking)
        {
            if (_changingMoveSpeed < maxMoveSpeed)
            {
                _changingMoveSpeed += increaseMoveSpeedStep * Time.deltaTime;
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (_isRunning)
        {
            if (_facingRight)
            {
                rb.velocity = new Vector2(_changingMoveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-_changingMoveSpeed, rb.velocity.y);
            }
            if (IsHittingWall())
            {
                StartCoroutine(HitsWall());
            }
        }
    }
    //IEnumerator Attack()
    //{

    //}
    IEnumerator HitsWall()
    {
        StopRunning();
        _isDizzy = true;
        _myAnim.SetBool("IsDizzy", true);
        yield return new WaitForSeconds(3f);
        _myAnim.SetBool("IsDizzy", false);
        _isDizzy = false;
        StopRunning();
        Flip();
    }
    IEnumerator StartRunning()
    {
        _isRunning = true;
        _myAnim.SetBool("GetsAngry", true);
        yield return new WaitForSeconds(1f);
        _myAnim.SetBool("GetsAngry", false);
        _myAnim.SetBool("IsRunning", true);
        _changingMoveSpeed = startingMoveSpeed;
    }
    private void StopRunning()
    {
        _myAnim.SetBool("IsRunning", false);
        _isRunning = false;
        _changingMoveSpeed = 0;
    }
    private void CheckPlayerPos(Vector2 playerPos)
    {
        if ((playerPos.x - transform.position.x) >= 0)
        {
            _playerOnRight = true;
        }
        else if ((playerPos.x - transform.position.x) < 0)
        {
            _playerOnRight = false;
        }
    }
    public void Flip()
    {
        Vector3 newScale = _baseScale;
        newScale.x = transform.localScale.x * -1;
        transform.localScale = newScale;
        _facingRight = !_facingRight;
    }
    bool IsHittingWall()
    {
        bool val = false;
        float castDist = baseCastDist;
        if (!_facingRight)
        {
            castDist = -baseCastDist;
        }
        Vector3 targetPos = wallCheckingCastPos.position;
        targetPos.x += castDist;
        Debug.DrawLine(wallCheckingCastPos.position, targetPos, Color.blue);
        if (Physics2D.Linecast(wallCheckingCastPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }
        return val;
    }
}
