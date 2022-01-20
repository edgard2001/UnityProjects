using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private List<Transform> _path;
    [SerializeField] private GameObject _enemyPrefab;

    public bool IsSpawning = false;
    public int NumEnemiesPerWave = 1;
    public float SpawnRatePerSecond = 0.1f;

    public void InitiateSpawning()
    {
        if (IsSpawning) return;
        if (_path.Count == 0) return;
        StartCoroutine(SpawnWave());
        IsSpawning = true;
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < NumEnemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f / SpawnRatePerSecond);
        }
        IsSpawning = false;
    }

    private void SpawnEnemy()
    {
        GameObject.Instantiate(_enemyPrefab, _path[0].position, Quaternion.identity);
    }

    public Vector3 GetNextWaypoint(Vector3 currentWaypoint)
    {
        int positionIndex = _path.FindIndex(x => (x.position - currentWaypoint).sqrMagnitude < 0.001f);
        if (positionIndex == (_path.Count - 1)) return currentWaypoint;
        return _path[positionIndex + 1].position;
    }

}
