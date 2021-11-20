using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanBullet : Bullet
{
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            IDamageable<float> damageTarget = hit.collider.gameObject.GetComponent<IDamageable<float>>();

            if (damageTarget != null)
            {
                damageTarget.TakeDamage(damage);
            }

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);

            

            Destroy(gameObject, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetColors(Color.Lerp(lr.startColor, Color.clear, Time.deltaTime * 10f), Color.Lerp(lr.endColor, Color.clear, Time.deltaTime * 10f));
    }
}
