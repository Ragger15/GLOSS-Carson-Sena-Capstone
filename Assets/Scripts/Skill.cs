using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skill
{
    [field: SerializeField] public int SkillLevel { get; set; }
    [field: SerializeField] public string Name { get; set; }

}
