using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanBullet : Bullet
{
    private LineRenderer lr;

    public Light light;
    public float flashBrightness;
    public AnimationCurve flashFalloff;
    private float flashTimer;

    public LayerMask mask;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        light.intensity = flashBrightness;
        flashTimer = Time.time;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore))
        {
            IDamageable damageTarget = hit.collider.gameObject.GetComponent<IDamageable>();

            if (damageTarget != null)
            {
                damageTarget.TakeDamage(damage);

                Collider[] colliders = Physics.OverlapSphere(hit.point, 1f);

                foreach (Collider colHit in colliders)
                {
                    Rigidbody rb = colHit.GetComponent<Rigidbody>();

                    if(rb != null)
                    {
                        rb.AddExplosionForce(500f, hit.point, 1f);
                    }
                }
            }

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);

            Destroy(gameObject, 0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetColors(Color.Lerp(lr.startColor, Color.clear, Time.deltaTime * 10f), Color.Lerp(lr.endColor, Color.clear, Time.deltaTime * 10f));

        //light.intensity = Mathf.Lerp(light.intensity, 0f, Time.deltaTime * 10f);

        if (flashTimer < 0)
        {
            return;
        }
        float curveTime = (Time.time - flashTimer) / 0.2f;
        if (curveTime > 1)
        {
            flashTimer = -1;
        }
        else
        {
            light.intensity = flashFalloff.Evaluate(curveTime);
        }
    }
}
