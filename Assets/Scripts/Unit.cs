using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Unit : MonoBehaviour
{
    public UnitBase _base; 

    public int HP { get; set; }

    public List<AttackMove> Moves { get; set; }

    public Unit(UnitBase pBase)
    {
        _base = pBase;

        HP = Base.MaxHP;

        Moves = new List<AttackMove>();
        foreach (var move in Base.MovesBase)
        {
            Moves.Add(new AttackMove(move));
        }
    }

    public UnitBase Base {
        get { return _base; }
    }

    public AttackMoveBase GetLearnableMove(List<LearnableMove> playerLearnableMoves)
    {
<<<<<<< Updated upstream
        return Base.LearnableMoves.Where((LearnableMove move) => move.Enemy == BattleSystem.enemyDefeated).FirstOrDefault();
=======
        List<AttackMoveBase> newMoves;
        newMoves = new List<AttackMoveBase>();
        foreach (var move in playerLearnableMoves){
            if(move.Enemy == BattleSystem.enemy)
                newMoves.Add(move.Base);
        }
        int maxLearnableMoves = newMoves.Count;
        AttackMoveBase newMove = newMoves[(UnityEngine.Random.Range(0,maxLearnableMoves))];
        return newMove;
>>>>>>> Stashed changes
    }

    public bool TakeDamage(int damage){
        HP -= damage;
        if(HP < 0) HP = 0;
        return (HP == 0) ? true : false;
    }

    public void Heal(int heal) {
        HP += heal;
        if(HP > Base.MaxHP) HP = Base.MaxHP;
    }

}
