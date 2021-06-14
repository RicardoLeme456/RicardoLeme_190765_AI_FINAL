using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour {

    [Header("Speed")]
	float speed = 20.0F;
    float rotationSpeed = 120.0F;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    [Header("Vida")]
    /*public Slider healthBar;
    float health = 100.0f;*/
    public GameManager manager;
    public Slider healthBar;
    public float health = 100f;

    [Header("Collider")]
    float translationMovementValue;
    public GameObject player;
    public Transform playerCol;

    [Header("Respawn")]
    public GameObject prefab;

    [SerializeField] private Transform playerDown;
    [SerializeField] private Transform respawnPoint;

    private void Start()
    {
        InvokeRepeating("UpdateHealth", 5, 5.0f);

        CheckMovement();
    }

    void Update() {
        /*Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);
        healthBar.value = (int)health;
        healthBar.transform.position = healthBarPos + new Vector3(0, 60, 0);*/

        healthBar.maxValue = health;
        healthBar.value = health;

        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if(Input.GetKeyDown("space"))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*2000);
        }
        
    }

    /* void UpdateHealth()
     {
         if (health < 100)
             health++;
     }*/

     void OnCollisionEnter(Collision col)
     {
         if (col.gameObject.tag == "Shell")
         {
            /*health -= 10;

            if(health <= 0)
            {
                playerDown.transform.position = respawnPoint.transform.position;
                var position = new Vector3(Random.Range(-87.4f, 87.4f), 0, Random.Range(134.9f, -180f));
                Instantiate(prefab, position, Quaternion.identity);
            }*/

            TakeDamage(2f);
        }
     }

    public void TakeDamage(float amnt)
    {
        health -= amnt;
        if (health <= 0f)
        {
            manager.GameOver();
        }
        float _h = Mathf.Clamp(health, 0, 100f);
        healthBar.value = _h;
    }

    void CheckMovement()
    {
        RaycastHit hitL, hitR;

        if (Physics.Raycast(player.transform.position, Vector3.left, out hitL))
        {
            if (hitL.distance < translationMovementValue)
            {
                translationMovementValue = hitL.distance;
            }
        }

        if (Physics.Raycast(player.transform.position, Vector3.right, out hitR))
        {
            if (hitR.distance < translationMovementValue)
            {
                translationMovementValue = hitR.distance;
            }
        }
    }
}
