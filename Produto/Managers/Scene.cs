using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GodChallenge.Manager {
    public class Scene {
        private string name;
        private Texture2D[] hud;
        private float distanceNodes;

        public string Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
            }
        }
        public Texture2D[] Hud {
            get {
                return this.hud;
            }
            set {
                this.hud = value;
            }
        }
        public float SkillStartX { get; set; }

        public float SkillStartY { get; set; }

        public Rect positionAndScale { get; set; }

        public Rect MiniMapIssues { get; set; }
        public Rect MiniMapPosAndScale { get; set; }
        
        public float DistanceNodes {
            get {
                return this.distanceNodes;
            }
            set {
                this.distanceNodes = value;
            }
        }

        public Scene() {
            this.hud = new Texture2D[3];
            this.distanceNodes = 20f;
        }
    }
}
