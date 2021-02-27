using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public void GetHit()
    {
        Destroy(gameObject);
    }

}
