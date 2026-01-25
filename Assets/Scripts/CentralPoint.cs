using UnityEngine;

public class CentralPoint : MonoBehaviour
{
    SpriteRenderer sp;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
         sp.color = Player.Instance.thirdColor;
    }
}
