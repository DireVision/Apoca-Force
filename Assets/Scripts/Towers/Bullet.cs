using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    [HideInInspector] public int damageToDeal;
    public float speed = 10;

    public void Seek(Transform __target, int damage)
    {
        target = __target;
        damageToDeal = damage;
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
}
