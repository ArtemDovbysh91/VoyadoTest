using System.Threading.Tasks;

namespace VoyadoTest.Services;

public interface IGoogleSearch
{
    Task<float> Search(string query);
}