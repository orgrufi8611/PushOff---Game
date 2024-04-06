using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingRing : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameLogic gameLogic;
    public float velocity;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        if(velocity < 1)
        {
            velocity = 1;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Rigidbody enemy = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Player Punch Enemy Really Hard");
            enemy.AddForce((collision.transform.position - transform.position).normalized * velocity * gameLogic.launchingPower * 75, ForceMode.Impulse);
        }
    }
}
