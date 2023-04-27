using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Unit", menuName = "Unit/Create new unit")]

public class UnitBase : ScriptableObject
{
    [SerializeField] string name;

    [SerializeField] Sprite spriteImage;

    [SerializeField] int maxHP;

    [SerializeField] List<LearnableMove> learnableMoves;

    [SerializeField] List<AttackMoveBase> movesBase;

    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

    public string Name {
        get { return name; }
    }

    public int MaxHP {
        get { return maxHP; }
    }

    public Sprite SpriteImage{
        get { return spriteImage; }
    }

    //List of learnable moves for the player
    public List<LearnableMove> LearnableMoves {
        get { return learnableMoves; }
    }

    //List of unit's current moves
    public List<AttackMoveBase> MovesBase {
        get { return movesBase; }
    }

    public void AddMove(AttackMoveBase newMove, int index)
    {
        movesBase[index] = newMove;
    }

}

public enum EnemyDefeated
{
    NONE,
    OCTOCAT,
    KILLER
}

[System.Serializable]

public class LearnableMove
{
    [SerializeField] AttackMoveBase moveBase;
    [SerializeField] EnemyDefeated enemy;

    public AttackMoveBase Base  {
        get { return moveBase; }
    }

    public EnemyDefeated Enemy {
        get { return enemy; }
    }

}



