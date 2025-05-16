using UnityEngine;
using UnityEngine.WSA;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 initialSpeed;
    [SerializeField] private float velocityMultiplier = 1.05f;
    private Rigidbody2D rb;
    private bool isMoving = false; // Para evitar que la bola se mueva antes de que el jugador la lance
    private Vector2 startPos;

    [SerializeField] private GameObject lifePowerUpPrefab; // Referencia al prefab del powerUP

    private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound, brickSound, loseLifeSound; // This is set in the inspector


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Launch();
    }

    private void Launch()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            isMoving = true;
            transform.parent = null;
            rb.linearVelocity = initialSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("block"))
        {
            Destroy(collision.gameObject);
            rb.linearVelocity *= velocityMultiplier; // Aumentamos la velocidad de la bola cada vez que destruye un bloque
            GameManager.Instance.BlockDestroyed();

            audioSource.clip = brickSound;
            audioSource.Play();

            //Definimos un aleatorio entre 0 y 1
            float random = Random.value;
            // Si el aleatorio es menor que, por ejemplo, 0.2 (20% de posibilidad) de generar un powerUp
            if (random < 0.2f) Instantiate(lifePowerUpPrefab, collision.transform.position, Quaternion.identity);
        } else
        {

            audioSource.clip = hitSound;
            audioSource.Play();
        }
            VelocityFix();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("derrota"))
        {
            Debug.Log(":(");
            GameManager.Instance.LoseLife();
            audioSource.clip = loseLifeSound;
            audioSource.Play();
        }
    }

    private void VelocityFix()
    {
        float velocidadDelta = 0.5f; // Velocidad que queremos que aumente la bola
        float velocidadMinima = 0.2f; // Velocidad mínima que queremos que tenga la bola

        if (Mathf.Abs(rb.linearVelocityX) < velocidadMinima) // Si la velocidad de la bola en el eje x es menor que la mínima
        {
            velocidadDelta = Random.value < 0.5f ? velocidadDelta : -velocidadDelta; // Elegimos un valor aleatorio entre -0.5 y 0.5
            rb.linearVelocity = new Vector2(rb.linearVelocityX + velocidadDelta, rb.linearVelocityY); // Aumentamos la velocidad de la bola
        }

        if (Mathf.Abs(rb.linearVelocityY) < velocidadMinima) // Si la velocidad de la bola en el eje y es menor que la mínima
        {
            velocidadDelta = Random.value < 0.5f ? velocidadDelta : -velocidadDelta; // Elegimos un valor aleatorio entre -0.5 y 0.5
            // Otra forma de aumentar la velocidad (esta vez en el eje y)
            rb.linearVelocity += new Vector2(0f, velocidadDelta); // Aumentamos la velocidad de la bola
        }
    }

    public void ResetBall()
    {
        isMoving = false;
        rb.linearVelocity = Vector3.zero;
        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = startPos;
    }

}
