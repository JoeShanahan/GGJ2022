using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    [SerializeField] VillagerMove _movement;
    [SerializeField] VillagerAnimations _animation;
    [SerializeField] GameObject _gibsPrefab;
    [SerializeField] Material[] _materials;
    [SerializeField] SkinnedMeshRenderer[] _meshes;

    public int currentHealth;
    public int maxHealth;

    private int _materialIndex;
    private bool _hasDied = false;

    // Start is called before the first frame update
    void Start()
    {
        bool hasSkirt = Random.Range(0, 2) == 1;
        _meshes[1].gameObject.SetActive(hasSkirt);

        _materialIndex = Random.Range(0, _materials.Length);

        foreach (var mesh in _meshes)
            mesh.material = _materials[_materialIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExplosiveDamage(Vector3 position, int damage, float force, float radius)
    {
        float actualDamage = damage;
        float distance = Vector3.Distance(position, transform.position);

        if (distance > radius)
            return;

        float distPercent = Mathf.Clamp01(distance / radius);
        actualDamage *= Mathf.Pow(1 - distPercent, 0.5f);

        currentHealth -= Mathf.RoundToInt(actualDamage);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
            ExplosiveKill(position, force, radius);
    }

    public void Damage(Vector3 position, int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0)
            Kill(position);
    }

    public void ExplosiveKill(Vector3 position, float force, float radius)
    {
        GameObject gibsParent = Instantiate(_gibsPrefab, transform.position, transform.rotation);

        foreach (MeshRenderer rend in gibsParent.GetComponentsInChildren<MeshRenderer>())
            rend.material = _materials[_materialIndex];

        foreach (Rigidbody rb in gibsParent.GetComponentsInChildren<Rigidbody>())
            rb.AddExplosionForce(force, position, radius);
        
        Destroy(gameObject);
    }

    public void Kill(Vector3 position)
    {
        if (_hasDied)
            return;

        _hasDied = true;

        Vector3 direction = (position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, direction);

        bool isFacingKiller = dot > 0;

        if (isFacingKiller)
            _animation.DieBackwards();
        else
            _animation.DieForwards();

        GetComponent<Collider>().enabled = false;
        GetComponent<VillagerMove>().enabled = false;
        Destroy(GetComponent<Rigidbody>());

    }
}
