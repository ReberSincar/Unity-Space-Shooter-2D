using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public GameObject missilePrefab;
    public int health = 100;
    private float speed = 10f;
    private float xMax;
    private float xMin;
    Text healthText;
    public AudioClip explosion;
    public AudioClip fire;
    public GameObject bigExplosionPrefab;
    public GameObject tinyExplosionPrefab;
    public SceneController sceneController;
    void Start()
    {
        healthText = GameObject.Find("Health").GetComponent<Text>();
        sceneController = GameObject.Find("SceneManager").GetComponent<SceneController>();
        healthText.text = health.ToString();
        float zCamera =  Camera.main.transform.position.z;
        Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0,0, zCamera));
        Vector3 rigth = Camera.main.ViewportToWorldPoint(new Vector3(1,0, zCamera));
        xMin = left.x + 0.7f;
        xMax = rigth.x - 0.8f;
    }

    // Update is called once per frame
    async void Update()
    {
        float newXPosition = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("FireMissile", 0.00001f, 0.3f);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("FireMissile");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += speed * Time.deltaTime * Vector3.left;
        } else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
        }

        if(health <= 0)
        {
            GameObject explosionObject = Instantiate(bigExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(explosion, transform.position);
            sceneController.LoadGameOverScene();
            await Task.Delay(TimeSpan.FromSeconds(2));
            DestroyImmediate(explosionObject);
        }
    }

    public async void DecreaseHealth(int incomingDamage) {
        health -= incomingDamage;
        healthText.text = health < 0 ? "0" : health.ToString();
        if (health > 0) {
            GameObject explosionObject = Instantiate(tinyExplosionPrefab, transform.position, Quaternion.identity);
            explosionObject.transform.parent = gameObject.transform;
            await Task.Delay(TimeSpan.FromSeconds(2));
            DestroyImmediate(explosionObject);
        }
    }

    void FireMissile()
    {
        Instantiate(missilePrefab, new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), Quaternion.identity);
        AudioSource.PlayClipAtPoint(fire, transform.position);
    }
}
