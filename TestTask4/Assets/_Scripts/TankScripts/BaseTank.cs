using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTank : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private int teamId;
    private int _defaultHp;
    [SerializeField] protected Weapoon weapoon;
    [SerializeField] private Transform tankBody;
    [SerializeField] protected float speed;
    private Transform _spawnPosition;
    [SerializeField] private BaseNavigation navigation;
    private void Awake()
    {
        _defaultHp = hp;
        TankInit();
    }
    public void SetRespawnPosition(Transform position)
    {
        _spawnPosition = position;
    }
    public virtual void TankInit()
    {
        navigation.Init();
        weapoon.Init(teamId);
    }
    private void FixedUpdate()
    {
        TankUpdate();
    }
    protected virtual void TankUpdate()
    {
        Move();
    }
    protected virtual void Move()
    {
        transform.position += navigation.GetDirection() * speed * Time.deltaTime;
        tankBody.LookAt(tankBody.position + navigation.GetDirection());
    }
    protected void Shot()
    {
        weapoon.FireStart();
    }
    private void StopShot()
    {
        weapoon.FireStop();
    }
    public virtual void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Dead();
        }
    }
    protected virtual void Dead()
    {
        gameObject.SetActive(false);
        weapoon.Dead();
        navigation.NavigationOff();
        Invoke(nameof(Respawn), 1);
    }
    public virtual void Respawn()
    {
        hp = _defaultHp;
        gameObject.SetActive(true);
        transform.position = _spawnPosition.position;
        TankInit();
    }
    public int TeamId => teamId;
}
