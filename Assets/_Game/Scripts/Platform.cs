using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int rows = 8;
    public int columns = 5;
    public int totalBrick;
    public Transform brickSpawnPos;
    public float spacing = 5.0f;
    public Brick brickPrefab;
    public Transform brickSpawner;


    public List<Brick> bricks = new List<Brick>();
    private List<Vector3> emptyPos = new List<Vector3>();
    public List<ColorType> deActiveBricks = new List<ColorType>();

    void Start()
    {
        SpawnListPosBrick();
        totalBrick = rows * columns;

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
            }
        }
    }

    //Spawn brick with colorType
    private void SpawnBrick(ColorType color)
    {
            // Spawn bricks equally for 4 characters
         
           for (int i = 0; i < (totalBrick / 4); i++)
           {
                if (GetBrickPoint(color).Count == (totalBrick / 4) )
                {
                    return;
                }
                else
                {
                    if (emptyPos.Count != 0)
                    {
                        Vector3 position = emptyPos[Random.Range(0, emptyPos.Count)];
                        Brick brick = SimplePool.Spawn<Brick>(brickPrefab, position, Quaternion.identity);
                        brick.transform.SetParent(brickSpawner);
                        emptyPos.Remove(position);
                        brick.ChangeColor(color);
                        bricks.Add(brick);
                    }
                }
           }
            
    }
 
    //Get list Brick of ColorType
    public List<Vector3> GetBrickPoint(ColorType colorType)
    {
        List<Vector3> list = new List<Vector3>();
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType && bricks[i].rd.enabled)
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
            SpawnBrick(other.gameObject.GetComponent<Character>().colorType);
            other.gameObject.GetComponent<Character>().platform = this;
        }
    }
    public void ClearAll()
    {
        //foreach(var i in bricks)
        //{
        //    SimplePool.Despawn(i);
        //}
        //bricks.Clear();
        //emptyPos.Clear();
    }
}
