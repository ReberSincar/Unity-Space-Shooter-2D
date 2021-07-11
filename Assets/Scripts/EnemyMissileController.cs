using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileController : MonoBehaviour
{
    public float speed;
    public int damage;
    EnemyPosition enemyPosition;

    private void Start()
    {
        enemyPosition = GameObject.Find("EnemyOrder").GetComponent<EnemyPosition>();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (speed + enemyPosition.level / 2f) * Time.deltaTime, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            collision.gameObject.GetComponent<ShipController>().DecreaseHealth(damage*enemyPosition.level);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
