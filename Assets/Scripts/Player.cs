using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Color thirdColor;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletSpread;
    [SerializeField] private int bulletNbr;
    
    [SerializeField] private float fireBurst;
    [SerializeField] private Rigidbody2D playerRb;

    public UnityEvent ShootEvent;

    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        CheckShoot();
        LookAtMouse();

        //Shoot();

    }

    private void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.DrawLine(transform.position, mousePos, color: Color.red);


        Vector2 Direction = (mousePos - (Vector2)transform.position).normalized;

        float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, Angle + 90);
    }

    public void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootEvent.Invoke(); 
            for (int i = 0; i < bulletNbr; i++)
            {
                Shoot();
            }

        }
    }

    public void Shoot()
    {
        ScreenShake.Shake(0.1f, 1);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        SpriteRenderer bulletSr = bullet.GetComponent<SpriteRenderer>();


        Color color = new Color(Random.Range(0.8f,1), thirdColor.g, thirdColor.b);

        bulletSr.color = color;


        float randomSpread = Random.Range(-bulletSpread, bulletSpread);

        Vector2 bulletDir = Quaternion.Euler(0, 0, randomSpread) * -firePoint.up;

        float randomSpeed = Random.Range(bulletSpeed-1, bulletSpeed+1);

        bulletRb.AddForce(bulletDir * randomSpeed, ForceMode2D.Impulse);

        playerRb.AddForce(firePoint.up * fireBurst, ForceMode2D.Impulse);
        Debug.Log("Pew Pew");
    }
}
