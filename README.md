
# SimpleBT - Simple Behavior Tree for Unity 6

SBT is a basic graph view tool to develop behaviors for AI focused on easy shareable behaviors with JsonUtility. 

**Note**: This is a tool developed as a final assignment for University in 2025. This asset comes with a minimal set of features.






## What are Behavior Trees?

Behavior Trees (shortened as “BTs”) are trees of hierarchical nodes that control the flow of decision making of an AI entity. Compared to FSMs these do not require programmed transitions between states, but rather flows from one node to another in a branching tree fashion.

This flow is decided by the interaction between the **composite** nodes, **decorator** nodes and **execution** nodes. Composite nodes can branch and determine the order in which their child nodes are executed, decorator nodes alter the result of their child node and execution nodes run actions (like moving to a new position) or conditions (checking if something is true or not).

Ticking is an essential part of this model, and every time the behavior ticks (updates) it has to return 1 of 3 states: Success, Failure or Running.

For further information you can read *Michele Colledanchise and Petter ¨Ogren"'s Behavior Trees in Robotics and AI An Introduction* here: https://arxiv.org/pdf/1709.00084
## How to Install the Package

Head over the "releases" section and download the unity package. Open your Unity 6 project and then open the package, select all files and import.

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

**Note**: Currently supported types *in* the SBTBlackboardGraph, any type can be stored on the Blackboard's dictionary at behavior runtime.

## Generating the Tree and Blackboard

To generate the tree select a GameObject and then press "*Generate*". If the behavior has changed you can press *"Save & Regenerate*".
If you want to get rid of the components you can press "*Remove Components*".

![Generation](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Saving%20and%20Regenerating.png?raw=true)

## Available Nodes (In Sections)

<details>
<summary>Ungrouped</summary>

| Name   | Description                                                                                                        |
|--------|--------------------------------------------------------------------------------------------------------------------|
| Root   | Acts as the tree root <br/> Must be added first before any other node                                              |
| Branch | Behavior Tree as a node <br/> Recommended for behavior branching |

</details>


<details>
<summary>Actions</summary>

| Name                      | Description                                                                                               | Section       |
|---------------------------|-----------------------------------------------------------------------------------------------------------|---------------|
| Always Succeed            | Self explanatory                                                                                          | General       |
| Send Message              | Calls the specified method with no parameters on all scripts attached to the GameObject                   | General       |
| Override Tag              | Self explanatory                                                                                          | General       |
| Parent GameObject to Self | Uses *.SetParent()* on the target GameObject                                                              | General       |
| Unparent GameObject       | Uses *.SetParent(null)* on the target GameObject                                                          | General       |
| Destroy GameObject        | Self explanatory                                                                                          | General       |
| Set Active                | Sets the target GameObject to active or disabled.                                                         | General       |
| Set Active (Toggle)       | Toggles the target GameObject's active status                                                             | General       |
| Store Random Position 3D  | Raycasts with additional height and a random range into the ground. <br/>A Vector2 position can be stored | General       |
| Stop                      | Rigidbody / Rigidbody 2D linear velocity is set to 0                                                      | Movement      |
| Follow 2D                 | Follows a target and can Ignore the X or Y axis <br/>Can toggle between transform or Rigidbody2D          | Movement / 2D |
| Flee 2D                   | Moves in the opposite direction of Follow2D                                                               | Movement / 2D |
| Go To Position 2D         | Goes to a Vector 2 position and can ignore the X or Y axis                                                | Movement / 2D |
| Linear Move 2D            | Continuously moves the GameObject in a linear velocity                                                    | Movement / 2D |
| Follow 3D                 | Same as Follow2D but can also ignore the Z axis                                                           | Movement / 3D |
| Flee 3D                   | Same as Flee2D but can also ignore the Z axis                                                             | Movement / 3D |
| Go To Position 3D         | Same as GoToPosition3D but can also ignore the Z axis                                                     | Movement / 3D |
| Wait X Seconds            | Self explanatory                                                                                          | Movement / 3D |

</details>


<details>
<summary>Conditions</summary>

| Name                   | Description                                                  |
|------------------------|--------------------------------------------------------------|
| Always Succeed         | Self explanatory                                             |
| Always Fail            | Self explanatory                                             |
| Comparison             | Compares between numbers and if the "A" value is null or not |
| Is Near Ledge 2D       | Checks if the gameObject is near a ledge in 2D               |
| Is GameObject Close 2D | Checks if the gameObject's target is close enough in 2D      |
| Is GameObject Close 3D | Checks if the gameObject's target is close enough in 3D      |

</details>


<details>
<summary>Composites</summary>

| Name              | Description                                                                                                                                                                                                  |
|-------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Sequence          | Ticks each children in order, fails if one fails, succeeds if all succeed                                                                                                                                    |
| Selector          | Ticks each children in order, succeeds if one succeeds, fails if all fail                                                                                                                                    |
| Parallel Sequence | Ticks all children in parallel, if any fail all fail                                                                                                                                                         |
| Random Sequence   | Selects a random children to tick until all are ticked (Follows Sequence Logic)                                                                                                                              |
| Priority          | Prioritizes nodes on the left and aborts lesser important nodes <br/> Requires Composites as children and each composite must contain a Condition <br/>AlwaysSucceed recommended to the least important node |

</details>


<details>
<summary>Decorators</summary>

| Name           | Description |
|----------------| ------------- |
| Repeat Forever | Always returns "Running", preventing the behavior from finishing |

</details>

<details>
<summary>Blackboard</summary>

| Name                   | Description      |
|------------------------|------------------|
| Remove Key             | Self explanatory |
| Invert Numerical Value | Self explanatory |

</details>

<details>
<summary>Other</summary>

| Name  | Description                                                                             |
|-------|-----------------------------------------------------------------------------------------|
| Debug | Sends a *Debug.Log()* message <br/> Can choose between Succeed or Failure after the log |

</details>

</details>

<details>
<summary>Custom</summary>

This section is exclusive for your custom nodes.

</details>

## Creating Custom Nodes

1. Right-click on the graph view to open the contextual menu
2. Select "Create Custom Node"

![Step 1 & 2](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Create%20Custom%20Node%20Step%201_1.png?raw=true)

3. Choose type, give it a name and select "Create"

![Step 3](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Create%20Custom%20Node%20Step1_X.png?raw=true)

4. Modify the *Title* and *ClassReference* parameters inside the constructor, then add your logic

![Step 4](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Example%20GraphAction.png?raw=true)

5. (If no CustomEntrySO exists) Right-click on the graph view's contextual menu and select "Create Custom Entries SO",
6. Open your Custom Entries Scriptable Object script and add a new entry inside the *GetEntries()* method. Userdata must start with "Custom_"

![Step 6](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Example%20Entries.png?raw=true)

7. Create an asset from your Custom Entries SO and drag it to the "Custom Entries SO" Object Field.

![Step7](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Custom%20Entries.png?raw=true)

**If your class reference is the same name as your non-editor node it should appear on the graph view!**

## Future Updates
- Implement Repeat Until Failure, Repeat Until Success, Repeat X Times *decorators*
- Implement Parallel, Random *Sequences* and *Selectors*.
- Modify Node Styling

## Known Issues

- There is no safeguard if you save accidentally

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
