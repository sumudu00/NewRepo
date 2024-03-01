using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    [SerializeField] float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;//
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

        if(other.CompareTag("enemy"))
        {
            Destroy(gameObject);
            other.GetComponent<EnemyBehavior>().OnDeath();
        }
    }
    
}
