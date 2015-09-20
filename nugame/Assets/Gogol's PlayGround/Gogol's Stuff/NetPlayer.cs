
/// <summary>
/// This is a Player as seen by the server. 
/// </summary>
public class NetPlayer
{
    /// <summary>
    /// This is the actual connection to the other side.
    /// </summary>
   public NetConnection Connection = null;

    /// <summary>
    /// Tells the server if this person is the administrator.
    /// </summary>
    private bool IsAdmin = false;

    /// <summary>
    /// A Constructor for a net Player object.
    /// </summary>
    /// <param name="_conn"> The connection to the other side. </param>
    /// <param name="_isAdmin"> Weather or not the player is an admin on the server. </param>
    public NetPlayer(NetConnection _conn, bool _isAdmin = false)
    {
        this.Connection = _conn;
        this.IsAdmin = _isAdmin;
    }

    /// <summary>
    /// Sets if this player is an admin.
    /// </summary>
    /// <param name="_isAdmin"></param>
    public void setAdmin(bool _isAdmin)
    {
        IsAdmin = _isAdmin;
    }

    /// <summary>
    /// Gets if this player is an admin.
    /// </summary>
    /// <returns></returns>
    public bool getAdmin()
    {
        return IsAdmin;
    }
}

