using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] TilePrefabs;
    private List<GameObject> _activeTiles = new List<GameObject>();
    private float _spawnPos = 0;
    private float _tileLenght = 81;

    [SerializeField] private Transform _player;
    private int _startTiles = 6;

    void Start()
    {
        for (int i = 0; i < _startTiles; i++)
        {
            if (i == 0)
                SpawnTile(3);
            SpawnTile(Random.Range(0, TilePrefabs.Length));
        }
    }

    void Update()
    {
        if (_player.position.z - 60> _spawnPos - (_startTiles * _tileLenght))
        {
            SpawnTile(Random.Range(0, TilePrefabs.Length));
            DeleteTile();
        }        
    }
    
    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(TilePrefabs[tileIndex], transform.forward * _spawnPos, transform.rotation);
        _activeTiles.Add(nextTile);
        _spawnPos += _tileLenght;
    }

    private void DeleteTile()
    {
        Destroy(_activeTiles[0]);
        _activeTiles.RemoveAt(0);
    }
}
