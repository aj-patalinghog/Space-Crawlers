using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] UnitBase _base;

    public Unit Unit { get; set; }

    public void SetUpUnit()
    {
        Unit = new Unit(_base);

        GetComponent<Image>().enabled = true;
        GetComponent<Image>().sprite = Unit.Base.SpriteImage;
    }
}
