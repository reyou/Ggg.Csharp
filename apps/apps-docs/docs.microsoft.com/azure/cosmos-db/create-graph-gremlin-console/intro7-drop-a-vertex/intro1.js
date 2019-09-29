// Let's now delete a vertex from the graph database.
// Input (drop Jack vertex):
g.V()
  .hasLabel("person")
  .has("firstName", "Jack")
  .drop();
