using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int rows = 8;
    public int columns = 5;
    public Transform brickSpawnPos;
    public float spacing = 5.0f;
    public Brick brickPrefab;


    public List<Brick> bricks = new List<Brick>();
    private List<Vector3> emptyPos = new List<Vector3>();

    void Start()
    {
        SpawnListPosBrick();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnListPosBrick()
    {
        for(int i=0; i<rows; i++)
        {
            for(int k=0; k < columns; k++)
            {
                Vector3 position = brickSpawnPos.position + new Vector3(i * spacing, 0, k * spacing);
                //Brick brick = SimplePool.Spawn<Brick>(brickPrefab, position, Quaternion.identity);
                //List position to spawn brick
                //Instantiate(brickPrefab, position, Quaternion.identity);
                emptyPos.Add(position);
                Debug.Log("spawn pos");
            }
        }
    }

    //Spawn brick with colorType
    private void SpawnBrick(ColorType color)
    {
            // Spawn bricks equally for 4 characters
            for (int i = 0; i < (rows*columns) / 4; i++)
            {
            Debug.Log("paint");
            Vector3 position = emptyPos[Random.Range(0, emptyPos.Count)];
            Brick brick = SimplePool.Spawn<Brick>(brickPrefab, position, Quaternion.identity);
            brick.ChangeColor(color);
            bricks.Add(brick);
            emptyPos.Remove(position);
            }
    }
 

    //Get list Brick of ColorType
    public List<Vector3> GetBrickPoint(ColorType colorType)
    {
        List<Vector3> list = new List<Vector3>();
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                list.Add(bricks[i].TF.position);
            }
        }

        return list;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_CHARACTER))
        {
            Debug.Log("collision");
            SpawnBrick(other.gameObject.GetComponent<Character>().colorType);
            other.gameObject.GetComponent<Character>().platform = this;
        }
    }
}
