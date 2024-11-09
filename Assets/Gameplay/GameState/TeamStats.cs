using System.Collections.Generic;
using Common.PlayerInput;
using Gameplay.Characters;
using Gameplay.PositionProviding;

namespace Gameplay.GameStates
{
    public class TeamStats : ITeamStats
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly List<ICharacterProvider> _characters;
        public int Score {get; private set;}
        public IReadOnlyList<ICharacterProvider> Characters => _characters;

        public TeamStats(ICharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
            _characters = new List<ICharacterProvider>();
        }

        public void AddScore() => Score++;
        public void ResetScore() => Score = 0;
        public ICharacterProvider CreatePlayer(IPositionProvider positionProvider, IPlayerInput playerInput)
        {
            CharacterProvider characterProvider = new CharacterProvider(_characterFactory, positionProvider);
            PlayerInputCharacterController playerInputCharacterController = new PlayerInputCharacterController(playerInput);
            playerInputCharacterController.Attach(characterProvider.Character, characterProvider.Character);
            _characters.Add(characterProvider);
            return characterProvider;
        }
    }
}
