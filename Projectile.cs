using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    public Weapon parent;
    public ParticleSystem impactFX;
    public float damage = 25f;
    public float speed;
    public float gravity;
    public float lifetime = 1f;
    public float areaOfEffect = 1f; 
    Collider col;
    Rigidbody rb;

    private void Awake()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    } 
    private void Update()
    {  
        if (lifetime > 0f) lifetime -= Time.deltaTime;
        else Kill();

        DownwardForce();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != gameObject.layer)
            Kill();
    }

    private void DownwardForce()
    {
        if (gravity == 0) return;

        Vector3 t = rb.angularVelocity;
        t.y -= gravity * Time.deltaTime;
        rb.angularVelocity -= t;
    }
    public void Orient(Weapon input, Vector3 hitPoint = new())
    {
        parent = input;
        damage = Mathf.Round(input.damage * Random.Range(0.9f, 1.1f)); 
        gameObject.layer = input.gameObject.layer;
        speed = input.projectileSpeed;
        lifetime = input.projectileRange / input.projectileSpeed;

        if (hitPoint.magnitude > 0.1f)
            transform.LookAt(hitPoint); 

        rb.AddForce(input.shootPoint.forward * speed + ProjectileTilt(input), ForceMode.Impulse);
    }
    public Vector3 ProjectileTilt(Weapon input)
    {
        float f = input.accuracyMod;

        // random horizontal + vertical spread in world space
        float x = Random.Range(-f, f);
        float y = Random.Range(-f, f);

        // convert to world-space directional offset
        return (transform.right * x) + (transform.up * y);
    }

    public void Stop()
    {
        col.enabled = false;
    }
    public void Kill()
    {
        GetAreaHits();
        PlayEffect();
        Destroy(gameObject);
    }
    public void PlayEffect()
    {
        if(impactFX) Instantiate(impactFX, transform.position, transform.rotation);
    }
    public void GetAreaHits()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, areaOfEffect);
        foreach(var item in hits)
        {
            if (item.transform.TryGetComponent<CharacterVitals>(out var health))
            {
                Debug.LogWarning("HIT: " + health.name);
                health.TakeDamage(damage);
            }
        }
    }
}
