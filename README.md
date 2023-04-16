# Project 2 - Fish Tank Simulation

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: _Jennifer Pichardo_
-   Section: _01_

## Simulation Design

_A simulation of a peaceful fish tank. One group of fish will typically stick together in groups while other types will try to avoid each other._

### Controls

-   _List all of the actions the player can have in your simulation_
    -   Controls: Click with your mouse to feed your fish.

## _Agent 1: Flocking Fish_

_This type of fish will typically stick together in a group of the same kind. They will try to avoid and flee from the larger fish in the tank._

### _State 1: Avoid Agent 1_

**Objective:** Avoid the larger fish in the tank as much as possible.

#### Steering Behaviors

- _List all behaviors used by this state_
   - Agent 1 will flee and evade from the nearest Agent 2 in the tank.
- Obstacles - _Obstacles for this Agent would be the larger fish, and present decorations in the tank._
- Seperation - _This state sepearates by giving other fish of the same agent distance from each other_
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   - When this agent gets within range of Agent 2.
   
### _State 2: Acquire Food_

**Objective:** Try to get any food that is present in the tank

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _Obstacles for this Agent would be present decorations in the tank._
- Seperation - _At this state, this agent will avoid other fish. _
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   - When the player clicks to spawn food to feed the fish.

## _Agent 2: Larger Fish_

_This type of fish will typically want to be by itself and not be around fish of its own kind. Other smaller fish in the tank will avoid this agent._

### _State 1: Be by itself_

**Objective:** Keep as much distance from other larger fish as possible.

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _Present decorations in the tank and other Agent 2 are obstacles._
- Seperation - _At this state, it separates from other Agent 2 and keep distance from them._
   
#### State Transitions

- _List all the ways this agent can transition to this state_
    - When it comes close to other Agent 2 in the tank.
   
### _State 2: Acquire Food_

**Objective:** Try to get any food that is present in the tank

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _Obstacles for this Agent would be present decorations in the tank._
- Seperation - _At this state, this agent will keep distance from other fish. _
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   - When the player clicks to spawn food to feed the fish.

## Sources

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_

## Make it Your Own

- Will add my own art assets to the game.
- Will add some ambience sound to the game.
- My overall goal for this is to create a peaceful and calm aesthetic and ambience for the final product.

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

