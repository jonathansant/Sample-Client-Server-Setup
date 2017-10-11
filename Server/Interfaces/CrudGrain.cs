using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public abstract class CrudGrain<TGrainState> : Grain<TGrainState>, ICrudGrain<TGrainState> 
		where TGrainState : new()
    {
	    public Task<TGrainState> Get()
	    {
		    return Task.FromResult(State);
	    }

	    public Task Create(TGrainState initiState)
	    {
		    // TODO: add validation

		    State = initiState;
		    return WriteStateAsync();
	    }

	    public async Task<TGrainState> Delete()
	    {
		    await ReadStateAsync();
		    return State;
	    }
    }
}
