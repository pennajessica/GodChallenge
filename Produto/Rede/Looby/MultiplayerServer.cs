using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerServer : MonoBehaviour {
    #region [ Variables ]

    private string gameNameType = "GodChallenge v1.0 Server";

    private Ping masterServerPing;

    private int connectionPort = 26500;

    private List<Ping> serverPingList = new List<Ping>();

    private HostData[] hostData;

    private int numberOfPlayers = 5;

    private string serverName;

    public string playerName;

    private string titleMessage = "God Challenge Network";

    #region [ Window Variables ]

    private WindowNetworkState windowState;

    #region [ Main Window ]

    private Rect mainWindowRect;

    private int mainWindowWidth = 400;

    private int mainWindowHeight = 200;

    private int btnHeightCntWindow = 60;

    private int leftIndent;

    private int topIndent;

    #endregion [ Main Window ]

    #region [ Show Server List Window ]
    private GUIStyle plainStyle = new GUIStyle();

    private GUIStyle styleCentered = new GUIStyle();

    private GUIStyle pingStyle = new GUIStyle();

    private GUIStyle playersStyle = new GUIStyle();

    public Font font;

    private Vector2 scrollPosition = Vector2.zero;

    private int pbWidth = 90;

    private int sbWidth = 250;
    #endregion [ Show Server List Window ]

    private int defaultWindowWidth;
    private int defaultWindowHeigth;

    #endregion [ Window Variables ]


    #endregion [ Variables ]

    void Start() {
        serverName = PlayerPrefs.GetString("serverName");
        if (string.IsNullOrEmpty(serverName))
            serverName = "GodChallenge Server 1";

        playerName = Configuration.GetPlayerName();
        if (string.IsNullOrEmpty(playerName))
            playerName = "Player1";

        MasterServer.ipAddress = "127.0.0.1";
        MasterServer.port = 23466;

        MasterServer.RequestHostList(gameNameType);

        masterServerPing = new Ping(MasterServer.ipAddress);

        windowState = WindowNetworkState.MainWindow;

        //Set GUIStyles.

        plainStyle.alignment = TextAnchor.MiddleLeft;

        plainStyle.normal.textColor = Color.white;

        plainStyle.wordWrap = true;

        //plainStyle.fontStyle = FontStyle.Bold;

        plainStyle.font = font;

        styleCentered.alignment = TextAnchor.MiddleCenter;

        styleCentered.normal.textColor = Color.white;

        styleCentered.wordWrap = true;

        //boldStyleCentered.fontStyle = FontStyle.Bold;

        styleCentered.font = font;

        pingStyle.alignment = TextAnchor.MiddleCenter;

        pingStyle.normal.textColor = Color.white;

        pingStyle.wordWrap = true;

        pingStyle.fontStyle = FontStyle.Bold;

        pingStyle.font = font;

        playersStyle.alignment = TextAnchor.MiddleCenter;

        playersStyle.normal.textColor = Color.white;

        playersStyle.wordWrap = true;

        playersStyle.font = font;

        defaultWindowHeigth = mainWindowHeight;

        defaultWindowWidth = mainWindowWidth;

    }

    string teste = "";

    void OnGUI() {
        if (Network.peerType == NetworkPeerType.Disconnected) {
            leftIndent = Screen.width / 2 - mainWindowWidth / 2;

            topIndent = Screen.height / 2 - mainWindowHeight / 2;

            mainWindowRect = new Rect(leftIndent, topIndent, mainWindowWidth, mainWindowHeight);

            mainWindowRect = GUILayout.Window(0, mainWindowRect, ConnectionWindow, titleMessage);
        } else {
            GUILayout.Label(string.Format("{0} / {1} players conectados", Network.connections.Length, numberOfPlayers), plainStyle);

            GUILayout.Space(15);
            if (GUILayout.Button("Click!")) {
                networkView.RPC("Click", RPCMode.AllBuffered, playerName);
            }
            GUILayout.Space(15);
            GUILayout.Label(teste, plainStyle);

            
        }
    }

    [RPC]
    void Click(string name) {
        teste += string.Format("Player Click! {0}\n", name);
    }

    void ConnectionWindow(int windowId) {
        GUILayout.Space(15);

        switch (windowState) {
            case WindowNetworkState.MainWindow:
                DrawMainWindow();
                break;
            case WindowNetworkState.SetupServerWindow:
                DrawSetupServerWindow();
                break;
            case WindowNetworkState.ShowServerListWindow:
                DrawListWindow();
                break;
            default:
                break;
        }

        GUILayout.Space(15);
    }


    private void DrawMainWindow() {
        if (GUILayout.Button("Configurar um servidor", GUILayout.Height(btnHeightCntWindow)))
            windowState = WindowNetworkState.SetupServerWindow;

        GUILayout.Space(10);

        if (GUILayout.Button("Conectar a um servidor", GUILayout.Height(btnHeightCntWindow)))
            windowState = WindowNetworkState.ShowServerListWindow;

    }

    private void DrawSetupServerWindow() {
        GUILayout.Label("Insira o nome do servidor");

        serverName = GUILayout.TextField(serverName);


        GUILayout.Space(5);


        if (GUILayout.Button("Criar e registrar servidor público", GUILayout.Height(btnHeightCntWindow))) {
            //Save the serverName using PlayerPrefs.

            PlayerPrefs.SetString("serverName", serverName);


            //Tell the ScoreTable script the winning criteria.

            //TellEveryoneWinningCriteria(winningScore);


            //If this computer doesn't have a public address then use NAT.

            Network.InitializeServer(numberOfPlayers, connectionPort, !Network.HavePublicAddress());

            MasterServer.RegisterHost(gameNameType, serverName, "");

        }


        GUILayout.Space(10);


        if (GUILayout.Button("Voltar", GUILayout.Height(btnHeightCntWindow))) {
            windowState = WindowNetworkState.MainWindow;
        }
    }

    private void DrawListWindow() {
        GUILayout.Label("Insira seu nome de jogador", plainStyle);

        playerName = GUILayout.TextField(playerName);


        GUILayout.Box("", GUILayout.Height(5));

        GUILayout.Space(15);


        //If hostData is empty and and no public servers were
        //found then display that to the user.

        if (hostData == null) {
            GUILayout.Space(50);

            GUILayout.Label("Procurando servidores...", styleCentered);

            StartCoroutine(TalkToMasterServer());


            GUILayout.Space(50);
        } else if (hostData.Length == 0) {
            GUILayout.Space(50);

            GUILayout.Label("Nenhum servidor encontrado.", styleCentered);

            GUILayout.Space(50);
        }

        //If hostData isn't empty then display the list of public
            //servers it has.
        else {
            //Header row

            GUILayout.BeginHorizontal();

            GUILayout.Label("Servidores", plainStyle, GUILayout.Height(btnHeightCntWindow / 2), GUILayout.Width(sbWidth));

            GUILayout.Label("Jogadores", styleCentered, GUILayout.Height((btnHeightCntWindow / 2)), GUILayout.Width(pbWidth));

            //GUILayout.Label("IP Address", styleCentered, GUILayout.Height(btnHeightCntWindow/2));

            GUILayout.Label("Latência", styleCentered, GUILayout.Height(btnHeightCntWindow / 2), GUILayout.Width(pbWidth));

            GUILayout.EndHorizontal();


            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.MinHeight(btnHeightCntWindow));

            for (int i = 0; i < hostData.Length; i++) {

                GUILayout.BeginHorizontal();

                //Each of the available public servers are listed as buttons and the player
                //clicks on the relevant button to connect to a public server.

                if (GUILayout.Button(hostData[i].gameName,
                    GUILayout.Height(btnHeightCntWindow / 2), GUILayout.Width(sbWidth))) {
                    //Ensure that the player can't join a game with an empty name

                    if (playerName == "") {
                        playerName = "Player";
                    }

                    //If the player has a name that isn't empty then attempt to join 
                    //the server.

                    if (playerName != "") {
                        //Connect to the selected public server and save the player's name
                        //to player prefs.

                        Configuration.ChangePlayerName(playerName);
                        Configuration.SaveConfigurations(); 
                        Network.Connect(hostData[i]);
                    }
                }

                //Dispaly the number of players currently in the server and the max number of players.
                if (hostData[i].connectedPlayers == hostData[i].playerLimit)
                    playersStyle.normal.textColor = Color.red;
                else
                    playersStyle.normal.textColor = Color.white;


                GUILayout.Label(string.Format("{0} / {1}", hostData[i].connectedPlayers, (hostData[i].playerLimit)), playersStyle,
                    GUILayout.Height(btnHeightCntWindow / 2), GUILayout.Width(pbWidth));

                //GUILayout.Label(hostData[i].ip[0].ToString(), styleCentered, GUILayout.Height(btnHeightCntWindow/2));


                //List the latency of each of the public servers. If the ping isn't complete or a latency couldn't be retreived
                //then output N/A meaning Not Available. I think we can't ping computers within our own network that don't have 
                //a public IP address. The ping should work on servers that are not part of our network.

                if (serverPingList[i].isDone) {

                    if (serverPingList[i].time < 100)
                        pingStyle.normal.textColor = Color.green;
                    else
                        pingStyle.normal.textColor = Color.red;


                    GUILayout.Label(serverPingList[i].time.ToString(), pingStyle, GUILayout.Width(pbWidth), GUILayout.Height(btnHeightCntWindow / 2));

                } else {
                    GUILayout.Label("N/A", styleCentered, GUILayout.Height(btnHeightCntWindow / 2), GUILayout.Width(pbWidth));
                }


                GUILayout.EndHorizontal();

                GUILayout.Space(10);


            }

            GUILayout.EndScrollView();
        }

        GUILayout.Space(15);

        GUILayout.Box("", GUILayout.Height(5));


        //A refresh button that allows the user to refresh the list of
        //public servers.

        if (GUILayout.Button("Atualizar", GUILayout.Height(btnHeightCntWindow / 2))) {
            hostData = new HostData[0];

            StartCoroutine(TalkToMasterServer());
        }


        GUILayout.Space(10);


        if (GUILayout.Button("Voltar", GUILayout.Height(btnHeightCntWindow / 2))) {
            MakeConnectionWindowDefaultSize();

            hostData = new HostData[0];
            windowState = WindowNetworkState.MainWindow;
        }
    }

    private void MakeConnectionWindowDefaultSize() {
        mainWindowWidth = defaultWindowWidth;
        mainWindowHeight = defaultWindowHeigth;
    }


    /// <summary>
    /// Search all servers available
    /// </summary>
    /// <returns></returns>
    private IEnumerator TalkToMasterServer() {
        hostData = new HostData[0];

        // Limpa a lista de hosts do MasterServer
        MasterServer.ClearHostList();

        // Pede uma nova lista utilizando o tipo de servidor do meu jogo.
        MasterServer.RequestHostList(gameNameType);

        // Espera o tempo necessário para fazer esta requisição.
        yield return new WaitForSeconds(masterServerPing.time / 100 + 0.1f);

        // Recebe a lista dos servidores que estão disponiveis para este jogo.
        hostData = MasterServer.PollHostList();

        // Limpa a lista de servidores.
        serverPingList.Clear();
        serverPingList.TrimExcess();

        // Adiciona os novos pings dos servidores a lista de pings.
        if (hostData.Length > 0)
            foreach (HostData hd in hostData)
                serverPingList.Add(new Ping(hd.ip[0]));

    }
}

public enum WindowNetworkState {
    MainWindow,
    SetupServerWindow,
    ShowServerListWindow
}
