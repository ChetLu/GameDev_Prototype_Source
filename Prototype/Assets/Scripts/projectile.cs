using UnityEngine;

public class projectile : MonoBehaviour
{

    public float projectileSpeed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //sets velocity of missile 
        rb.velocity = transform.up * projectileSpeed;
    }

    //when missile collider comes into contact with another object in the scene 
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
       
        Asteroid a = hitInfo.GetComponent<Asteroid>();
        Enemy e = hitInfo.GetComponent<Enemy>();
       
        //if missile collides with an asteroid calls GetHit function which removes object from the scene
        if ( a != null)
        {
            a.GetHit();
        }
        //if the object is an enemy, its GetHit function is called and enemy is removed and will add to the player score
        else if(e != null)
        {
            e.GetHit();
        }
        Debug.Log(hitInfo.name); 
        //remove missile oject upon collision
        Destroy(gameObject);
    
    }
    
}
