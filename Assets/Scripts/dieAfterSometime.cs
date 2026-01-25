using UnityEngine;

public class dieAfterSometime : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2);
    }
}
