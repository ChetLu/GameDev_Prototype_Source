using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float score;
    public bool invincibility;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");
        transform.position += new Vector3(movementX, movementY, 0) * Time.deltaTime * movementSpeed;
    }
   
    //toggle function for Toggle in the UI 
    public void Toggle_Changed(bool newValue)
    {
        //on each click bool will be flipped to opposite value
        invincibility = !invincibility;
    }
}
