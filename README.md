
# Ghostibles
**IMPORTANT NOTE: The updated Output files are located under the Beta release tag. The assets are updated in the branch**
## Alpha Release Design Update
* We introduced new resources for the player, which are minor gems. These are  dispersed across the map as well as protected by ghosts. They provide additional health. This aids the character prior to encountering the boss. 
* Ghosts now have designated areas in which they are patrolling. There are 7 areas that have at least one ghost. This indicates more ghosts as well as more waypoints that ghosts use to patrol. This increases the difficulty of the game as well as creates a cohesive environment where ghosts are limited to areas. 
* There is an additional way to collect ammunition outside of destroyed ghosts. These are also dispersed across the map as well as protected by ghosts. This aids the character prior to encountering the boss. 
## UI Design
* We have included a new way to visualize health and the amount of ammunition you have left. This is indicated by bars and colors change when you change the amount of health/ammunition you have. This is easy to interpret when playing. 
* We have included a way to visualize weapon selection as well as how to use it. They are located in the lower left side of the screen for easy viewing access. 
* There is an end screen to indicate the result of the gameplay. This allows players to see when they win/lose. 
## Sound Design 
* We have included different pieces of background music to navigate throughout the map. This includes a thunderstorm, ambient haunted house music, the boss level, as well as end screen music. This builds onto the atmosphere of the game. 
* Amy, our main character, has reactions to events that happen throughout the game. This primarily includes collecting items, and getting hurt by ghosts. You are also able to hear Amy's movements throughout the map. We can better understand Amy as a character. 
* There are also sounds when those items are collected to provide feedback on collision. 
## Shader Design and Alpha Release Modifications
From the feedback that we received from the Alpha release, we implemented the following changes into the Beta release:
* We changed the colors of the gems to be more drastic in order to clearly differentiate the purpose of each gem. The red one for health and the green one to collect for the objective. 
* We added a timing limitation for the lyre. This was great feedback as we wanted to create a need for each of the weapons as well as ensure that neither weapon was too strong. 
* We did fix the issue with Amy being able to excessively jump and limit it to 3 jumps. It created an unnecessary hack within the game that would reduce the overall gameplay. 
* We readjusted the cannon in the way that it shot the bullets to line up with the animation placement for clear attack from Amy. 
* We contributed to the atmosphere by implementing a night time skybox as well as adding a roof to complete the haunted house. 
* We added the resources onto our introduction screen to clarify what each resource on the map represented. 
* The ghost representation was one of the game objects on the map that was considered to change the shader. This was to identify them as ghosts instead of other evil looking creatures. 

The following are differences from the Alpha release due to shader construct implementations as well as additional fixes: 
* We created an introductory menu for style and to outline a central area to play the game. This will also introduce the weapons as a tutorial for the players.
* We created an objective screen to inform the player on what their main goal is to do within the game in order to win. 
* We created a credit screen in order to give credit to all the art and sound resources we utilized to construct our game. 
* (Shader construct) We implemented a shader to have the ghosts show some transparency and appear more like a ghost. 
* (Shader construct) We implemented a shader to visualize Amy’s second weapon: the lyre. It is used to stun the ghosts so now we can visually see where her attack spans. 
* (Shader construct) We also implemented a shader for the skybox to make it appear night time and contribute to the atmosphere. 
* The audio stopped looping after the track was over. We wanted to keep the suspense going until the end of the game, so we edited the audio source to continue to loop.
* The ghost was able to follow the ghost into the air. Though ghosts could have the ability to float, we wanted to limit their capabilities as a tactic for players to escape. We limited the ghost chase to stay on the floor and not in the air. 




