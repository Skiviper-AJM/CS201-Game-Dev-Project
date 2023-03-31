Arctic Beat
Integrated Design Document


Developed by Adam McArdle
 
(Screenshot from past game jam project used for inspiration, Penguns)
  



(Game was co-developed and designed by me, UI was by me, enemy assets were by co-creator, used for this project with permission)


































Table of contents








Executive Summary .. 3
        
Project Parameters .. 5


Gameplay Overview .. 8
        
UI .. 10


Lo-fI … 11
        
References.. 14
        
________________
Executive Summary


Mission Statement;


My goal is to produce a game with great replayability that is simple to pick up but difficult to master. Towards this end I plan on revisiting an old concept I made for a 48 hour game jam in the past, with a unique new twist. This concept plays upon established industry standards for high speed, reaction based bullet hell games with responsive controls and simple mechanics. The twist being, instead of just a bullet hell like my last project, it is also a rhythm game.


Bullet hells draw their difficulty from reaction based gameplay, if the player can’t learn to keep up with what is happening visually, then they can’t finish or continue the game. My game will differ from this, and instead of using just visuals to respond to in-game actions, everything will be tied to a beat, things may only proceed 1 cell at a time, or perform any actions such as firing, on the beat. This also applies to players, they may only move, shoot, or catch a (color coded) projectile on the beat. 


In this way, I reduce the reliance on reaction speed by making everything move in a predictable and learnable pattern, essentially splitting the difference between a completely reaction focused game driven by impulse and a more calm and focused game designed to make you consider your next move carefully.


Unique Selling Points;


As mentioned, the uniqueness of Arctic Beat is that it bridges two seemingly opposed genres in order to produce a fresh and equally engaging middle ground between impulse and strategy. Most rhythm games have you reacting solely to audio cues, while most bullet hells have you reacting solely to the visual. By introducing the “Beat Lock” mechanic, the game only updates on the beat, so predicting the path of enemies, or projectiles, is as simple as mastering the current game rhythm, and landing shots is much the same.


There will also be some, if minimal, lore elements that bring together a engaging enough world to keep the players interested while they explore procedurally generated levels and defeat bosses in a set order.




















Brief Gameplay Overview;


Arctic Beat’s gameplay will play out over the course of floors, made of a set amount of premade rooms, each room will have premade enemy encounters with hazards the player must overcome by dodging their projectiles, catching color-coded projectiles, and using them to fire back at and defeat the enemies. Each “floor” will also have one boss encounter the players must defeat before advancing to the next floor. Each floor will have it’s own rhythm, with boss encounters being sped up or more erratic.


Players will have 10 hit points spread across 5 hearts, some projectiles will deal half a heart, some will deal a full heart, the projectiles that deal a full heart can also be “caught” using the bucket, once a projectile is “caught”, it will be added to the ammunition count, which maxes out to 5, and may be fired back at an enemy at any time. In this way, players are made to carefully consider their next actions and manage their ever dwindling hearts and their limited ammunition. Health is fully restored after defeating each boss, with bonus rewards for clearing a boss without losing any health.


Hyperclick will take place in a single rectangular room, with a stationary boss in the center. The gameplay loop will involve clicking targets as they appear around the room, while bullets slowly fire from the boss. Clicking a target will increase the action bar, while getting hit by a bullet or missing a target will decrease it. If the bar hits 0 the player dies, and if it reaches a certain threshold the player will advance to the next level, gaining points and increasing the difficulty. Occasionally, the boss will also enter an attack phase, where targets appear much more infrequently, but bullets are fired much faster.


The goal of this game will be to escape all the floors, of which 5 are planned, and leave the iceberg. The name for the setting the games takes place in.


Story;


There will be no dialogue of any kind, nor any items to pick up or random text, all story elements will be told visually via set and enemy design. The players will begin in the first room of the first floor, if they are yet to die or escape (the game keeps track) the game will assume this is their first time playing, and a brief tutorial with messages scribbled on ice explaining gameplay mechanics, will play out before they are dropped to the first level.


The tutorial will be one of a handful of visual story-telling elements, and will change as the players make progress. It may be revisited at any time. For examples of where I draw this design philosophy from, Miura, the late author of berserk, mastered this design philosophy and was capable of producing entire chapters with minimal dialogue. This then inspired the popular dark souls games both visually and narratively.






Project Parameters


Constraints;


Since the developer will have minimal time and resources, asset reuse from a previous project, with permission from the assets creator, will be used as a way of shrinking development time and maintaining design consistency. Gameplay mechanics will also be kept as simple to use and implement as possible, there will be no multiple difficulties or multiplayer, no upgrades of any sort that directly affect gameplay and no enemy varieties with mechanics that alter in game mechanics.


Project Duration


29/2/23 - 21/4/23 (roughly 8 weeks)


Alpha Due


10/4/23, this gives me a week and change to fix any severe issues.


Game Engine


Unity 2021 LTS, this allows for best hardware compatibility and makes programming on multiple machines easier.


Target Platform


Windows PC’s


Minimum Hardware specs;
 
OS: Windows 7 or later
Processor: Intel Core 2 Duo E6320 (2*1866) or equivalent
Memory: 2 GB RAM
Graphics: GeForce 7600 GS (512 MB) or equivalent
Storage: 2 GB available space


Primary Programming Language


C#, unities native language.










Team Size


Project lead, developer, UI, UX and asset development - Adam McArdle


Also credits to;
Enemy and player asset creator (Partial) - Dodecahedron#6045 (Opted to go by discord     pseudonym)


Project Methodology & Frameworks


I will be using the agile methodology for the fastest possible turnover as well as maximizing what I can produce with such a tight timeframe, the focus being on producing something functional with at least some of the planned gameplay aspects present.


