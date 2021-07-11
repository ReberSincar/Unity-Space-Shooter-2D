using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosition : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float speed;
    private bool isRight = true;
    private float xMax;
    private float xMin;
    public int level = 0;
    void Start()
    {
        float zCamera = Camera.main.transform.position.z;
        Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zCamera));
        Vector3 rigth = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zCamera));
        xMin = left.x + 0.7f;
        xMax = rigth.x - 0.8f;

        CreateEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            transform.position += speed * level* 0.2f * Vector3.right * Time.deltaTime;
        }
        else
        {
            transform.position += speed * level * 0.2f * Vector3.left * Time.deltaTime;
        }
        float rightLimit = transform.position.x * 5;
        float leftLimit = transform.position.x * 5;

        if(rightLimit > xMax) {
            isRight = false;
        } else if(leftLimit < xMin){
            isRight = true;
        }

        if (IsAllEnemiesDies())
        {
            CreateEnemies();
        }
    }

    void CreateEnemies()
    {
        level += 1;
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
            enemy.transform.parent = child;
        }
    }

    bool IsAllEnemiesDies()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount > 0)
                return false;
        }

        return true;
    }
}
