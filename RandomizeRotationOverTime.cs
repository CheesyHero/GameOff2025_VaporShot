using UnityEngine;

public class RandomizeRotationOverTime : MonoBehaviour
{
    public Vector2 interval = new(1f, 3f);
    float timer = 0;

    public Vector3 minRotValues = new(360, 360, 360);
    public Vector3 maxRotValues = new(-360, -360, -360);
    Vector3 currentTorque = new();
    Vector3 targetTorque = new();
    Vector3 smoothing = new();

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            RandomizeTorque();
            timer = Random.Range(interval.x, interval.y);
        }

        currentTorque = Vector3.SmoothDamp(currentTorque, targetTorque, ref smoothing, 1f);
        transform.Rotate(currentTorque * Time.deltaTime);
    }

    public void RandomizeTorque()
    {
        targetTorque = GetRandamizedTorqueValue();
    }
    public Vector3 GetRandamizedTorqueValue()
    {
        return new Vector3(
            GetRandomRotValue(minRotValues.x, maxRotValues.x),
            GetRandomRotValue(minRotValues.y, maxRotValues.y),
            GetRandomRotValue(minRotValues.z, maxRotValues.z));
    }
    public float GetRandomRotValue(float min, float max)
    {
        return Random.Range(min, max);
    }
}
