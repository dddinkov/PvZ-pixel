# Plants vs. Zombies Clone in pixelart style (Unity, C#)

A fan-made 2D tower defence game inspired by Plants vs. Zombies, built in Unity as a personal learning project.
Demo: https://dddinkov.itch.io/td-demo?secret=1U5PPubnxz430MBD7TOiy3Itekc

![Gameplay Screenshot](./description-resources/gameplay-1.png)
![Card Selection Screenshot](./description-resources/card-deck-1.png)

## Features

- **Card selection system** — choose your plants before each level starts
- **Drag-and-drop planting** — custom implementation using Unity's `IDragHandler`, `IPointerUpHandler`, `IPointerClickHandler`, and `IPointerDownHandler` interfaces
- **Sun economy** — suns fall from the sky and are produced by Sunflowers; collect them to plant
- **Wave spawner** — zombies spawn in configurable waves with increasing difficulty
- **Cooldown system** — card cooldowns animated with a cosine curve for a smooth feel

## Plant Almanac

| Plant | Description |
|-------|-------------|
| ![Peashooter](Assets/Sprites/Cards%20and%20slots/peashooter-card.png) | Shoots peas at zombies |
| ![Sunflower](Assets/Sprites/Cards%20and%20slots/sunflower-card.png) | Produces sun over time |
| ![Wallnut](Assets/Sprites/Cards%20and%20slots/wallnut-card.png) | Blocks zombies, has damaged states |
| ![Potato Mine](Assets/Sprites/Cards%20and%20slots/potato-mine-card.png) | Explodes on contact after arming |
| ![Dollie](Assets/Sprites/Cards%20and%20slots/dollie-card.png) | Poisons zombies on collision |
| ![Homing Thistle](Assets/Sprites/Cards%20and%20slots/homing-thistle-card.png) | Shoots thorns that follow the nearest zombie |

## Technical highlights

- Drag-and-drop built from scratch using four Unity pointer interfaces — no third-party plugins
- All pixel art assets created manually in [Pixilart](https://www.pixilart.com)
- All sound effects from [Pixabay](https://pixabay.com)

## Project structure

```
Assets/
├── Scripts/        # All C# game logic
├── Sprites/        # Pixel art assets
├── Animations/     # Animation clips and controllers
├── Prefabs/        # Reusable game objects
└── Scenes/         # Menu and game scenes
```

## Disclaimer

This is a fan-made learning project inspired by Plants vs. Zombies (EA/PopCap Games).  
All pixel art assets were hand-drawn from scratch.  
This project is not affiliated with or endorsed by EA or PopCap Games.  
Not for commercial use.
