using DogGo.Models;

namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        //List<Walk> GetAllWalks();
        Walk GetWalkById(int id);

        List<Walk> GetWalksByWalkerId(int walkerId);
    }
}
