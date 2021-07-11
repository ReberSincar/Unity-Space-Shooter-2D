using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 10;
    public int scorePoint = 100;
    public GameObject enemyMissilePrefab;
    ScoreController scoreController;
    public AudioClip explosion;
    public AudioClip fire;
    public GameObject smallExplosionPrefab;
    public GameObject tinyExplosionPrefab;
    private EnemyPosition enemyPosition;
    void Start()
    {
        InvokeRepeating("FireMissile", 0.1f, 1f);
        scoreController = GameObject.Find("Score").GetComponent<ScoreController>();
        enemyPosition = GameObject.Find("EnemyOrder").GetComponent<EnemyPosition>();
    }

    private async void Update()
    {
        if(health <= 0)
        {
            GameObject explosionObject = Instantiate(smallExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(explosion, transform.position);
            scoreController.IncreaseScore(scorePoint*enemyPosition.level);
            await Task.Delay(TimeSpan.FromSeconds(2));
            DestroyImmediate(explosionObject);
        }
    }

    public async void DecreaseHealth(int damage) {
        health -= damage;
        if(health > 0)
        {
            GameObject explosionObject = Instantiate(tinyExplosionPrefab, transform.position, Quaternion.identity);
            explosionObject.transform.parent = gameObject.transform;
            await Task.Delay(TimeSpan.FromSeconds(2));
            DestroyImmediate(explosionObject);
        }
    }

    void FireMissile()
    {
        if(UnityEngine.Random.value < 0.3 + enemyPosition.level*0.1)
        {
            GameObject missile = Instantiate(enemyMissilePrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
            missile.transform.rotation = Quaternion.Euler(180, 0, 0);
            AudioSource.PlayClipAtPoint(fire, transform.position);
        }
    }
}
