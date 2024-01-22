using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float respawnY = 10;
    private float _respawnX;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _respawnX = transform.position.x;
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = new Vector2(_respawnX, respawnY);
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void OnMouseDown()
    {
        Debug.Log("down");
        gameObject.SetActive(false);
        GameManager.instance.UnlistEnemy(gameObject);
    }
}
