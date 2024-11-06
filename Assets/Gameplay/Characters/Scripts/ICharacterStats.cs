namespace Gameplay.Characters
{
    public interface ICharacterStats
    {
        public float Acceleration {get;}
        public float WalkSpeed {get;}
        public float MinJumpHeight {get;}
        public float MaxJumpHeight {get;}
        public float ArmsSpeed {get;}
        public float MaxBendAngle {get;}
        public float BendingSpeed {get;}
    }
}
