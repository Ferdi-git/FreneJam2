using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] float flashDuration = 0.1f;
    [SerializeField] Color flashColor = Color.white;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void FlashF()
    {
        StartCoroutine(FlashCo());
    }

    IEnumerator FlashCo()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.black;
    }
}
