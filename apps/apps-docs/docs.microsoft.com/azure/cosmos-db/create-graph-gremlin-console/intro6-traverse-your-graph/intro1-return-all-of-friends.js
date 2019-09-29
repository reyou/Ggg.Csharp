// Let's traverse the graph to return all of Thomas's friends.
// Input (friends of Thomas):
g.V()
  .hasLabel("person")
  .has("firstName", "Thomas")
  .outE("knows")
  .inV()
  .hasLabel("person");
