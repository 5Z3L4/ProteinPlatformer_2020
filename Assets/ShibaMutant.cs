using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShibaMutant : MonoBehaviour
{
    [Tooltip("true -> w prawo false -> w lewo")]
    public bool leftOrRight;
    public float moveSpeed;
    public float focusRange;

    private PlayerMovement _player;
    private bool _playerOnRight;
    private bool _facingRight;
    private Vector3 _baseScale;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _playerLayer;
    private Animator _myAnim;

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
        if (Mathf.Abs(Vector2.Distance(_player.transform.position, transform.position)) <= focusRange)
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
                        if (_playerOnRight && !_facingRight)
                        {
                            Flip();
                        }
                        else if (!_playerOnRight && _facingRight)
                        {
                            Flip();
                        }
                        else
                        {
                            //start running
                        }
                    }
                }
            }
            Debug.DrawLine(transform.position, _player.transform.position);
        }
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
    IEnumerator StartRunning()
    {
        _myAnim.SetBool("running", true);
        yield return new WaitForSeconds(1f);
    }
}
