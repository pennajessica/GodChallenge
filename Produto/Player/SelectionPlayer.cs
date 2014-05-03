using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GodChallenge.Domain;

public class SelectionPlayer {
    private static SelectionPlayer singleton = new SelectionPlayer();
    public Player[] Players { get; private set; }
    public SelectionPlayer() {
        singleton = this;

        this.Players = new Player[7];
        this.Players[0] = new Zeus(1000);
        this.Players[1] = new Hades(1000);
        this.Players[2] = new Poseidon(1000);
        this.Players[3] = new Athena(1000);
        this.Players[4] = new Artemis(1000);
        this.Players[5] = new Ares(1000);
        this.Players[6] = new ZeusTest(1000);
    }
    public static Player getPlayer(int id) {
        if (id < singleton.Players.Length) {
            return singleton.Players[id];
        } else {
            return null;
        }
    }
    
}
