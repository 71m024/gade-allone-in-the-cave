using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 5f;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Store user input as a movement vector
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        _rb.MovePosition(transform.position + input * (Time.deltaTime * speed));
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        else if (collision.gameObject.CompareTag("Grow"))
        {
            Destroy(collision.gameObject);
            _rb.transform.localScale += new Vector3(1.0f,1.0f,1.0f)*1;
        }
        else if (collision.gameObject.CompareTag("Shrink"))
        {
            Destroy(collision.gameObject);
            if (_rb.transform.localScale.x > 1)
            {
                _rb.transform.localScale -= new Vector3(1.0f,1.0f,1.0f)*1;
            }
        }
    }
}
