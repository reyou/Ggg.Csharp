/* A) This time, the path consists of three steps. 
The path object contains the labels 'start' and 'move', 
as we have assigned these labels to the first and third step of our traversal. */
// Using as(x), we can assign the label x to the previous step. 
g.V('Benjamin').as('start').out().out().as('move').path().limit(1);
// B) The path object contains a vertex as well as an edge.
g.V('Ron').in().outE().path();
