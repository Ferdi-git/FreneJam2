using DG.Tweening;
using System.Collections;
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
    public float health = 100; 
    public Vector2 dirTowardPlayer;
    public bool isBlinking = false;
    public TrailRenderer lineRenderer;
    public GameObject deathParticule;

    private void Start()
    {
        player = Player.Instance.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void Initialise(SOEnemy so)
    {
        speed = so.speed;
        health = so.life;
        spriteRenderer.sprite = so.sprite;
        transform.localScale = new Vector2(so.scale, so.scale);
    }



    private void Update()
    {
        dirTowardPlayer = (player.transform.position - transform.position ).normalized;

        rb.linearVelocity += dirTowardPlayer * speed * Time.deltaTime;

        float distFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (!isBlinking)
        {

        float newBrightness = Mathf.Clamp01(1 - (distFromPlayer / fullBrightnessCloseness));

        spriteRenderer.color = new Color(newBrightness, newBrightness, newBrightness);
        }
    }


    public void Hit()
    {
        lineRenderer.enabled = true;
        health -= 10;
        rb.AddForce( -dirTowardPlayer * speed*5 , ForceMode2D.Impulse);
        Blinking();
        //Destroy(gameObject);
    }

    private void Blinking()
    {
        isBlinking = true;
        for (int i = 0; i < 5; i++)
        {
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(spriteRenderer.DOColor(new Color(1, 1, 1, 10), 1));
            sequence.Append(spriteRenderer.DOColor(new Color(1, 1, 1, 10), 0.2f));
            sequence.Append(spriteRenderer.DOColor(new Color(1, 1, 1, 1), 1));
            sequence.Append(spriteRenderer.DOColor(new Color(1, 1, 1, 1), 0.2f));


            if ( i == 2 && health <= 0)
            {
                sequence.Append(spriteRenderer.DOColor(player.GetComponent<Player>().thirdColor, 1));
                sequence.Append(spriteRenderer.DOColor(player.GetComponent<Player>().thirdColor, 0.2f).OnComplete(() =>
                {
                    var goParti = Instantiate(deathParticule, transform.position, Quaternion.identity);
                    var ps = goParti.GetComponent<ParticleSystem>();
                    ParticleSystem.MainModule main = ps.main;
                    main.startColor = player.GetComponent<Player>().thirdColor;
                    ps.Play();
                    Destroy(gameObject);

                }));
                
            }
            sequence.Play();
        }
        lineRenderer.enabled = false;
        isBlinking = false;

    }


}
