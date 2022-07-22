using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    public float speed = 8f;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        var xInput = Input.GetAxis("Horizontal");
        var yInput = Input.GetAxis("Vertical");

        var xSpeed = xInput * speed;
        var ySpeed = yInput * speed;

        var newVelocity = new Vector3(xSpeed, 0, ySpeed);
        _playerRigidbody.velocity = newVelocity;
        // _playerRigidbody.AddForce(newVelocity * .01f, ForceMode.VelocityChange);
        // _playerRigidbody.AddForce(newVelocity * .01f, ForceMode.Impulse);
        // _playerRigidbody.AddForce(newVelocity * .01f, ForceMode.Force);
        // _playerRigidbody.AddForce(newVelocity * .01f, ForceMode.Acceleration);
    }

    public void Die()
    {
        gameObject.SetActive(false);
        var gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}
