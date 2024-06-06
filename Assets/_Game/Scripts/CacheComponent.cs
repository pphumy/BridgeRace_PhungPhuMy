using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheComponent
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider col)
    {
        if (!characters.ContainsKey(col))
        {
            characters.Add(col, col.GetComponent<Character>());
        }

        return characters[col];
    }

    //private static Dictionary<Collider, BridgeBrick> bridgeBricks = new Dictionary<Collider, BridgeBrick>();

    //public static BridgeBrick GetBridgeBrick(Collider col)
    //{
    //    if (!bridgeBricks.ContainsKey(col))
    //    {
    //        bridgeBricks.Add(col, col.GetComponent<BridgeBrick>());
    //    }

    //    return bridgeBricks[col];
    //
    
}

    
