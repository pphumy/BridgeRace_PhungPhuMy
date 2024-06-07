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
   public Renderer rd;
   private Platform platform;

    //public List<Brick> deActiveBricks = new List<Brick>();

   

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        rd.material = colors.GetMat(colorType);
    }


    private IEnumerator RespawnBrick()
    {
        yield return new WaitForSeconds(6);
        ActiveBrick();
    }

    private void Awake()
    {
        colorType = ColorType.None;
        //platform = this.GetComponentInParent<Platform>();
    }

    public void ActiveBrick()
    {
        this.ChangeColor(platform.deActiveBricks[Random.Range(0, platform.deActiveBricks.Count)].colorType);
        platform.deActiveBricks.Remove(this);
        rd.enabled = true;
        col.enabled = true;
        platform.bricks.Add(this);
    }
    public void DeActiveBrick()
    {
        rd.enabled = false;
        col.enabled = false;
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
