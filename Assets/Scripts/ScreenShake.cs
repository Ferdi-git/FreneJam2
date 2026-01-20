using DG.Tweening;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;
    private Vector3 originalPos;
    private Quaternion originalRot;

    private void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }

    private void OnShake(float duration, float strength)
    {
        transform.DOKill(); // Kill any existing shakes

        transform.DOShakePosition(duration, strength)
            .OnComplete(() => transform.localPosition = originalPos);

        transform.DOShakeRotation(duration, strength)
            .OnComplete(() => transform.localRotation = originalRot);
    }

    public static void Shake(float duration, float strength) => Instance.OnShake(duration, strength);
}