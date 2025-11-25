using System.Collections;
using UnityEngine;

public class DamageShake : MonoBehaviour
{
    public CharacterVitals parent;
    public bool useDamagePercentege = true;
    public float distance = 1f;
    public float duration = 0.25f;
    public float instancesPerSecond = 60f;
    Vector3 origin;
    private void Awake()
    {
        origin = transform.localPosition;
    }

    private void Start()
    {
        if (parent) parent.OnHit += Shake;
    }

    public void Shake(float magnitude)
    {
        magnitude /= 100f;

        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(ShakeAnimation(magnitude * distance, magnitude * duration));
    }
    Coroutine routine;
    IEnumerator ShakeAnimation(float mag, float dur)
    {
        Debug.Log("Start Shake : Mag = " + mag + " dur: " + dur);
        transform.localPosition = origin;

        while (dur > 0)
        {
            Vector3 roll = GetRandomDistance(mag);
            transform.localPosition = origin + roll;

            yield return new WaitForSeconds(1 / instancesPerSecond);
            dur -= 0.05f;
        }

        transform.localPosition = origin;
    }
    Vector3 GetRandomDistance(float magnitude)
    {
        return new Vector3(GetRandomRoll(magnitude), GetRandomRoll(magnitude), GetRandomRoll(magnitude));
    }
    float GetRandomRoll(float magnitude)
    {
        return Random.Range(-magnitude, magnitude);
    }
}
