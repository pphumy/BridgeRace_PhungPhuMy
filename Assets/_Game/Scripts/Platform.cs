using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int rows = 10;
    public int columns = 12;
    public Transform brickSpawnPos;
    public float spacing = 4.0f;
    public Vector3[] listSpawnCharacterPos;
    public Brick brickPrefab;
    // Start is called before the first frame update


    void Start()
    {
        SpawnListBrick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnListBrick()
    {
        for(int i=0; i<rows; i++)
        {
            for(int k=0; k < columns; k++)
            {
                Vector3 position = brickSpawnPos.position + new Vector3(i * spacing, 0, k * spacing);
                Brick brick = SimplePool.Spawn<Brick>(brickPrefab, position, Quaternion.identity);
            }
        }
    }
}
