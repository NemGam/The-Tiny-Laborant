using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;


    private int _health;
    private Rigidbody2D _rg;
    public UnityEvent<int> onHealthChange;
    public UnityEvent onDeath;
    
    private float horizontal;
    private float vertical;

    [Header("Player Settings")]
    public float walkSpeed = 20.0f;
    private bool invincibility;
    public float invincibilityDuration = 2f;
    public int exp; // 1 exp from 1 coin

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _rg = GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _health = 3;
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        _rg.velocity = new Vector2(horizontal, vertical) * (walkSpeed);
        
    }
    
    private void Rotation(Vector2 lookTarget)
    {
        Vector3 direction = (Vector2)transform.position - lookTarget;
        float angle = Mathf.Atan2(direction.x, direction.y) * -Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
        Debug.DrawLine(lookTarget, (Vector2)transform.position, Color.red);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (invincibility) return;
        
        if (other.CompareTag("DamageZone"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        StopAllCoroutines();
        _health--;
        onHealthChange?.Invoke(_health);

        if (_health <= 0)
        {
            onDeath?.Invoke();
            enabled = false;
            return;
        } 
        
        StartCoroutine(Invincibility());
    }

    private void FixedUpdate()
    {
        Move();
        Rotation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private IEnumerator Invincibility()
    {
        Color initialColor = _spriteRenderer.color;
        _spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0.2f);
        invincibility = true;
        yield return new WaitForSeconds(invincibilityDuration);
        invincibility = false;
        _spriteRenderer.color = initialColor;
    }
}
