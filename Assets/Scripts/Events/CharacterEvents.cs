using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    //charac damage and damage val
    public static UnityAction<GameObject, int> characterDamaged;

    //charac heal and amnt heal
    public static UnityAction<GameObject, int> characterHealed;
}