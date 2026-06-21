# Sky Roller — Assignment 5: Endless Survival

A 3D rolling-ball endless runner built in Unity, extended from the Sky Roller module skeleton.

## How to Play

- **WASD / Arrow Keys** — Steer the ball left/right (it rolls forward automatically)
- Survive as long as possible — the distance you travel is your score
- Avoid **spikes** — instant game over
- Avoid **conveyor belts** — push you sideways, risking a fall
- Avoid **slow zones** — temporarily cut your speed, making reactions harder
- Falling off any platform ends the run
- Press **Restart** on the Game Over screen to try again

## New Systems (Assignment 5)

### Procedural Platform Generation — `PlatformGenerator.cs`
- Spawns new platform sections ahead of the player as they move forward
- Removes old sections once they're far enough behind the player
- Picks randomly from an array of platform prefabs (at least 4 required)
- Keeps the scene from filling up indefinitely

### Platform Prefabs (4+)
| Prefab | Description |
|---|---|
| `Platform_Straight` | Plain straight platform, no hazard |
| `Platform_Narrow` | Narrower platform — harder to stay on |
| `Platform_Spikes` | Straight platform with a Spike hazard |
| `Platform_Conveyor` | Straight platform with a Conveyor hazard |
| `Platform_SlowZone` | Straight platform with a Slow Zone hazard (bonus 5th variant) |

### Hazard System (3 types besides falling)
- **SpikeHazard.cs** — instant game over on touch
- **ConveyorHazard.cs** — pushes the player sideways while standing on it (affects control)
- **SlowZoneHazard.cs** — temporarily reduces forward speed (affects movement), reuses the existing `ActivateSpeedBoost` mechanic from the module

### Survival Score UI — `GameManager.cs`
- Tracks distance traveled (player's forward Z position) as the survival score
- Displayed live on screen as "Distance: Xm"
- On Game Over, shows the final distance survived

### Lose Condition & Restart — `GameManager.cs` + `DeathZone.cs`
- Falling into the `DeathZone` (or touching a `SpikeHazard`) calls `GameManager.GameOver()`
- Game Over freezes the game (`Time.timeScale = 0`), locks player controls, and shows a Game Over panel
- A Restart button calls `GameManager.RestartGame()`, which reloads the scene

## Scripts

| File | Purpose |
|------|---------|
| `GameManager.cs` | Survival score tracking, UI updates, game over flow, restart |
| `PlatformGenerator.cs` | Endless procedural platform spawning/despawning |
| `SpikeHazard.cs` | Instant-lose hazard |
| `ConveyorHazard.cs` | Sideways-push hazard (control) |
| `SlowZoneHazard.cs` | Speed-reduction hazard (movement) |
| `DeathZone.cs` | Updated to call `GameManager.GameOver()` instead of instantly reloading |
| `PlayerMovement.cs` | Updated with `externalSidePush` (for conveyors) and `LockControls()` (for game over) |
| `CameraFollow.cs` | Unchanged — follows the player ball |

## Scene Setup (do this in the Unity Editor)

1. **Delete (or keep aside)** the hand-placed `Level` platform blocks in `GameScene` — the `PlatformGenerator` will build the level at runtime instead. You can keep a short starting platform manually if you want a safe spawn area.
2. **Build the platform prefabs** (Straight, Narrow, Spikes, Conveyor, SlowZone) out of stretched Cubes for the walkable surface, with the relevant hazard script + trigger Collider attached for the hazard variants. Save each as a prefab in `Assets/Prefabs/Platforms`.
3. **Create a `PlatformGenerator` empty GameObject**, add the `PlatformGenerator` script, assign the Player transform and all platform prefabs.
4. **Create a `GameManager` empty GameObject**, add the `GameManager` script, assign the Player transform.
5. **Build the UI Canvas**: a `ScoreText` (TextMeshPro) always visible, and a `GameOverPanel` (inactive by default) containing a `FinalScoreText` and a `Restart` Button wired to `GameManager.RestartGame()`.
6. Assign `scoreText`, `gameOverPanel`, and `finalScoreText` on the `GameManager` component.
