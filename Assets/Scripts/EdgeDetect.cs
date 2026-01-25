using System.Collections;
using UnityEngine;

public class EdgeDetect : MonoBehaviour
{
    public bool isAnEnemy = false;
    private void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);


        Vector3 moveAdjustment = Vector3.zero;
        if (viewportPosition.x < 0.1f)
        {
            if(isAnEnemy) StartCoroutine(coLinerender());

            moveAdjustment.x += 0.8f;
        }
        else if (viewportPosition.x > 0.9f)
        {
            if (isAnEnemy) StartCoroutine(coLinerender());

            moveAdjustment.x -= 0.8f;
           
        }
        else if (viewportPosition.y < 0.1f)
        {
            if (isAnEnemy) StartCoroutine(coLinerender());

            moveAdjustment.y += 0.8f;
            
        }
        else if (viewportPosition.y > 0.9f)
        {
            if (isAnEnemy) StartCoroutine(coLinerender());

            moveAdjustment.y -= 0.8f;

        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moveAdjustment);
    }

    private IEnumerator coLinerender()
    {
        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();


        trailRenderer.time = 0;
        yield return new WaitForSeconds(0.01f);
        trailRenderer.time = 0.5f;

    }
}
