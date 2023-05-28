using HotelApi.model_s;

namespace HotelApi.Repository
{
    public interface IServer
    {
        IEnumerable<ServersDetails> GetServers();
        ServersDetails GetServerById(int id);
        ServersDetails PostServer(ServersDetails server);
        ServersDetails PutServer(int id, ServersDetails server);
       

    }
}