There will be no other frameworks - aside from Unity itself - unless a trello board for bug and feature tracking can be counted, due to the simplicity of the project and the tight deadline forcing the most time-optimal workflow possible.


Budget 


All assets have been either procured from prior projects with permission, produced in-house, or acquired from the Unity Store’s vast array of free assets. Music will be sourced from copyright free / non-dmca sources. 


Assuming a 15 hour timeframe to produce any art required, and a generous 80 hour timeframe to program and produce this. Being 4 hours per day for 20 days on just the programming aspect, and averaging the total pay a junior dev and asset developer receive to about 30 NZD per hour, the project will cost roughly 2700 new zealand dollars in wages alone to produce.


Software Used


Unity Game Engine (Development Platform)
Aseprite (Art)
Paint.net (Art)
Github Desktop /Gitkraken (Backups and Version Control)
Visual Studio and VS Code (IDE used by unity and also what has been previously taught)


Target Audience


The games audience will be PC gamers in the age range of 14-30, primarily those who keep up with and play popular games, such as enter the gungeon or crypt of the necrodancer. The reason for this is because while it curbs the difficulty aspect by combining the gameplay of these two games, it will still be a fairly intense experience your casual mobile game player would have difficulty staying engaged with. It will also be assuming mastery over keyboard and mouse controls. 


  

(Enter the gungeon)
  

(Crypt of the necrodancer)




Gameplay Overview


Core Functionality


Arctic beat is focused primarily on its rhythm mechanics, with everything being tied to a beat, with a visual indicator in addition to the actual audio cue, showing when the next beat will be. If a player attempts to act outside of the beat, they “skip a beat” and cannot move or act until the next beat. In this way the player has to pay attention to what is happening on screen AND the sounds coming in through their headset/ speakers in order to play effectively.


The game will otherwise draw direct inspiration from the bullet hell genre, with procedural room generation and diverse enemy variety. The draw being the refreshing middle ground between the high intensity skill based gameplay elements and the more strategic gameplay elements.




The Beat


As mentioned, this mechanic will control everything; enemies, players and projectiles alike. Either may only perform one action on the beat, be it moving, shooting or performing another special action in the case of bosses. 


Movement


Every enemy as well as the player has the option to move ONE tile on the beat. This also applies to projectiles. Diagonal movement across cells will not be allowed.


Shooting


As mentioned, enemies and players may shoot projectiles that move one tile per beat in a straight line from where they are shot, enemy projectiles are red, and player projectiles are blue. Some enemy projectiles will be green however, which ties into the next mechanic discussed. Players will only have 5 ammunition at a time. Enemies (except bosses) may only have 1 active bullet at a time.


Catching


This mechanic is unique to players, while enemies can choose to either move or shoot on the beat, players have an extra option - catching. By using their beat action they can catch color coded green projectiles, adding them to the players ammunition count which allows them to defeat enemies as well as being used to avoid otherwise undodgeable attack patterns. The game will also attempt to produce a green projectile if it deems the player cannot dodge the current wave of bullets.




Health and ammo


Over the course of the game the player will be tasked with managing 2 resources, ammunition and health. As discussed, ammunition may be refilled by performing the catch action. Health on the other hand is only ever lost, losing half a heart when struck by a red projectile, or one full heart when struck by a green one (essentially when failing a “catch”). Players begin each floor with 5 hearts.


Story


As discussed, the story will be entirely visual storytelling, the enemies will progress visually, and may appear as the penguins natural predators, or something more unseemly. This particular design aspect is in flux as well as being optional, as such it will be determined by the rate of development. 


Schedule


The following are my intended deadlines, some of which have already been met or exceeded. In green are completed features and red are ones taking longer than intended;






Feature
	Deadline
	Urgency
	Movement
	10/03
	High
	Assets
	15/03
	Medium
	Bullets
	17/03
	High
	Enemies
	20/03
	High
	Beat
	28/03
	High
	Projectile catch
	30/03
	Medium
	Health and Ammo
	3/04
	Medium
	Procedural rooms
	8/04
	Low
	Boss mechanics
	10/04
	Low
	











UI


Philosophy


Bullet hell games and rhythm games have to have simple and easy to understand UI designs, with large bulky icons and intuitive designs. An example being hearts that represent health, when hit they partially, or fully, gray out, communicating to the player how much health they have remaining without having to explain to the player what it is. There will be no inventory menu or pause menu, so the only 2 UI sets will be the main menu as well as the death screen, both of which will be designed in such a way the player understands what they’re looking at


Sketch
Attached is a rough pre lo-fi sketch of the few UI screens to be implemented in the game.
  



 




























  



________________




Lo-fi
Start Menu


This screen is shown when the game is first started up and whenever the player continues after dying, it has a sub menu pictured below it that appears when the quit button is pressed, the no option closes the software and the yes option returns the player to the previous menu.
  

  

 
Gameplay / HUD


The screen Shows only two things during gameplay, health and ammunition, both tucked cleanly into each bottom corner out of the way. The rest of the white square will entirely be the gameplay area, with the player typically centered on the screen.
  

 
________________


Death
On Death the screen will blank out, a death sound will play and the player will be presented with this screen, any input will direct the player back to the main menu screen.
  





 
________________


References
Windows minimum specs; Steam page for enter the gungeon
Enter the gungeon itself; https://store.steampowered.com/app/311690/Enter_the_Gungeon/
Crypt of the necrodancer; https://store.steampowered.com/app/247080/Crypt_of_the_NecroDancer/




Software
Unity - unity.com
Aseprite - aseprite.org
Visual Studio +  VS Code - visualstudio.microsoft.com
Github Desktop - github.com
Gitkrakken - https://www.gitkraken.com/