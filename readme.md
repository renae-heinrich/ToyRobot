Toy Robot

Completed in C#

The application is a simulation of toy robots moving on a square tabletop, of dimensions 5 units x 5 units.

Robots are free to roam around the surface of the table, but must be prevented from falling to destruction and colliding with another robot. Any movement that would result in the robot falling or colliding will be ignored, however further valid movement commands are still be allowed.

 - First command must be PLACE will put a robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST. 

   eg PLACE 0,0,NORTH


- MOVE will move the toy robot one unit forward in the direction it is currently facing.


- LEFT and RIGHT will rotate the robot 90 degrees in the specified direction without changing the position of the robot.


- Input is from the console.


- Multiple robots can operate on the table.


- PLACE will add a new robot to the table with incrementing number identifier, i.e. the first placed robot will be 'Robot 1', then the next placed robot will be 'Robot 2', then 'Robot 3', etc.


- REPORT will report on how many robots are present and which robot is active


- ROBOT <number> command will make the robot identified by active. Subsequent commands will affect that robot's position/direction. Any command that affects position/direction (e.g. MOVE, LEFT, RIGHT...) will affect only the active robot.