﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItem : ItemBasic
{
    public int LifeRestore;

    public abstract void Use(IConsume consumer);
}