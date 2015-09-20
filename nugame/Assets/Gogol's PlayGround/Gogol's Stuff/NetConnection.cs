using UnityEngine.Networking;

/// <summary>
/// Object to represent a connection.
/// </summary>
public class NetConnection
{
    /// <summary>
    /// Ip Address of the connection
    /// </summary>
    private string IPAddress;

    private int ConnectionID = 0;

    /// <summary>
    /// Creates a net Connection object.
    /// </summary>
    /// <param name="_connectionID"> Connection provided by unity. </param>
    public NetConnection(int _connectionID)
    {
        string ipAddress = "";
        int port = 0;
        UnityEngine.Networking.Types.NetworkID network;
        UnityEngine.Networking.Types.NodeID dstNode;
        byte error;
        NetworkTransport.GetConnectionInfo(NGNetworkManager.socketID, _connectionID, out ipAddress, out port, out network, out dstNode, out error);
        this.ConnectionID = _connectionID;
        this.IPAddress = ipAddress;
    }

    /// <summary>
    /// Gets the IP Address
    /// </summary>
    /// <returns> The Ip Address </returns>
    public string getIPAddress()
    {
        return IPAddress;
    }

    /// <summary>
    /// Gets the connection ID.
    /// </summary>
    /// <returns></returns>
    public int getConnectionID()
    {
        return ConnectionID;
    }

    public byte Send(byte[] _buffer, int _bufferSize, int _channel)
    {
        byte error;
        NetworkTransport.Send(NGNetworkManager.socketID, ConnectionID, _channel, _buffer, _bufferSize, out error);
        return error;
    }

    public byte Disconnect()
    {
        byte error;
        NetworkTransport.Disconnect(NGNetworkManager.socketID, ConnectionID, out error);
        return error;
    }

    public void Refresh()
    {
        string ipAddress = "";
        int port = 0;
        UnityEngine.Networking.Types.NetworkID network;
        UnityEngine.Networking.Types.NodeID dstNode;
        byte error;
        NetworkTransport.GetConnectionInfo(NGNetworkManager.socketID, ConnectionID, out ipAddress, out port, out network, out dstNode, out error);
        NGNetworkManager.LOG("CONNETIONID:" + ConnectionID);
        NGNetworkManager.LOG("ERROR:" + error);
        this.IPAddress = ipAddress;
    }
}
