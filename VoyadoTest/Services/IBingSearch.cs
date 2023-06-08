using System.Threading.Tasks;

namespace VoyadoTest.Services;

public interface IBingSearch
{
    Task<float> Search(string query);
}