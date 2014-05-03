using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Configuration {
    private static Configuration sing = new Configuration();
    public string PlayerName { get; set; }
    public float VolumeLevel { get; set; }
    public string MasterServerIp { get; set; }
    public int MasterServerPort { get; set; }
    public bool UseUnityMasterServer { get; set; }
    private string configPath = "config.cfg";

    public Configuration() {
        if (sing == null) {
            sing = this;

            if (File.Exists(configPath)) {
                try {
                    using (StreamReader sr = new StreamReader(configPath)) {
                        string volumeLevel = sr.ReadLine();
                        string playerName = sr.ReadLine();
                        string masterServerIp = sr.ReadLine();
                        string masterServerPort = sr.ReadLine();
                        string useUnityMasterServer = sr.ReadLine();

                        PlayerName = playerName.Split('=')[1];
                        VolumeLevel = String.IsNullOrEmpty(volumeLevel.Split('=')[1]) ? 1 : Convert.ToSingle(volumeLevel.Split('=')[1]);
                        MasterServerIp = String.IsNullOrEmpty(masterServerIp.Split('=')[1]) ? "127.0.0.1" : masterServerIp.Split('=')[1];
                        MasterServerPort = String.IsNullOrEmpty(masterServerPort.Split('=')[1]) ? 23466 : Convert.ToInt32(masterServerPort.Split('=')[1]);
                        UseUnityMasterServer = String.IsNullOrEmpty(useUnityMasterServer.Split('=')[1]) ? true : Convert.ToBoolean(useUnityMasterServer.Split('=')[1]);
                        
                        AudioListener.volume = VolumeLevel;
                    }
                } catch (Exception) {
                    throw;
                }
            } else {
                Tools.WriteConfigFile(1, "Player", "127.0.0.1", 23466, true);
            }
        }
    }

    public static void SaveConfigurations() {
        Tools.WriteConfigFile(sing.VolumeLevel, sing.PlayerName, sing.MasterServerIp, sing.MasterServerPort, sing.UseUnityMasterServer);
    }

    public static void ChangeVolume(float volume) {
        sing.VolumeLevel = volume;
        AudioListener.volume = sing.VolumeLevel;
    }

    public static void ChangePlayerName(string name) {
        sing.PlayerName = name;
    }

    public static void ChangeMasterServerIp(string masterServerIp) {
        sing.MasterServerIp = masterServerIp;
    }

    public static void ChangeMasterServerPort(int port) {
        sing.MasterServerPort = port;
    }

    public static void ChangeUseUnityMasterServer(bool use) {
        sing.UseUnityMasterServer = use;
    }

    public static float GetVolume() {
        return sing.VolumeLevel;
    }

    public static string GetPlayerName() {
        return sing.PlayerName;
    }

    public static string GetMasterServerIp() {
        return sing.MasterServerIp;
    }

    public static int GetMasterServerPort() {
        return sing.MasterServerPort;
    }

    public static bool GetUseUnityMasterServer() {
        return sing.UseUnityMasterServer;
    }
}
