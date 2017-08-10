﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {
    public int playerHp = 1;
    public float playerSpeed = 1;
    public float enemySpeed = 1;
    public int damageAmount = 1;

    public static Config game;
	
	void Start () {
        game = this;
	}
	
}