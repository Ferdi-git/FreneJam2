using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Ennemy : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public float alphaSpeed = 0.5f;
    public GameObject player;
    public float speed;


    private void Start()
    {
        player = Player.Instance.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, speed * Time.deltaTime);        

        //spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + alphaSpeed * Time.deltaTime);
    }


    public void Hit()
    {
        //transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, -speed);

        rb.AddForce( -transform.forward * speed*5 , ForceMode2D.Impulse);
        //Destroy(gameObject);
    }
}
