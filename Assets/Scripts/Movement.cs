using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust;
    [SerializeField] float rotationThrust;
    [SerializeField] AudioClip engineThrust;

    [SerializeField] private ParticleSystem _mainEngineParcticles;
    [SerializeField] private ParticleSystem _rightEngineParcticles;
    [SerializeField] private ParticleSystem _leftEngineParcticles;

    AudioSource audioSource;
    Rigidbody rb;

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
            Thrust();
        }
        else
        {
            ThrustStop();
        }
    }

    private void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else
        {
            RotateStop();
        }
    }

    private void Thrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.PlayOneShot(engineThrust, 1f);
            Debug.Log("Playing Audio");
        }
        if (!_mainEngineParcticles.isPlaying)
        {
            _mainEngineParcticles.Play();
        }
    }

    private void ThrustStop()
    {
        _mainEngineParcticles.Stop();
        audioSource.Stop();
    }

    private void RotateLeft()
    {
        if (!_rightEngineParcticles.isPlaying)
        {
            _rightEngineParcticles.Play();
        }
        ApplyRotation(rotationThrust);
    }

    private void RotateRight()
    {
        if (!_leftEngineParcticles.isPlaying)
        {
            _leftEngineParcticles.Play();
        }
        ApplyRotation(-rotationThrust);
    }

    private void RotateStop()
    {
        _rightEngineParcticles.Stop();
        _leftEngineParcticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }
}
