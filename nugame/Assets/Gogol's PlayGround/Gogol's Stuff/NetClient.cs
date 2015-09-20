using UnityEngine.Networking;

/// <summary>
/// Represents a Client on the network.
/// </summary>
public class NetClient
{
    /// <summary>
    /// Connection to the server.
    /// </summary>
    public NetConnection connection = null;

    /// <summary>
    /// Represents if the client is connected to a server.
    /// </summary>
    static bool isConnected = false;

    public NetClient()
    {

    }

    /// <summary>
    /// Connect to a server, Default is to ourself.
    /// </summary>
    /// <param name="ip"> IPaddress of server. Default is localhost</param>
    /// <param name="port"> Port on server. Default is 65000</param>
    /// <returns></returns>
    public bool Connect(string ip = "127.0.0.1", int port = 65000)
    {
        if (!NGNetworkManager.isInit)
            return false;
        else
        {
            byte error;
            int connectionID = NetworkTransport.Connect(NGNetworkManager.socketID, ip, port, 0, out error);

            // There was an error.
            if (error != (byte)NetworkError.Ok)
                return false;
            else
            {
                connection = new NetConnection(connectionID);
                return true;
            }
        }
    }

    public void Disconnect()
    {
        if (!NGNetworkManager.isInit ||!isConnected)
            return;

        connection.Disconnect();
    }


    /// <summary>
    /// Send a message to the server.
    /// </summary>
    /// <param name="_buffer">The payload </param>
    /// <param name="_bufferSize">the size of the payload. </param>
    /// <param name="_channel">the channel to send it on. </param>
    public void Send(byte[] _buffer, int _bufferSize, int _channel)
    {
        if (!NGNetworkManager.isInit || !isConnected)
            return;

        connection.Send(_buffer, _bufferSize, _channel);
    }

    int recConnectionId;
    int recChannelId;
    byte[] recBuffer = new byte[1024];
    int bufferSize = 1024;
    int dataSize;
    byte error;
    int amt;

    public void ClientUpdate()
    {
        //NGNetworkManager.LOG("{CLIENT} Running Client loop.");
        NetworkEventType recNetworkEvent = Receive(out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);
        do
        {
            //NGNetworkManager.LOG("{CLIENT} Processing Event...");
            switch (recNetworkEvent)
            {
                // Nothing happened.
                case NetworkEventType.Nothing:
                    break;

                // We connected to the server!
                case NetworkEventType.ConnectEvent:
                    isConnected = true;
                    NGNetworkManager.LOG("CONNECTED!");
                    connection.Refresh();
                    break;

                // We got game data.
                case NetworkEventType.DataEvent:
                    handleData(recBuffer, dataSize, recChannelId);
                    break;

                // We disconnected from the server.
                case NetworkEventType.DisconnectEvent:
                    isConnected = false;
                    NGNetworkManager.LOG("Disconnected!");
                    connection = null;
                    break;
            }
        } while (recNetworkEvent != NetworkEventType.Nothing);
    }

    public void handleData(byte[] buffer, int bufferSize, int channel)
    {

    }

    public NetworkEventType Receive(out int _connectionID, out int channelID, byte[] buffer, int buffersize, out int datasize, out byte error)
    {
        int recSocketID;
        NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recSocketID, out _connectionID, out channelID, buffer, buffersize, out datasize, out error);
        if (recSocketID != NGNetworkManager.socketID && _connectionID != connection.getConnectionID())
        {
            return NetworkEventType.Nothing;
        }
        return recNetworkEvent;
    }
}

