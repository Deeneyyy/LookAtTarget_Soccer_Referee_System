# **LookAtTarget: Soccer Referee System**

## **Overview**
This Unity script implements a dynamic targeting and movement system designed for soccer referee behavior in a game. It features:

1. **Central Referee Behavior**: 
   - A referee object orbits a target (e.g., the soccer ball or players) in a circular path.
   - Obstacle avoidance is included to ensure smooth movement.

2. **On-line Referee Behavior**: 
   - Two additional referees move dynamically along the **X-axis** and **Z-axis**, staying aligned with the current active target (e.g., the soccer ball or a player).

3. **Dynamic Target Switching**: 
   - The system allows you to switch between targets (soccer ball or players) dynamically using the `Tab` key.
   - Active targets are visually highlighted using different materials.

---

## **Features**
- **Circular Orbit**: 
  - The central referee orbits around the active target in a circular path with customizable radius.
- **Obstacle Avoidance**: 
  - Ensures the central referee avoids obstacles on the field dynamically.
- **Dynamic Target Switching**: 
  - Targets can be cycled through using input (`Tab` key) for dynamic gameplay.
- **Linear Movement**: 
  - On-line referees (sideline referees) move along the **X-axis** or **Z-axis**, keeping themselves aligned with the active target.
- **Visual Indicators**: 
  - Active targets are highlighted using a specific material to distinguish them.

---

## **How It Works**

### 1. **Central Referee (Orbiting Object)**
- Orbits the active target in a circular motion based on user-configured minimum and maximum radius.
- Adjusts its position dynamically to avoid obstacles in its path using raycasting.
- Always faces the active target, simulating the referee's focus on the ball or player.

### 2. **On-line Referees (Follower Objects)**
- Two referees are positioned along the X-axis and Z-axis, moving only along their respective axes.
- Their movement is constrained to the axis and aligned with the active target's position.

### 3. **Target Switching**
- Use the `Tab` key to cycle through multiple targets (soccer ball, players, etc.).
- Active targets are highlighted with a unique material for easy identification.

---

## **Usage**

### **Setup Instructions**
1. **Attach the Script:**
   - Attach the `LookAtTarget` script to any empty GameObject in your scene.

2. **Assign the Orbiting Object:**
   - Drag the central referee GameObject (orbiting object) into the `OrbitingObject` field.

3. **Assign Followers:**
   - Drag the sideline referees (followers) into the `FollowerX` and `FollowerZ` fields.

4. **Set Targets:**
   - Add all potential targets (e.g., soccer ball, players) into the `Targets` array.

5. **Assign Materials:**
   - Assign materials for the active and inactive states to visually distinguish the active target.

6. **Configure Parameters:**
   - Customize parameters for:
     - **Orbiting Speed** (`OrbitSpeed`)
     - **Radius Adjustment Speed** (`RadiusAdjustSpeed`)
     - **Obstacle Avoidance** (`ObstacleDetectionDistance`, `ObstacleAvoidanceStrength`)
     - **Follower Speed** (`FollowSpeed`)

---

## **Key Bindings**
| Action             | Key   |
|--------------------|-------|
| Cycle Target       | Tab   |

---

## **Inspector Configuration**
| Field                     | Description                                                                                         |
|---------------------------|-----------------------------------------------------------------------------------------------------|
| `Targets`                 | Array of objects to target (e.g., ball, players).                                                  |
| `ActiveMaterial`          | Material to indicate the active target.                                                            |
| `InactiveMaterial`        | Material to indicate inactive targets.                                                             |
| `OrbitingObject`          | Central referee object orbiting the target.                                                        |
| `FollowerX`               | Referee moving along the X-axis.                                                                   |
| `FollowerZ`               | Referee moving along the Z-axis.                                                                   |
| `MinRadius`               | Minimum orbit radius for the central referee.                                                      |
| `MaxRadius`               | Maximum orbit radius for the central referee.                                                      |
| `OrbitSpeed`              | Speed of orbiting motion.                                                                          |
| `RadiusAdjustSpeed`       | Speed of radius adjustment for the orbiting referee.                                               |
| `ObstacleDetectionDistance` | Distance for detecting obstacles in front of the central referee.                                  |
| `ObstacleAvoidanceStrength` | Strength of the avoidance vector to bypass obstacles.                                             |
| `FollowSpeed`             | Speed at which on-line referees move to follow the target.                                         |

---

## **Code Highlights**

### **1. Orbiting the Target**
```csharp
void OrbitAroundTargetWithAvoidance(Transform target)
{
    // Orbit logic including radius adjustment and obstacle avoidance
    ...
    orbitingObject.position = Vector3.MoveTowards(orbitingObject.position, desiredPosition, orbitSpeed * Time.deltaTime);
    orbitingObject.LookAt(target);
}
