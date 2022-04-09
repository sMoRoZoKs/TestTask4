using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NavigationDirection
{
    public Vector3 Direction;
    [HideInInspector] public bool Hit;
}
public class Detector : BaseNavigation
{
    // сделать добавление направлений для проверки рейкастом,
    // и ивент которы будет обновлять события в ии в случае если тстанет доступным для езды новое тнаправление, 
    // и рандоимно выбирать 1 из них  
    [SerializeField] private List<NavigationDirection> directions;
    private List<bool> _lastDirectionHits = new List<bool>();
    [SerializeField] private float timeForChangeDirection = 0.5f;
    [SerializeField] private float lookRadius;
    private Vector3 _lastActiveDirection = Vector3.zero;
    private RaycastHit _hit;
    public override void Init()
    {
        StartCoroutine(UpdateLastDirection());
    }
    public override void NavigationOff()
    {
        StopCoroutine(UpdateLastDirection());
    }
    private void FixedUpdate()
    {
        UpdateDirections();
    }
    private IEnumerator UpdateLastDirection()
    {
        while (true)
        {
            ChangeLastDirection();
            yield return new WaitForSeconds(timeForChangeDirection);
        }
    }
    private void ChangeLastDirection()
    {
        List<Vector3> activeDirections = new List<Vector3>();
        for (int i = 0; i < directions.Count; i++)
        {
            if (!directions[i].Hit) activeDirections.Add(directions[i].Direction);
        }
        if (activeDirections.Count > 0) _lastActiveDirection = activeDirections[Random.Range(0, activeDirections.Count)];
        else _lastActiveDirection = Vector3.zero;
    }
    private void UpdateDirections()
    {
        for (int i = 0; i < directions.Count; i++)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(directions[i].Direction), out _hit, lookRadius))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(directions[i].Direction) * _hit.distance, Color.red);
                directions[i].Hit = true;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(directions[i].Direction) * lookRadius, Color.blue);
                directions[i].Hit = false;
            }
        }
        if (_lastDirectionHits.Count != directions.Count)
        {
            ChangeLastDirectionHits();
        }
        int countLastHit = 0;
        int countHit = 0;

        for (int i = 0; i < _lastDirectionHits.Count; i++)
        {

            if (directions[i].Hit) countHit++;
            if (_lastDirectionHits[i]) countLastHit++;

        }
        if (countHit != countLastHit)
        {
            ChangeLastDirection();
            ChangeLastDirectionHits();
        }
    }
    private void ChangeLastDirectionHits()
    {
        _lastDirectionHits.Clear();
        for (int i = 0; i < directions.Count; i++)
            _lastDirectionHits.Add(directions[i].Hit);
    }
    public override Vector3 GetDirection()
    {
        return new Vector3(_lastActiveDirection.x,_lastActiveDirection.y,_lastActiveDirection.z);
    }
}
