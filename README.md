
# SimpleBT - Simple Behavior Tree for Unity 6

SBT is a basic graph view tool to develop behaviors for AI focused on easy shareable behaviors with JsonUtility. 

**Note**: This is a tool developed as a final assignment for University in 2025. This asset comes with a minimal set of features.






## What are Behavior Trees?

Behavior Trees (shortened as “BTs”) are trees of hierarchical nodes that control the flow of decision making of an AI entity. Compared to FSMs these do not require programmed transitions between states, but rather flows from one node to another in a branching tree fashion.

This flow is decided by the interaction between the **composite** nodes, **decorator** nodes and **execution** nodes. Composite nodes can branch and determine the order in which their child nodes are executed, decorator nodes alter the result of their child node and execution nodes run actions (like moving to a new position) or conditions (checking if something is true or not).

Ticking is an essential part of this model, and every time the behavior ticks (updates) it has to return 1 of 3 states: Success, Failure or Running.

For further information you can read *Michele Colledanchise and Petter ¨Ogren"'s Behavior Trees in Robotics and AI An Introduction* here: https://arxiv.org/pdf/1709.00084
## How to Install the Package

Work in progress

## Creating a Behavior

First, open the window located at the top bar called *SimpleBT*->*Window*.

![SBTEditorWindow](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/SBTEditorWindow%20Window.png?raw=true)

Once open you can write a new name for the behavior  and start adding nodes by pressing **space** or **right-clicking**->**Create Node**. A search window will show up.

**Note**: 
- It is required to create a Root node first before any other, and there cannot be more than 1. If you try to add another you'll get a popup
- Behavior names **can only contain** numbers and '_' characters

![SBTSearchWindow](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/SBTSearchWindow.png?raw=true)

![Populating the Behavior](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/BehaviorExample.png?raw=true)

At any point you can press the button **Save** to save the behavior onto a JSON file which will be located in *Assets->SimpleBT->GraphData*. 

**To load the behavior** you can:
- Type the name manually first (Not recommended)
- Select the JSON File (.simple) (Recommended)

## Using the Blackboard

To create a new variable press the **+** icon, you can rename it by double-click. Modify its value type or content by accessing the expanded section (">" icon on the left).

![Blackboard](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Blackboard.png?raw=true)

To delete a variable select the respective field and press **delete** (not backspace).

### Currently Supported Types

| Type       | Syntax                                 | Blackboard Method                 |
|------------|----------------------------------------|-----------------------------------|
| int        | 3                                      | GetValue<<Type>**int**>               |
| float      | 3.2                                    | GetValue<<Type>**float**>             |
| bool       | true                                   | GetValue<<Type>**bool**>              |
| string     | something                              | GetValue<<Type>**string**>            |
| Vector2    | 3, 3.9                                 | GetValue<<Type>**Vector2**>           |
| Vector3    | 21.1, 3, 90                            | GetValue<<Type>**Vector3**>           |
| GameObject | Tomato, Untagged, 391290 (Instance id) | GetComplexValue<<Type>**GameObject**> |

**Note**: Currently supported types *in* the SBTBlackboardGraph, any type can be store on the Blackboard's dictionary at behavior runtime.

## Generating the Tree and Blackboard

To generate the tree select a GameObject and then press "*Generate Tree & Blackboard*". If the behavior has changed you can press *"Regenerate*".
If you want to get rid of the components you can press "*Remove Components*".

![Generation](https://i.imgur.com/lhvxWX6.png)

**Note**: 
- To properly generate it you **must save beforehand**.
- After generation you can modify the values by hand on each ScriptableObject if they contain values (Not recommended, will be overwritten on regeneration)

## Creating Custom Nodes

WIP

Currently there is no automatic process that creates all the classes for you, so it requires a bit of manual labor to implement.

### Creating a new Graph Node
### Creating a new Non-Graph Node
### Adding the Graph Node to the Search Window

## Available Nodes

These are the current available notes split in categories.

### Core
| Name  | Description |
| ------------- | ------------- |
| Root  | Acts as the tree root  |
| Condition  | Compares numerical or object values  |

### Actions
| Name  | Description |
| ------------- | ------------- |
| SetActive  | Disables or enables the GameObject |
| SetActive (Toggle)  | Toggles the active state of the GameObject  |
| Wait  | Waits a specific amount of time  |
| Debug  | Prints a message, can choose if Succeeds or Fails after  |

### Composites
| Name  | Description |
| ------------- | ------------- |
| Sequence  | Ticks each children in order, fails if one fails, succeeds if all succeed  |
| Selector  | Ticks each children in order, succeeds if one succeeds, fails if all fail  |

### Decorators
| Name  | Description |
| ------------- | ------------- |
| Repeat Forever  | Always returns "Running", preventing the behavior from finishing |

## Future Updates
- Implement Behavior Node (For behavior nesting)
- Implement Unity related Action Nodes
- Implement Repeat Until Failure, Repeat Until Success, Repeat X Times *decorators*
- Implement Parallel, Random *Sequences* and *Selectors*.

## Known Issues

Pending

## Special Thanks

- Indie Wafflus on “Unity Dialogue System - Creating the Search Window”: https://www.youtube.com/c/IndieWafflus
- Mert Kirimgeri: UNITY DIALOGUE GRAPH TUTORIAL - Variables and Search Window: https://youtu.be/F4cTWOxMjMY?si=_NuLrvaJIA7bOOhd

## License

MIT License

Copyright (c) [2025] [RuDev]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
