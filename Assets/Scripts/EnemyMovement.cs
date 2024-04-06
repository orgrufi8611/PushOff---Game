using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameLogic gameLogic;
    public float movementMaxSpeed;
    [SerializeField] float movementAcceloration;
    [SerializeField] LayerMask stage,player;
    [SerializeField] float radius;
    [SerializeField] bool useRB;
    [SerializeField] float velocity;
    float gravity = -9.81f;
    bool grounded;
    Vector3 applyGravity = Vector3.zero;
    //CharacterController cC;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        velocity = 0;
        //cC = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity += movementAcceloration * Time.deltaTime;
        velocity = Mathf.Clamp(velocity, 0, movementMaxSpeed);
        rb.AddForce((gameLogic.player.position - transform.position).normalized * velocity, ForceMode.Force);
        Vector3.ClampMagnitude(rb.velocity, movementMaxSpeed);
        if (transform.position.y + transform.localScale.y <= gameLogic.center.position.y)
        {
            gameLogic.BallDropped();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody player = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Enemy Punch Player");
            player.AddForce((collision.transform.position - transform.position).normalized * velocity * gameLogic.launchingPower, ForceMode.Impulse);
            
        }
    }
   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
