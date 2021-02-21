using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    //Projectile references
    private Rigidbody projectileRigidbody;
    public GameObject centerOfMass;
    //Projectile features
    public float velocity = 20f;
    public float despawnTime = 3f;
    public float destroyTime = 1f;

    private void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DespawnProjectile", despawnTime);
    }

    void DespawnProjectile()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, destroyTime);
    }

    public void SpawnAndLauchProjectile(Camera mainCamera)
    {
        // Remove parent to prevent rotate with the user
        transform.SetParent(null);
        // Changes velocity
        projectileRigidbody.velocity = mainCamera.transform.forward * velocity;
        // Rotates toward target
        transform.LookAt(transform.position + projectileRigidbody.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("This projectile hits: " + other.ToString());
    }
}
