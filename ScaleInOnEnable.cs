using UnityEngine;

public class ScaleInOnEnable : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 startSize = new();
    private void OnEnable()
    {
        transform.localScale = startSize;
        LeanTween.scale(gameObject, Vector3.one, speed).setEaseOutBack();
    }
}
