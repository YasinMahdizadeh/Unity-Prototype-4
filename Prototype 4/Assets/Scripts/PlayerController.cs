using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float forwardInput;

    public float speed = 5;
    private Rigidbody playerRb;
    public float collisionIndex = 5.0f;

    public bool hasPowerup = false;
    public GameObject powerupIndicator;
    private GameObject focalPoint;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        //powerupIndicator = GameObject.Find("Powerup Indicator");
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput 
            * Time.deltaTime * speed);
        powerupIndicator.gameObject.transform.position = transform.position + new Vector3(0, 0.8f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountDownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position -
                transform.position).normalized;

            Debug.Log("Player collided with " + collision.gameObject + " & powerup is set to "
                + hasPowerup);

            enemyRigidbody.AddForce(awayFromPlayer * collisionIndex, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
