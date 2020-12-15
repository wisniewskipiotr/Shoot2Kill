using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{


    public float moveSpeed, lifeSpam;
    public Rigidbody theRB;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;

        lifeSpam -= Time.deltaTime;

        if(lifeSpam <= 0)
        {

            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }
        Destroy(gameObject);
    }
}
