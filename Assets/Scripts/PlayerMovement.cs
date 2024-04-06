using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameLogic gameLogic;
    [SerializeField] LaunchingRing ring;
    [SerializeField] float movementMaxSpeed;
    [SerializeField] float movementAcceloration;
    [SerializeField] float radius;
    [SerializeField] LayerMask stage,enemy;
    [SerializeField] bool useRB;
    [SerializeField] float velocity;
    [SerializeField] Vector3 direction;
    float gravity = -9.81f;
    bool grounded;
    Vector3 applyGravity = Vector3.zero;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        grounded = false;
        velocity = 0;
        rb = GetComponent<Rigidbody>();
        ring.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            velocity += movementAcceloration * Time.deltaTime;
            velocity = Mathf.Clamp(velocity, 0, movementMaxSpeed);
            direction = gameLogic.cam.transform.forward;
            direction.y = 0;
            direction.Normalize();
            rb.AddForce(direction*velocity,ForceMode.Force);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            velocity += movementAcceloration * Time.deltaTime;
            velocity = Mathf.Clamp(velocity, 0, movementMaxSpeed);
            direction = gameLogic.cam.transform.forward;
            direction.y = 0;
            direction.Normalize();
            rb.AddForce(-direction * velocity, ForceMode.Force);
        }
            Vector3.ClampMagnitude(rb.velocity,movementMaxSpeed);
        if (transform.position.y + transform.localScale.y <= gameLogic.center.position.y)
        {
            gameLogic.GameOver();
        }
        ring.velocity = velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Rigidbody enemy = collision.gameObject.GetComponent<Rigidbody>();
                Debug.Log("Player Punch Enemy");
                enemy.AddForce((collision.transform.position - transform.position).normalized * velocity * gameLogic.launchingPower, ForceMode.Impulse);
        }
        if(collision.gameObject.tag == "Diamond")
        {
            Destroy(collision.gameObject);
            StartCoroutine(ActivateRing());
        }
    }

    IEnumerator ActivateRing()
    {
        ring.gameObject.SetActive(true);
        yield return new WaitForSeconds(10);
        ring.gameObject.SetActive(false);
    }
  
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}
