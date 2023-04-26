# Gamedevjs2023
Project repo for Gamedev.js Jam 2023 submission

# Project setup
- godot 3 .net - https://github.com/godotengine/godot/releases/download/3.5.2-stable/Godot_v3.5.2-stable_mono_win64.zip
- .net sdk x64 for running game - https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.203-windows-x64-installer
- git x64 - https://github.com/git-for-windows/git/releases/download/v2.40.0.windows.1/Git-2.40.0-64-bit.exe

# Core game loop
Wave based arena action game where the player races against the clock to collect resources and defeat as many enemies as possible before the alarm clock timer reaches 0.
Once the alarm is triggered, all enemies will be alerted and will swarm in on the player. Once the player defeats all enemies, they can move on to the next wave.
Enemies will drop currency that the player can then use in shops to buy health refills, items, and stat upgrades.
The game will be action/strategy, as the player will have to make a decision as to what will be the most effective route to take before the timer reaches 0.
- Game starts in a 'calm' phase where enemies will mostly be inactive and will not pursue the player unless triggered to do so
	- When a new wave loads, the player will be given an initial wide view of the entire arena, so that they can see where the enemies and items are and then decide where they should go next
	- At some half time point, a shop portal will appear on the map that the player can go to if they choose to do so
	- If the player is able to defeat all enemies before the timer reaches 0, then a portal to the next wave will appear and they will not have to go through the 'alert' phase for the current wave
	- If the timer hits 0, then the game will enter the 'alert' phase
- In the 'alert' phase, all of the enemies will swarm in on the player's position and attack
	- If the player's HP hits 0, then it will be Game Over
	- If the player is able to defeat all enemies , then a portal to the next wave will appear
	
# Controls
## Game Controls
- Movement: WASD or Arrow Keys
- Melee Attack: Space bar or Z or Left Click
- Ranged Attack: X or Right Click
- Use Current Item: C or E or Middle Click
- Pause Game: Enter or Esc
## Menu Controls
- Use mouse cursor and Left Click
