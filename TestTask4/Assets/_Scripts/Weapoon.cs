using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapoon : MonoBehaviour
{
    [SerializeField] private Bullet bulletExaple;
    [SerializeField] private List<Bullet> bullets;
    [SerializeField] private Transform pointForSpawnBullet;
    [SerializeField] private float delay = 0.5f;
    private int _teamId;
    private bool _fire = false;
    private bool _inited = false;
    public void Init(int teamId)
    {

        if (_inited) return;
        _teamId = teamId;
        _inited = true;
        StartCoroutine(Shot());
    }
    public void Dead()
    {
        _inited = false;
        FireStop();
        StopCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            if (_fire) SpawnBullet();
            yield return new WaitForSeconds(delay);
        }
    }
    public void FireStart()
    {
        _fire = true;
    }
    public void FireStop()
    {
        _fire = false;
    }
    private void SpawnBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i])
            {
                bullets.RemoveAt(i);
                i--;
                continue;
            }
            else if (bullets[i].IsUse)
            {
                continue;
            }
            bullets[i].Init(_teamId);
            SetPosition(bullets[i].transform);
            return;
        }
        bullets.Add(Instantiate(bulletExaple, pointForSpawnBullet));
        bullets[bullets.Count - 1].Init(_teamId);
        SetPosition(bullets[bullets.Count - 1].transform);
    }
    private void SetPosition(Transform objectForSpawn)
    {
        objectForSpawn.position = pointForSpawnBullet.position;
        objectForSpawn.rotation = pointForSpawnBullet.rotation;
    }
    public bool IsFire => _fire;
}
