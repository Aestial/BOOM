using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] string enemyTag = "Enemy";
    public float speed = 1f;
    public float delay = 0f;
    public float lifeTime = 1f;
    Transform player;
    Vector3 deltaPosition;
    // Vector3 originalPosition;
    float startTime;
    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // originalPosition = this.transform.position;
        deltaPosition = new Vector3(speed, 0, 0);
        StartCoroutine(WaitAndActive());
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            this.transform.position += deltaPosition;
            if (Time.time - startTime >= lifeTime)
            {
                RestartPosition();
                startTime = Time.time;
            }
        }
    }

    void RestartPosition()
    {
        Vector3 playerPos = player.position;
        this.transform.position = playerPos;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        Debug.Log(collider.name + "  " + (collider.tag == enemyTag));
        if (collider.tag == enemyTag)
        {
            Debug.Log("Bullet collided!");
            Destroy(collider.gameObject);
            RestartPosition();
        }
    }

    IEnumerator WaitAndActive()
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
        startTime = Time.time;
    }
}
