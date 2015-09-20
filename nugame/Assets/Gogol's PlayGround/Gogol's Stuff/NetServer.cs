using UnityEngine.Networking;
using System.Collections.Generic;

public class NetServer
{
    /// <summary>
    /// The Players on the server.
    /// </summary>
    HashSet<NetPlayer> Players = null;

    /// <summary>
    /// The maximum number of players.
    /// </summary>
    public int MaxPlayers = 4;

    /// <summary>
    /// Regular server constructor.
    /// </summary>
    /// <param name="_maxPlayers"> Maximum number of players. </param>
    public NetServer(int _maxPlayers = 4)
    {
        this.MaxPlayers = _maxPlayers;
        this.Players = new HashSet<NetPlayer>();
    }

    /// <summary>
    /// Sends a message to a particular Client.
    /// </summary>
    /// <param name="_buffer"> The payload</param>
    /// <param name="_bufferSize">Size of the payload.</param>
    /// <param name="_Connection"> Connection to send the payload to.</param>
    /// <param name="_channel">The Channel to send the payload to.</param>
    public void SendMessage(byte[] _buffer, int _bufferSize, NetConnection _Connection, int _channel)
    {
        if (!NGNetworkManager.isInit || Players.Count == 0)
            return;

        _Connection.Send(_buffer, _bufferSize, _channel);
    }

    /// <summary>
    /// Send the same message to all clients. 
    /// </summary>
    /// <param name="_buffer">The payload.</param>
    /// <param name="_bufferSize">Size of the payload.</param>
    /// <param name="_channel">Channel to send it on.</param>
    public void SendToAll(byte[] _buffer, int _bufferSize, int _channel)
    {
        if (!NGNetworkManager.isInit || Players.Count == 0)
            return;

        foreach (NetPlayer player in Players)
        {
            player.Connection.Send(_buffer, _bufferSize, _channel);
        }
    }

    int recConnectionId;
    int recChannelId;
    byte[] recBuffer = new byte[1024];
    int bufferSize = 1024;
    int dataSize;
    byte error;

    int amt;

    /// <summary>
    /// Called once a frame, handles all the server updates.
    /// </summary>
    public void ServerUpdate()
    {
        NetworkEventType recNetworkEvent = Receive(out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);
        do {
            NGNetworkManager.LOG("{SERVER} Processing Event..." + (int)recNetworkEvent);
            switch (recNetworkEvent)
            {
                case NetworkEventType.Nothing:
                    break;

                // Someone connected to us.
                case NetworkEventType.ConnectEvent:
                    addPlayer(recConnectionId);
                    break;

                // We got game data.
                case NetworkEventType.DataEvent:
                    handleData(recConnectionId, recChannelId, recBuffer, dataSize);
                    break;

                // Someone disconneted from us.
                case NetworkEventType.DisconnectEvent:
                    removePlayer(recConnectionId);
                    break;
            }

        } while (recNetworkEvent != NetworkEventType.Nothing);
    }

    /// <summary>
    /// Adds a player to the server.
    /// </summary>
    /// <param name="connectionID"></param>
    public void addPlayer(int connectionID)
    {
        Players.Add(new NetPlayer(new NetConnection(connectionID), false));
        NGNetworkManager.LOG("{SERVER} Got a new Player!");
    }

    /// <summary>
    /// Removes a player from the server.
    /// </summary>
    /// <param name="connectionID"></param>
    public void removePlayer(int connectionID)
    {
        foreach(NetPlayer player in Players)
        {
            if (player.Connection.getConnectionID() == connectionID)
            {
                Players.Remove(player);
                return;
            }

        }
    }

    /// <summary>
    /// We got game data that we need to do something with.
    /// </summary>
    /// <param name="connectionID"> Who it was from </param>
    /// <param name="channelID">What channel it was recieved on. </param>
    /// <param name="buffer">The data </param>
    /// <param name="buffersize">size of the data. </param>
    public void handleData(int connectionID, int channelID, byte[] buffer, int buffersize)
    {

    }

    public NetworkEventType Receive(out int _connectionID, out int channelID, byte[] buffer, int buffersize, out int datasize, out byte error)
    {
        int recSocketID;
        NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recSocketID, out _connectionID, out channelID, buffer, buffersize, out datasize, out error);
        if(recSocketID != NGNetworkManager.socketID)
        {
            return NetworkEventType.Nothing;
        }
        return recNetworkEvent;
    }


}

