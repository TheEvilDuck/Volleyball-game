namespace Common.States
{
    public class State : IState
    {
        protected readonly IStateMachine _stateMachine;
        public State(IStateMachine stateMachine) => _stateMachine = stateMachine;
        
        public virtual void Enter() {}

        public virtual void Exit() {}

        public virtual void Update(float deltaTime) {}
    }
}
