using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject playerObj = null;
    bool goRight;
    public Vector3 a; 
    
    public Transform firePoint;
    public GameObject missilePrefab;

    public float pointForKill;
    public float enemySpeed;
    public float enemyLimit;

    // Start is called before the first frame update
    void Start()
    {
        //vector stores enemy's initial spawn coordinates
        a = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        if (playerObj == null)
        {
            playerObj = GameObject.FindWithTag("Player");            
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        //check if player position is inline with vertical axis and below the enemy 
        ShootPlayer();
        Movement();
    }

    //enemy will shoot player if they are below them on the yaxis and fall in line with them on the xaxis
    void ShootPlayer()
    {
        if (Mathf.Abs(playerObj.transform.position.x - transform.position.x) <= 0.01f && playerObj.transform.position.y < transform.position.y)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
    }

    
    public void GetHit()
    {
        //remove the enemy
        Destroy(gameObject);
        //on removal of enemy add points for kill to the player skill
        playerObj.GetComponent<Player>().score += pointForKill;
    }

    //have enemies move side to side 
    public void Movement()
    {
        //if  enemy is at spawn point goRight is set to true and begin moving right until it reaches the end point and start travelling left
        if (Mathf.Abs(transform.position.x- a.x) <= 0.1f)
        {
            goRight = true;
            
        }
        else if (Mathf.Abs(transform.position.x - (a.x + enemyLimit)) <= 0.1f)
        {
            goRight = false;
        }

        if (goRight)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * enemySpeed;
        }
        else
        {
            transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * enemySpeed;
        }
        
    }
}
