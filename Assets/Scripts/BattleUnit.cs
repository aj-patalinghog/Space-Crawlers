using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    public UnitBase unitBase;

    public Unit Unit { get; set; }

    public void SetUpUnit()
    {
        Unit = new Unit(unitBase);

        GetComponent<Image>().enabled = true;
        GetComponent<Image>().sprite = Unit.Base.SpriteImage;
    }
}
