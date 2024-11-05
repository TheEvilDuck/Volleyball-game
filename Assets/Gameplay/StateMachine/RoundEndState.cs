using Common.States;
using UnityEngine;

namespace Gameplay.States
{
    public class RoundEndState : State
    {
        public RoundEndState(IStateMachine stateMachine) : base(stateMachine)
        {
            
        }

        public override void Enter()
        {
            _stateMachine.ChangeState<ServeState>();
        }

        public override void Exit()
        {
            
        }
    }
}
