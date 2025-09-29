    using UnityEngine;
    using UnityEngine.InputSystem;

public class Playercs : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 screenBoundery;

    [SerializeField] int playerHealth = 5;
    [SerializeField] float invinsibleTime = 3f;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 700f;
    [SerializeField] float bulletSpeed = 9f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;

    bool invinsible;
    float targetAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBoundery = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }


    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
        Rigidbody2D playerBullet = Instantiate(bullet, gun.transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        playerBullet.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }


    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput != Vector2.zero)
        {
            targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBoundery.x, screenBoundery.x)
                                        , Mathf.Clamp(transform.position.y, -screenBoundery.y, screenBoundery.y));
    }

    void FixedUpdate()
    {
        float rotation = Mathf.MoveTowardsAngle(rb.rotation, targetAngle - 90, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rotation);
    }

    void ResetInvinsibility()
    {
        invinsible = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies") && !invinsible)
        {
            if (playerHealth <= 1)
            {
                Destroy(gameObject);
            }
            else
            {
                playerHealth--;
                invinsible = true;
                Invoke("ResetInvinsibility", invinsibleTime);
                Debug.Log("Player health:" + playerHealth);
            }
        }
    }
}
