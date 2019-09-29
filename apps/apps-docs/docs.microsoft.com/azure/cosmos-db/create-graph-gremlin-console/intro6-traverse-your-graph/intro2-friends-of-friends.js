// Next, let's get the next layer of vertices.
// Traverse the graph to return all the friends of Thomas's friends.
// Input (friends of friends of Thomas):
g.V()
  .hasLabel("person")
  .has("firstName", "Thomas")
  .outE("knows")
  .inV()
  .hasLabel("person")
  .outE("knows")
  .inV()
  .hasLabel("person");
