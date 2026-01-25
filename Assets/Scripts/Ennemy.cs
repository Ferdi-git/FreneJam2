using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
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
    public bool isDead = false;
    public TrailRenderer lineRenderer;
    public GameObject deathParticule;
    private SOEnemy soEnemy;

    private void Start()
    {
        player = Player.Instance.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void Initialise(SOEnemy so)
    {
        soEnemy = so;
        speed = so.speed;
        health = so.life;
        fullBrightnessCloseness = so.brightnessCloseness;
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
        if (health <= 0)
        {
            lineRenderer.startColor = player.GetComponent<Player>().thirdColor;
            lineRenderer.endColor = player.GetComponent<Player>().thirdColor;
        }
        rb.AddForce( -dirTowardPlayer * speed*5 , ForceMode2D.Impulse);
        if(!isDead)
            Blinking();
        //Destroy(gameObject);
    }

    private void Blinking()
    {
        isBlinking = true;
        DG.Tweening.Sequence sequence = DOTween.Sequence();

        Color blinkColor = Color.white;


        if (health <= 0)
        {
 
            blinkColor = player.GetComponent<Player>().thirdColor;
        }


        for (int i = 0; i < 5; i++)
        {
            sequence.Append(spriteRenderer.DOColor(blinkColor, 0.2f));
            sequence.Append(spriteRenderer.DOColor(new Color(1, 1, 1, 1), 0.2f));


            if ( i == 2 && health <= 0)
            {
                sequence.Append(spriteRenderer.DOColor(blinkColor, 0.5f).OnComplete(() =>
                {
                    Death();
                }));

                break;
                
            }

        }
        sequence.OnComplete(() =>
        {
            lineRenderer.enabled = false;
            isBlinking = false;
        });

        sequence.Play(); 

    }


    public void Death()
    {
        if (isDead) return;

        isDead = true;
        var goParti = Instantiate(deathParticule, transform.position, Quaternion.identity);
        var ps = goParti.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = ps.main;
        main.startColor = player.GetComponent<Player>().thirdColor;
        ps.Play();
        UIManager.Instance.AddToScore(soEnemy.points); 
        Destroy(gameObject);
    }

}
