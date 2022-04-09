using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    private int _teamId;
    private bool _isUse;
    private void FixedUpdate()
    {
        if (_isUse) transform.position += transform.forward * speed * Time.deltaTime;
    }
    public void Init(int bulletTeamId)
    {
        _teamId = bulletTeamId;
        _isUse = true;
        transform.SetParent(null);
        gameObject.SetActive(true);
    }
    public void BulletDestroy()
    {
        _isUse = false;
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        BaseTank tank = other.GetComponent<BaseTank>();
        if (tank)
        {
            if (tank.TeamId != _teamId)
            {
                tank.Damage(damage);
                BulletDestroy();
            }

        }
        else if (other.GetComponent<Wall>())
        {
            BulletDestroy();
        }
    }
    public bool IsUse => _isUse;
}
