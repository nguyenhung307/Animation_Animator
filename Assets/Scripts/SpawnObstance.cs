using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstance : MonoBehaviour
{
    [SerializeField] List<GameObject> obstance = new List<GameObject>();

    [SerializeField] private float _maxX;
    [SerializeField] private float _maxZ;

    public List<Vector3> listPos = new List<Vector3>();

    private float offset;

    private float _count = 1000;
    private Vector3 pos = Vector3.zero;
    private void Start() {
        SpawnPosition();
    }
    private void Update() {
       SpawnObs(pos);
    }
    void SpawnPosition(){
        for(float x = -_maxX; x < _maxX ; x+=10){
            for(float z = - _maxZ ; z < _maxZ; z+=10){
                Vector3 add = new Vector3(x , 0 ,z );
                listPos.Add(add);
            }
        }
    }
    void SpawnObs(Vector3 pos){

        while(_count >0){
        int index = Random.Range(0, obstance.Count);
        GameObject clone = Instantiate(obstance[index],listPos[Random.Range(0, listPos.Count)], Quaternion.identity);
        listPos.Remove(pos);
        _count--;
        }
    }
}
