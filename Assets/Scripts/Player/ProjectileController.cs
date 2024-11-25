using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private float projectileLifeTime = 5f;
    
    public float speed = 50.0f;
        
    private void Update()
    {
        ProjectileSpawner();
    }
    
    private void ProjectileSpawner()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = ObjectPoolManager.Instance.GetObject("Projectile");
            projectile.transform.position = spawnPoint.position;
            
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.velocity = spawnPoint.forward * speed;
            StartCoroutine(RecycleProjectile(projectile));
        }
    }
        
    private IEnumerator RecycleProjectile(GameObject projectile)
    {
        yield return new WaitForSeconds(projectileLifeTime);
        ObjectPoolManager.Instance.ReturnObject("Projectile", projectile);
    }
}
