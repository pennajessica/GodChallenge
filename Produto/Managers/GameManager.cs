using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GodChallenge.Domain;
using GodChallenge.Lobby;

namespace GodChallenge.Manager {
    public class GameManager {
        private static GameManager singleton = new GameManager();

        public static string GameChat {
            get {
                if (string.IsNullOrEmpty(singleton.Chat)) {
                    return "";
                } else {
                    return singleton.Chat;
                }
            }
            set {
                singleton.Chat = value;
            }
        }

        public string Chat { get; set; }

        public List<Scene> SceneMap { get; private set; }

        public double NetworkTime { get; set; }

        public float StartTimeMatch { get; set; }

        public float MaxTurnTime { get; set; }

        public List<ClientMultiplayer> clients;

        public int nextClientID;

        public GameManager() {
            singleton = this;

            this.clients = new List<ClientMultiplayer>();
            this.nextClientID = 0;

            this.SceneMap = new List<Scene>();
            this.SceneMap.Add(new Scene() { Name = "MenuJogo" });

            Scene zeusScene = new Scene();
            zeusScene.Name = "Zeus";
            zeusScene.Hud[0] = Resources.Load("HUD/Zeus/hud_inventario") as Texture2D;
            zeusScene.Hud[1] = Resources.Load("HUD/Zeus/hud_minimap") as Texture2D;
            zeusScene.Hud[2] = Resources.Load("HUD/Zeus/hud_inimigos") as Texture2D;
            //zeusScene.X = 50.27f;
            //zeusScene.Y = 111;
            zeusScene.SkillStartX = 67;
            zeusScene.SkillStartY = 107.86f;
            zeusScene.positionAndScale = new Rect() { width = 1, height = 1f, x = 45 };

            zeusScene.MiniMapPosAndScale = new Rect() { width = 1, height = 1 };
            zeusScene.MiniMapIssues = new Rect() {
                x = 14.5f,
                y = 15,
                width = -67,
                height = -50
            };

            this.SceneMap.Add(zeusScene);

            Scene hadesScene = new Scene();
            hadesScene.Name = "Hades";
            hadesScene.Hud[0] = Resources.Load("HUD/Hades/hud_inventario") as Texture2D;
            hadesScene.Hud[1] = Resources.Load("HUD/Hades/hud_minimap") as Texture2D;
            hadesScene.Hud[2] = Resources.Load("HUD/Hades/hud_inimigos") as Texture2D;
            hadesScene.SkillStartX = 67.8f;
            hadesScene.SkillStartY = 108.35f;
            hadesScene.DistanceNodes = 15;

            hadesScene.positionAndScale = new Rect() { width = 1, height = 1, x = 22 };

            hadesScene.MiniMapPosAndScale = new Rect() { 
                width = 1, 
                height = 1,
                x = -52.53f,
                y = 0
            };
            hadesScene.MiniMapIssues = new Rect() {
                x = 69.88f,
                y = 94.43f,
                width = -170.4f,
                height = -108.59f
            };

            this.SceneMap.Add(hadesScene);

            Scene poseidonScene = new Scene();
            poseidonScene.Name = "Poseidon";
            poseidonScene.Hud[0] = Resources.Load("HUD/Poseidon/hud_inventario") as Texture2D;
            poseidonScene.Hud[1] = Resources.Load("HUD/Poseidon/hud_minimap") as Texture2D;
            poseidonScene.Hud[2] = Resources.Load("HUD/Poseidon/hud_inimigos_1") as Texture2D;
            poseidonScene.SkillStartX = 67.8f;
            poseidonScene.SkillStartY = 108.35f;
            poseidonScene.positionAndScale = new Rect() { width = 1, height = 1, x = 22 };

            poseidonScene.MiniMapPosAndScale = new Rect() { width = 1, height = 1 };
            poseidonScene.MiniMapIssues = new Rect() {
                x = 12.94f,
                y = 15,
                width = -55.18f,
                height = -43
            };

            this.SceneMap.Add(poseidonScene);

            Scene athenaScene = new Scene();
            athenaScene.Name = "Athena";
            athenaScene.Hud[0] = Resources.Load("HUD/Athena/hud_inventario") as Texture2D;
            athenaScene.Hud[1] = Resources.Load("HUD/Athena/hud_minimap") as Texture2D;
            athenaScene.Hud[2] = Resources.Load("HUD/Athena/hud_inimigos") as Texture2D;
            athenaScene.SkillStartX = 67.8f;
            athenaScene.SkillStartY = 108.35f;
            athenaScene.positionAndScale = new Rect() { width = 1, height = 1, x = 22 };
            athenaScene.DistanceNodes = 5;

            athenaScene.MiniMapPosAndScale = new Rect() { width = 1, height = 1 };
            athenaScene.MiniMapIssues = new Rect() {
                x = 14,
                y = 63.42f,
                width = -55.83f,
                height = -77.15f
            };
            this.SceneMap.Add(athenaScene);

            Scene artemisScene = new Scene();
            artemisScene.Name = "Artemis";
            artemisScene.Hud[0] = Resources.Load("HUD/Artemis/hud_inventario") as Texture2D;
            artemisScene.Hud[1] = Resources.Load("HUD/Artemis/hud_minimap") as Texture2D;
            artemisScene.Hud[2] = Resources.Load("HUD/Artemis/hud_inimigos") as Texture2D;
            artemisScene.SkillStartX = 67.8f;
            artemisScene.SkillStartY = 108.35f;
            artemisScene.positionAndScale = new Rect() { width = 1, height = 1, x = 22 };
            artemisScene.DistanceNodes = 5;

            artemisScene.MiniMapPosAndScale = new Rect() { width = 1, height = 1 };
            artemisScene.MiniMapIssues = new Rect() {
                x = 14,
                y = 63.42f,
                width = -70.2f,
                height = -77.6f
            };
            this.SceneMap.Add(artemisScene);

            Scene aresScene = new Scene();
            aresScene.Name = "Ares";
            aresScene.Hud[0] = Resources.Load("HUD/Ares/hud_inventario") as Texture2D;
            aresScene.Hud[1] = Resources.Load("HUD/Ares/hud_minimap") as Texture2D;
            aresScene.Hud[2] = Resources.Load("HUD/Ares/hud_inimigos") as Texture2D;
            aresScene.SkillStartX = 67.8f;
            aresScene.SkillStartY = 108.35f;
            aresScene.positionAndScale = new Rect() { width = 1, height = 1, x = 22 };
            aresScene.DistanceNodes = 5;

            aresScene.MiniMapPosAndScale = new Rect() { width = 1, height = 1 };
            aresScene.MiniMapIssues = new Rect() {
                x = 14.21f,
                y = 48.01f,
                width = -69.65f,
                height = -63.7f
            };
            this.SceneMap.Add(aresScene);

            Scene tutoScene = new Scene();
            tutoScene.Name = "Tutorial";
            tutoScene.Hud[0] = Resources.Load("HUD/Zeus/hud_inventario") as Texture2D;
            tutoScene.Hud[1] = Resources.Load("HUD/Zeus/hud_minimap") as Texture2D;
            tutoScene.Hud[2] = Resources.Load("HUD/Zeus/hud_inimigos") as Texture2D;
            //zeusScene.X = 50.27f;
            //zeusScene.Y = 111;
            tutoScene.SkillStartX = 22;
            tutoScene.SkillStartY = 85;
            tutoScene.positionAndScale = new Rect() { width = 0.8f, height = 0.8f };

            this.SceneMap.Add(tutoScene);
        }

