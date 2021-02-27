using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject playerObj = null;
    public float projectileSpeed;
    public Rigidbody2D rb;
    private bool invincibileState;
    public Vector3 a;

    // Start is called before the first frame update
    void Start()
    {
        a = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //assigns Player to the playerObj
        if (playerObj == null)
        {
            playerObj = GameObject.FindWithTag("Player");
        }

        //sets the velocity of the projectile
        rb.velocity = transform.up * projectileSpeed;
    }

    void Update()
    {
        TooFar();
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //sets bool to the invicibilty state of the player
        invincibileState = playerObj.GetComponent<Player>().invincibility;

        if (playerObj != null)
        {
            //if invincibility is on, missiles pass through player
            if (invincibileState)
            { 
                return;
            }
            else
            {
                Debug.Log("player hit");
                //invicibility off, remove player
                Destroy(playerObj);
            }
           
        }

        Debug.Log(hitInfo.name);
        //remove missile from scene upon collision
        Destroy(gameObject);
    }

    void TooFar()
    {
        if (Mathf.Abs(transform.position.y - a.y) >= 10)
        {
            Destroy(gameObject);
        }
    }
}
