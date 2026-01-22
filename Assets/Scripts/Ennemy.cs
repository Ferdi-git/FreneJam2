using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Ennemy : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public float fullBrightnessCloseness = 40f;
    public GameObject player;
    public float speed;
    public Vector2 dirTowardPlayer;

    private void Start()
    {
        player = Player.Instance.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        dirTowardPlayer = (player.transform.position - transform.position ).normalized;

        rb.linearVelocity += dirTowardPlayer * speed * Time.deltaTime;

        float distFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, fullBrightnessCloseness/distFromPlayer);
    }


    public void Hit()
    {

        rb.AddForce( -dirTowardPlayer * speed*5 , ForceMode2D.Impulse);
        //Destroy(gameObject);
    }
}
