namespace Gameplay.Characters
{
    public interface ICharacterProvider
    {
        public Character Character {get;}
        public ICharacterController Controller {get;}

        public void SetController(ICharacterController controller);
    }
}
