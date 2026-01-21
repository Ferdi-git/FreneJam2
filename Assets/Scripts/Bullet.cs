using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemmy"))
        {
            Ennemy scriptE = collision.GetComponent<Ennemy>();
            scriptE.Hit();
            Destroy(gameObject);
        }
    }

}
