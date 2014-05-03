using UnityEngine;
using System.Collections;
//using System.Xml.Linq;
using System.IO;
using System.Xml;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

public static class Tools {


    /// <summary>
    /// Timer Function.
    /// </summary>
    /// <param name="seconds">Delay time in seconds</param>
    /// <param name="action">Action to execute after time.</param>
    /// <returns></returns>
    public static IEnumerator WaitSeconds(float seconds, Action action) {
        yield return new WaitForSeconds(seconds);
        action();
    }

    public static void WriteConfigFile(float volumeLevel, string playerName, string masterServerIp, int masterServerPort, bool useUnityMasterServer) {
        try {
            using (FileStream fs = new FileStream("config.cfg", FileMode.OpenOrCreate)) {
                using (StreamWriter writer = new StreamWriter(fs)) {
                    writer.WriteLine(String.Format("volumeLevel={0}", volumeLevel));
                    writer.WriteLine(String.Format("playerName={0}", playerName));
                    writer.WriteLine(String.Format("masterServerIp={0}", masterServerIp));
                    writer.WriteLine(String.Format("masterServerPort={0}", masterServerPort));
                    writer.WriteLine(String.Format("useUnityMasterServer={0}", useUnityMasterServer));
                    writer.Close();
                }
            }
        } catch (Exception e) {
            throw e;
        }
    }

    // Convert an object to a byte array
    public static byte[] SerializeObject<T>(T obj) {
        if (obj == null)
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, obj);

        ms.Position = 0;

        return ms.ToArray();
    }

    // Convert a byte array to an Object
    public static T DeserializeObject<T>(byte[] arrBytes) {
        MemoryStream memStream = new MemoryStream(arrBytes);
        memStream.Position = 0;

        BinaryFormatter binForm = new BinaryFormatter();
        binForm.Binder = new VersionFixer();

        //memStream.Write(arrBytes, 0, arrBytes.Length);
        //memStream.Seek(0, SeekOrigin.Begin);

        T obj = (T)binForm.Deserialize(memStream);

        return obj;
    }

    public static object[] MessArray(object[] arr) {
        System.Random rnd = new System.Random();
        int actualPos, changePos;
        object aux;

        for (int x = 0; x < (arr.Length * 5); x++) {
            actualPos = rnd.Next(0, arr.Length);
            changePos = rnd.Next(0, arr.Length);

            if(changePos == actualPos)
                changePos = rnd.Next(0, arr.Length);

            aux = arr[actualPos];
            arr[actualPos] = arr[changePos];
            arr[changePos] = aux;
        }
        return arr;
    }

    // This class fixes up problems with assembly mismatch between different
    // kinds of build. For example, it allows you to use it between a standalone
    // and the editor.

    sealed class VersionFixer : SerializationBinder {
        public override Type BindToType(string assemblyName, string typeName) {
            Type typeToDeserialize = null;

            // For each assemblyName/typeName that you want to deserialize to
            // a different type, set typeToDeserialize to the desired type.
            String assemVer1 = Assembly.GetExecutingAssembly().FullName;

            if (assemblyName != assemVer1) {
                // To use a type from a different assembly version, 
                // change the version number.
                // To do this, uncomment the following line of code.
                assemblyName = assemVer1;

                // To use a different type from the same assembly, 
                // change the type name.
            }

            // The following line of code returns the type.
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

            return typeToDeserialize;
        }

    }

    //public static XDocument loadXMLFile(string xmlFilePath) {
    //    XDocument document = null;
    //    try {
    //        TextAsset xmlFile = Resources.Load(xmlFilePath) as TextAsset;
    //        MemoryStream assetStream = new MemoryStream(xmlFile.bytes);
    //        XmlReader xmlReader = XmlReader.Create(assetStream);
    //        document = XDocument.Load(xmlReader);
    //        xmlReader.Close();
    //    } catch (FileNotFoundException ex) {
    //        Debug.Log(string.Format("Arquivo não encontrado!\nErro: {0}", ex.Message));
    //    } catch (Exception ex) {
    //        Debug.Log(ex.Message);
    //    }
    //    return document;
    //}
}
