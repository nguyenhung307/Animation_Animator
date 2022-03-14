using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstance : MonoBehaviour
{
    [SerializeField] List<GameObject> obstance = new List<GameObject>();

    [SerializeField] private float _maxX;
    [SerializeField] private float _maxZ;

    private float offset;

    private float _count = 500;
    private Vector3 pos = Vector3.zero;
    private void Start() {
        SpawnPosition();
    }

    void SpawnPosition(){
        List<Vector2> listPos = new List<Vector2>();
        for(float x = -_maxX; x < _maxX ; x++){
            for(float z = - _maxZ ; z < _maxZ; z++){
                Vector2 add = new Vector2(x * 10 , z);
                listPos.Add(add);
            }
        }
    }
    void SpawnObs(Vector3 posSpawn){
        while(_count >0){
        int index = Random.Range(0, obstance.Count);
        GameObject clone = Instantiate(obstance[index],posSpawn, Quaternion.identity);
        _count--;
        }
    }
}
