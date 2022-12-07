using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust;
    [SerializeField] float rotationThrust;
    [SerializeField] AudioClip engineThrust;

    AudioSource audioSource;
    Rigidbody rb;

    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                /*audioSource.Play();*/
                audioSource.PlayOneShot(engineThrust, 1f);
                Debug.Log("Playing Audio");
            }
        }
        else if (gameObject.tag == "Crashed")
        {
            Debug.Log("Crash");
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.D))
            ApplyRotation(rotationThrust);
        
        else if (Input.GetKey(KeyCode.A))
            ApplyRotation(-rotationThrust);
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }
}

