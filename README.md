# 🎮 Ankus Axos - 2D Puzzle Platform Game

## 📋 About The Project

![XErrWopZDD-1748099256](https://github.com/user-attachments/assets/63877c7b-7e65-441c-9bac-e248dc86e644)
![6GLxJpQNfN-1748099257](https://github.com/user-attachments/assets/ba7e9c6a-08ee-43fb-81d7-94502e2b5a08)
![o2TBn825y2-1748099258](https://github.com/user-attachments/assets/2b857d33-cef2-4817-95bb-741e37d95d64)

**Ankus Axos** is a 2D puzzle-platform game developed during the Ankara University ANKU Game Jam. Players must strategically place limited objects to rescue axolotls while racing against time.

> The main character distributes their own lifetime to placed objects. The goal is to rescue all axolotls on the map by ensuring neither the character's lifetime nor the lifetimes of the placed objects run out before the rescue is complete. This unique mechanic challenges players to manage time and resources efficiently to complete each level.

## Game Link: [Ankus Axos](https://fiuby.com/games/axolotl-ascension)

### 🏆 Development Team
- **Kelami Batuhan Bölükbaşı** - Backend Developer & Scripting
- **Metehan Çavdar** - Team Lead
- **Duhan Demir** - Developer & Designer
- **Habibe Kekik** - Artist
- **Sama Orucova** - Developer

---

## 🎯 Game Mechanics

### Core Features
- **Object Placement System**: Players can place limited platforms and obstacles
- **Time Management**: Each level has a specific time limit
- **Axolotl Rescue**: Complete levels by collecting target number of axolotls
- **Dynamic Camera**: Pre-game map exploration with scroll functionality
- **Audio System**: Object placement and animation sound effects

### Advanced Systems
- **Timer System**: Countdown with visual warnings in final 5 seconds
- **Level Manager**: Level completion and transition management
- **Pause System**: Game pause and menu navigation
- **Camera Follow**: Pixel-perfect camera tracking system

---

## 🛠️ Technical Features

### Development Environment
- **Unity 2022.3 LTS**
- **C# .NET Framework**
- **2D Universal Render Pipeline**

### Design Patterns Used
- **Singleton Pattern**: For Timer and Manager classes
- **Component Pattern**: Modular game system architecture
- **Observer Pattern**: For UI updates
- **State Machine**: Game state management

### Performance Optimizations
- **Object Pooling**: For particle systems
- **Coroutine Usage**: Asynchronous timer operations
- **Event-Driven Architecture**: Loosely coupled system design

---

## 📁 Code Architecture

### Core Systems
```
GameManager.cs          - Main game flow and state management
Timer.cs               - Time system and countdown functionality
LevelManager.cs        - Level transitions and result screen
AxolotlManager.cs      - Target tracking and level completion
```

### Player & Camera
```
PlayerController.cs    - Character movement and physics
CameraFollow.cs       - Dynamic camera tracking
CameraControlScrool.cs - Pre-game camera controls
```

### Object Management
```
PlaceObjectManager.cs  - Object placement system
PlaceObjectButton.cs  - UI button management
AssignTimePanel.cs    - Object timer assignment
```

### UI & UX
```
MainMenu.cs           - Main menu navigation
PauseManager.cs       - Game pause system
CounterText.cs        - UI counter systems
```

### Utilities
```
Dead.cs               - Restart trigger system
Lake.cs               - Level completion trigger
SkipAndReloadScene.cs - Debug and testing tools
```

---

## 🎨 Featured Code Examples

### Dynamic Timer System
```csharp
private IEnumerator TotalTimerRoutine()
{
    while (totalTime > 0)
    {
        if (countdownText != null && totalTime <= 5)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = Mathf.CeilToInt(totalTime).ToString();
        }
        
        yield return new WaitForSeconds(1f);
        totalTime -= 1f;
    }
    
    CompleteLevel();
}
```

### Smart Object Placement
```csharp
public void OnButtonClick()
{
    if (currentCount <= 0) return;
    
    CreateGhostObject();
    currentCount--;
    
    if (currentCount <= 0)
        myButton.interactable = false;
}
```

### Pixel-Perfect Camera
```csharp
void LateUpdate()
{
    if (canFollow && target != null)
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, 
                                               target.position + offset, smoothSpeed);
        
        // Pixel snapping
        float ppu = 100f;
        smoothedPosition.x = Mathf.Round(smoothedPosition.x * ppu) / ppu;
        smoothedPosition.y = Mathf.Round(smoothedPosition.y * ppu) / ppu;
        
        transform.position = smoothedPosition;
    }
}
```

---

## 🚀 Technical Skills

### Programming Skills
- ✅ **C# Advanced**: LINQ, Generics, Events, Delegates
- ✅ **Unity Engine**: Component system, Coroutines, ScriptableObjects
- ✅ **Design Patterns**: Singleton, Observer, State Machine
- ✅ **Performance Optimization**: Object pooling, Memory management

### Game Development
- ✅ **2D Game Mechanics**: Physics, Collision Detection, Animation
- ✅ **UI/UX Programming**: Dynamic interfaces, Responsive design
- ✅ **Audio Integration**: Sound effects, Music management
- ✅ **Camera Systems**: Follow cameras, Smooth transitions

### Project Management
- ✅ **Version Control**: Git, GitHub collaboration
- ✅ **Code Architecture**: Modular, Maintainable, Scalable
- ✅ **Documentation**: Clean code, Comments, README
- ✅ **Team Collaboration**: Agile methodology, Code review

---

## 📊 Project Statistics

- **Total Lines of Code**: ~2000+ lines
- **Number of Scripts**: 23 C# files
- **Development Time**: 48 hours (Game Jam)
- **Platform**: PC (Windows, macOS, Linux)

---

## 🎯 Technologies Learned

Key skills gained during this project:

- **Unity Engine** professional game development
- **C#** object-oriented programming
- **Game Physics** and collision detection
- **UI Programming** and user experience design
- **Performance Optimization** techniques
- **Team Collaboration** and version control

---

## 🌟 Future Developments

- [ ] Mobile platform support
- [ ] Multiplayer functionality
- [ ] Level editor system
- [ ] Achievement system
- [ ] Cloud save integration

---

## 📞 Contact

**Kelami Batuhan Bölükbaşı**  
Backend Developer & Unity Programmer

- 📧 Email: [batuhankelami@gmail.com]
- 💼 LinkedIn: [Batuhan Bölükbaşı](https://www.linkedin.com/in/batuhan-b%C3%B6l%C3%BCkba%C5%9F%C4%B1-45b2b726b/)
- 🐙 GitHub: [Kelami Batuhan](https://github.com/KBatuhanB)

---


*This project demonstrates my technical skills and problem-solving abilities in game development as a portfolio work.*
