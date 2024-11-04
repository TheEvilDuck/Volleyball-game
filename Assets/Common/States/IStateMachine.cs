namespace Common.States
{
    public interface IStateMachine
    {
        public void ChangeState<TState>();
    }
}
