namespace Gameplay.Characters
{
    public interface ICharacterController
    {
        public void Attach(IMovable movable, IArmsMover armsMover);
        public void DetachCurrent();
        public void SetCanBending(bool enabled);
        public void SetCanMove(bool enabled);
        public void SetCanRotateArm(bool enabled);
    }
}
