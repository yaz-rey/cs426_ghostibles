Sofia Landeta, Omar Flores, Yazmin Reyes: Ghostibles
		
Our Design
-------------------------------------------------------------------------- 
We created a Victorian mansion-inspired haunted house that has lurking
lost ghosts with gems to collect. It has an overall dark earth toned theme 
to add to the atmosphere’s eeriness. We wanted the ghosts to be 
threatening so they have more scary features in a disheveled home. 
The main player must navigate through the moving ghosts and relentless
boss in order to collect the gems. 

3D Physics
-------------------------------------------------------------------------- 
- Hinge door when entering 
  This primarily serves as an introductory scene to prepare the player for
  the level that they are about to endure. You are able to swing it open 
  to first introduce yourself to the home and use the game mechanics. 
  
- Particle system fireplace
  The fireplace was added into the living space area to indicate that 
  though the home is not inhabited by human life, it is not empty. The
  home is instead occupied by others and lets the main player know they 
  are not alone. 

- Collider with Ghost “Field of View” 
  There is an invisible plane that is attached to the Ghost character. 
  Its main purpose is to serve the gameplay. When the main player collides
  with its plane collider, this will indicate to the ghost that the player 
  is in reach of attacking and to trigger the chase. 
  
Lights
-------------------------------------------------------------------------- 
- Fire in fireplace
  This provides warmth to the grimly themed home. If going toward the 
  light, it could also potentially play as a trap to any upcoming 
  traveling ghosts. 

- Indoor wall lights
  This provides warmth to the grimly themed home. It also adds to the idea
  that although the home appears vacant of human life, it is not empty. 
  Different utilities like lights are being utilized by the ghost 
  inhabitants.

- Candle light
  This provides warmth to the grimly themed home. It also adds to the idea
  that although the home appears vacant of human life, it is not empty. 

Sounds
-------------------------------------------------------------------------- 
- Ghost hunter shooting
  This confirms to the player that the shoot attack they utilized did 
  occur by playing a gun shot sound. 
  
- Ghost hunter playing music
  This confirms to the player that the music attack they utilized did 
  occur by playing music.
  
- Ghost scream 
  To add onto the atmosphere’s overall feeling of uneasiness, screams 
  were added when a player is nearby a ghost. 

- Door sound
  This is to confirm to the player that they will be entering the level
  and be introduced to the inhabitants. 

Textures
-------------------------------------------------------------------------- 
Textures are utilized throughout the entire map in order to fit the 
Victorian mansion aesthetic. We highlight the following: 

- Walls
  The walls are decorated with a dark green texture. This texture largely
  contributes to the bleak atmosphere to emphasize the threat of the 
  ghosts that exist in the space. 

- Rugs
  The rugs have a variety of textures that match the Victorian style 
  decor. They provide potential paths to the player to follow in order to
  move closer in collecting the gems and to navigate the layout of the 
  level. 
  
- Gem & Ammunition
  Both these items have a vibrant texture in comparison to the rest of the
  home. This is to clearly indicate to the player that these items should 
  stand out and to go towards them. This will either increase the score
  or increase the amount of bullets the player has. 
  

AI Techniques 
-------------------------------------------------------------------------- 
- Finite State Machine in Ghost (Yazmin):
  The FSM within the game primarily applies to the normal level ghosts. 
  There is an idle state, an attack state, and a stun state. These states
  are triggered based off the actions of the player: they are within the
  field of view of the ghost, they choose to stun the ghost. 

- Waypoints in Ghost (Sofia):
  The waypoints on the map are for the normal level ghosts to patrol the
  home. They are not prone to one area but all of them are protective of 
  all parts in the home. This becomes an obstacle for the player to not
  be seen by patrolling ghosts. 

- Navmesh in Boss (Omar):
  Once a gem is collected, the boss level ghost appears on the map. His 
  only goal is to destroy you, so he will continue to chase after you 
  until you defeat him. This is achieved by the boss level ghost to have
  knowledge of the map 

Mecanim
-------------------------------------------------------------------------- 
Ghost hunter (Amy):
- Idle (Yazmin) 
  This notifies the player of the waiting state. 
- Turn Left (Sofia) 
  This notifies the player of the turning state. 
- Turn Right (Sofia)
  This notifies the player of the turning state. 
- Shoot (Yazmin)
  This notifies the player of the shooting attack. 
- Play Music (Yazmin) 
  This notifies the player of the music attack. 

Ghost:
- Idle (Yazmin) 
  This notifies the player of the waiting state. The position is 
  confrontational. 
- Attack (Yazmin) 
  This notifies the player of the attack state. The animation is
  aggressive and clearly indicates to the player that they are not wanted. 
- Stun (Yazmin) 
  This notifies the player of the stunned state. 

Boss:
- Idle (Omar)
  This notifies the player of the waiting state. 
- Walk (Omar) 
  This notifies the player of the walking state. The character has a limp
  and is larger than the other characters. He poses more of a threat. 
