using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit
{
    // Start is called before the first frame update
    public ColorType colorType;


   [SerializeField] private ColorSO colors;
   [SerializeField] Collider col;
   [SerializeField] GameObject brickPrefab;
   [SerializeField] private Renderer rd;
    private Platform platform;

    public List<Brick> deActiveBricks = new List<Brick>();

   

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        rd.material = colors.GetMat(colorType);
    }


    private IEnumerator RespawnBrick()
    {
        yield return new WaitForSeconds(4);
        this.ChangeColor(deActiveBricks[Random.Range(0, deActiveBricks.Count)].colorType);
        deActiveBricks.Remove(this);
        rd.enabled = true;
        col.enabled = true;
        platform.bricks.Add(this);
    }
    private void Awake()
    {
        colorType = ColorType.None;
        //platform = this.GetComponentInParent<Platform>();
    }

    public void DeActiveBrick()
    {
        rd.enabled = false;
        col.enabled = false;
        deActiveBricks.Add(this);
        StartCoroutine(nameof(RespawnBrick));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_CHARACTER))
        {
            Character character = Cache.GetCharacter(other);
            this.platform = character.platform;
        }
    }
}
