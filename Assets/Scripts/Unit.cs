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

    public LearnableMove GetLearnableMove()
    {
        return Base.LearnableMoves.Where((LearnableMove move) => move.Enemy == BattleSystem.enemy).FirstOrDefault();
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
