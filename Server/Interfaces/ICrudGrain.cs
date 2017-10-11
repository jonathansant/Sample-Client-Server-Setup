using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface ICrudGrain<TGrainState> : IGrainWithStringKey
    {
	    Task<TGrainState> Get();

	    Task Create(TGrainState initiState);

	    Task<TGrainState> Delete();
    }
}
