using DI;

namespace Common.States
{
    public interface IStateMachine
    {
        DIContainer StateMachineContext {get;}
        public void ChangeState<TState>();
    }
}
