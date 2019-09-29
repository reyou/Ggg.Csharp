// Where graphs shine is when you need to answer questions like
// "What operating systems do friends of Thomas use?".
// You can run this Gremlin traversal to get that information from the graph:
g.V("thomas.1")
  .out("knows")
  .out("uses")
  .out("runsos")
  .group()
  .by("name")
  .by(count());
