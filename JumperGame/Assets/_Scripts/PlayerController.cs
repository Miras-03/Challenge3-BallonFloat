using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [Space(10)]
    [Header("Particles")]
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem fireworksParticle;

    [Space(20)]
    [Header("AudioClips")]
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip moneySound;

    private AudioSource audioPlayer;
    private Rigidbody rb;
    private Death death;

    private const int floatForce = 50;
    private bool isGameOver = false;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        death = Death.Instance;
    }

    private void Update() => CheckOrFly();

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (!isGameOver)
        {
            if (other.CompareTag("Bomb"))
            {
                isGameOver = true;
                explosionParticle.Play();
                audioPlayer.PlayOneShot(crashSound, 0.7f);
                death.NotifyObservers();
                Destroy(collision.gameObject);
            }
            else if (other.CompareTag("Money"))
            {
                fireworksParticle.Play();
                audioPlayer.PlayOneShot(moneySound, 1);
                Destroy(collision.gameObject);
            }
        }
    }

    private void CheckOrFly()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGameOver)
            rb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
    }
}