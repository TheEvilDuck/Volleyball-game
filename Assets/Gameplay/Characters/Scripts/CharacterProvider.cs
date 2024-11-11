using Common;
using Gameplay.PositionProviding;
using UnityEngine;

namespace Gameplay.Characters
{
    public class CharacterProvider : ICharacterProvider, IResatable
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly IPositionProvider _characterStartPosition;
        private readonly float _scale;
        public Character Character {get; private set;}

        public ICharacterController Controller {get; private set;}

        public CharacterProvider(ICharacterFactory characterFactory, IPositionProvider startPosition, float scale = 1f)
        {
            _characterFactory = characterFactory;
            _characterStartPosition = startPosition;
            _scale = scale;
            SetupNewCharacter();
        }

        public void ResetInnerState()
        {
            if (Character == null)
                return;

            GameObject.Destroy(Character.gameObject);
            SetupNewCharacter();
        }

        private void SetupNewCharacter()
        {
            Character = _characterFactory.Get(_characterStartPosition, _scale);

            if (Controller != null)
                Controller.Attach(Character, Character);
        }

        public void SetController(ICharacterController controller)
        {
            Controller = controller;

            if (Character != null)
                Controller.Attach(Character, Character);
        }
    }
}
