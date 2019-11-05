
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    private RaycastHit2D hit;
    private int layermask = 2;
    private Vector3 vec;
    public float xLimit = 0f;
    private bool goalMet = false;

    public Manager man;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "MousePlayerDeath" && !goalMet)
        {
            man.playerDied();
            Destroy(this.gameObject);
        }    
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(1)) 
        
        if(man.getGoalNumber() <= 0 && !goalMet)
        {
            goalMet = true;
        }

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (mousePosition.x <= xLimit && !goalMet)
        {
            vec = new Vector3(xLimit, mousePosition.y, 0f);
            transform.position = Vector2.Lerp(transform.position, vec, moveSpeed);
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
        

    }
}
