using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int damage;
    public GameObject explosionPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3( transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }

    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().DecreaseHealth(damage);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("EnemyMissile")) {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            await Task.Delay(TimeSpan.FromSeconds(2));
            DestroyImmediate(explosion);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
