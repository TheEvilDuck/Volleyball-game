using System.Collections.Generic;
using Common;
using Common.PlayerInput;
using Gameplay.Characters;
using Gameplay.PositionProviding;

namespace Gameplay.GameStates
{
    public class TeamStats : ITeamStats, IResatable
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly List<ICharacterProvider> _characters;
        private readonly List<IResatable> _resetables;
        private readonly float _charactersScale;
        private int _currentServer = 0;
        public int Score {get; private set;}
        public IReadOnlyList<ICharacterProvider> Characters => _characters;
        public ICharacterProvider CurrentServer => _characters[_currentServer];

        public TeamStats(ICharacterFactory characterFactory, float charactersScale = 1)
        {
            _characterFactory = characterFactory;
            _characters = new List<ICharacterProvider>();
            _resetables = new List<IResatable>();
            _charactersScale = charactersScale;
        }

        public void AddScore() => Score++;
        public void ResetScore() => Score = 0;
        public ICharacterProvider CreatePlayer(IPositionProvider positionProvider, IPlayerInput playerInput)
        {
            CharacterProvider characterProvider = new CharacterProvider(_characterFactory, positionProvider, _charactersScale);
            PlayerInputCharacterController playerInputCharacterController = new PlayerInputCharacterController(playerInput);
            playerInputCharacterController.Attach(characterProvider.Character, characterProvider.Character);
            _characters.Add(characterProvider);
            _resetables.Add(characterProvider);
            return characterProvider;
        }

        public void SwitchServerToNext()
        {
            _currentServer++;

            if (_currentServer >= _characters.Count)
                _currentServer = 0;
        }

        public void ResetInnerState()
        {
            foreach (IResatable resatable in _resetables)
                resatable?.ResetInnerState();
        }
    }
}
