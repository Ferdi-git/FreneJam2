using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    public Color thirdColor;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletSpread;
    [SerializeField] private int bulletNbr;
    
    [SerializeField] private float fireBurst;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private SpriteRenderer playerSP;
    [SerializeField] private Sprite originalSprite;
    [SerializeField] private Sprite damagedSprite;

    private bool canDie = false;
    public bool canShoot = true;
    public UnityEvent ShootEvent;

    private void Start()
    {
        playerSP.sprite = originalSprite;
        Instance = this;
        sp.color = thirdColor;
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
        if (Input.GetMouseButtonDown(0) && canShoot)
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


        Color color = thirdColor;

        bulletSr.color = color;


        float randomSpread = Random.Range(-bulletSpread, bulletSpread);

        Vector2 bulletDir = Quaternion.Euler(0, 0, randomSpread) * -firePoint.up;

        float randomSpeed = Random.Range(bulletSpeed-1, bulletSpeed+1);

        bulletRb.AddForce(bulletDir * randomSpeed, ForceMode2D.Impulse);

        playerRb.AddForce(firePoint.up * fireBurst, ForceMode2D.Impulse);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemmy"))
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().AddForce(dir * 12, ForceMode2D.Impulse);

            if (canDie)
            {
                Time.timeScale = 0.0f;
                UIManager.Instance.Lost();
            }
            else
            {
                StartCoroutine(CanDie());
            }

        }
    }

    private IEnumerator CanDie()
    {
        playerSP.sprite = damagedSprite;
        canDie = true;

        yield return new WaitForSeconds(7);
        
        canDie = false;
        playerSP.sprite = originalSprite;
    }


}