        public static Scene getSceneInfo(int sceneIndex) {
            //Debug.Log(sceneIndex);
            if (singleton.SceneMap == null || sceneIndex < 0 || sceneIndex > singleton.SceneMap.Count)
                return null;

            return singleton.SceneMap[sceneIndex];
        }

        private void setMaxTurnTime(int minutes) {
            this.MaxTurnTime = minutes * 60;
        }

        public static void startMatch(int minutes) {
            singleton.setMaxTurnTime(minutes);
            singleton.StartTimeMatch = Time.time;
        }

        public static float getStartTurnTime() {
            return singleton.StartTimeMatch;
        }

        public static float getMaxTurnTime() {
            return singleton.MaxTurnTime;
        }

        public static string getTimeMatch() {
            float timeMatch = Time.time - singleton.StartTimeMatch;

            int hours = (int)(timeMatch / 3600);

            int minutes = (int)(timeMatch / 60);

            int seconds = (int)(timeMatch % 60);

            string time = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

            return time;
        }

        public static string NetworkClock {
            get {
                double timeMatch = Network.time - singleton.NetworkTime;

                int hours = (int)(timeMatch / 3600);

                int minutes = (int)(timeMatch / 60);

                int seconds = (int)(timeMatch % 60);

                string time = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

                return time;
            }
        }

        public static void SetNetworkTime() {
            singleton.NetworkTime = Network.time;
        }

        [System.Serializable]
        public class ClientMultiplayer {
            public NetworkPlayer player;
            public int playerId;
            public string playerName;
            public GodSelect selected;
            public bool isReadyToPlay;

            public override string ToString() {
                return string.Concat(playerName, ": isReadyToPlay: ", isReadyToPlay);
            }
        }

        public static void AddClient(ClientMultiplayer client) {
            singleton.clients.Add(client);
            singleton.nextClientID++;
            singleton.clients = singleton.clients.OrderBy(l => l.playerId).ToList();
        }

        public static List<ClientMultiplayer> Clients {
            get {
                return singleton.clients;
            }
        }

        public static int NextClientID {
            get {
                return singleton.nextClientID;
            }
        }

        /// <summary>
        /// Verify if the Player is Ready to Play.
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static bool IsReadyToPlay(NetworkPlayer me) {
            return GameManager.GetClientByOwner(me).isReadyToPlay;
        }

        /// <summary>
        /// Remove the client from List clients
        /// </summary>
        /// <param name="owner"></param>
        public static void RemoveClientByOwner(NetworkPlayer owner) {
            singleton.clients.Remove(GameManager.GetClientByOwner(owner));
        }

        /// <summary>
        /// Notify that player is ready to Play.
        /// </summary>
        /// <param name="owner"></param>
        public static void SetReadyToPlay(NetworkPlayer owner) {
            GameManager.GetClientByOwner(owner).isReadyToPlay = true;
        }

        /// <summary>
        /// Get the Client from list of clients by the networkplayer.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static ClientMultiplayer GetClientByOwner(NetworkPlayer owner) {
            return singleton.clients.Find(l => l.player == owner);
        }
        
        /// <summary>
        /// Get the Client from list of clients by the playerId.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static ClientMultiplayer GetClientById(int playerId) {
            return singleton.clients.Find(l => l.playerId == playerId);
        }
    }


}