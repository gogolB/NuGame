using UnityEngine;
using UnityEngine.Networking;

public class NGNetworkManager : MonoBehaviour
{
    #region Channels
    // All the channels for Networking.
    public static int chatChannelID = 0;
    public static int actionChannelID = 0;
    public static int motionChannelID = 0;
    public static int eventsChannelID = 0;
    public static int acksChannelID = 0;
    #endregion

    // The maximum number of connections possible.
    static int MaxConnections = 10;
    public static int socketID = 0;
    static int socketPort = 65000;
    public static bool isInit = false;

    #region Server Variables
    // Server stuff.
    static bool isServer = false;
    public static NetServer Server = null;
    #endregion

    #region Client Variables
    // Client stuff.
    public static NetClient Client = null;
    #endregion

    // Initilizes the NetworkManager, irrespective of wether it is a server or not.
    private static ConnectionConfig Init()
    {
//

        // Initializing the Transport Layer with custom settings
        GlobalConfig gc = new GlobalConfig();
        gc.ReactorModel = ReactorModel.FixRateReactor;
        gc.ThreadAwakeTimeout = 10;
        NetworkTransport.Init(gc);

        // Setting up all the Channels with the appropriate QoS.
        ConnectionConfig config = new ConnectionConfig();
        chatChannelID = config.AddChannel(QosType.ReliableSequenced);
        actionChannelID = config.AddChannel(QosType.Reliable);
        motionChannelID = config.AddChannel(QosType.Unreliable);
        eventsChannelID = config.AddChannel(QosType.Reliable);
        acksChannelID = config.AddChannel(QosType.Reliable);

        isInit = true;
        return config;
    }

    // Called once per frame.
    void Update()
    {
        if (!isInit)
            return;
        if (isServer)
        {
            Server.ServerUpdate();
        }
        else if(Client != null)
        {
            Client.ClientUpdate();
        }
    }

    /// <summary>
    /// Starts a server with the given max players.
    /// </summary>
    /// <param name="maxPlayers"></param>
    public static void StartServer(int maxPlayers = 4)
    {
        MaxConnections = maxPlayers;
        HostTopology topology = new HostTopology(Init(), MaxConnections);
        socketID = NetworkTransport.AddHost(topology, socketPort);
        #if UNITY_EDITOR
            Debug.Log("Socket Open. SocketId is: " + socketID);
        #endif

        Server = new NetServer(maxPlayers);
        isServer = true;
    }

    /// <summary>
    /// Start a Client for connecting to another server.
    /// </summary>
    public static void StartClient()
    {
        HostTopology topology = new HostTopology(Init(), MaxConnections);
        socketID = NetworkTransport.AddHost(topology);
        #if UNITY_EDITOR
            Debug.Log("Socket Open. SocketId is: " + socketID);
        #endif
        
        Client = new NetClient();
        isServer = false;
    }

    /// <summary>
    /// Set the port.
    /// </summary>
    /// <param name="port"></param>
    public static void setPort(int port = 65000)
    {
        if (isInit)
            return;

        socketPort = port;
    }

    public static void LOG(string msg)
    {
        Debug.Log("[NETWORK]" + msg);
    }

}
