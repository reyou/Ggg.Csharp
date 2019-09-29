// Next, the following Gremlin statement inserts a "knows" edge between Thomas and Robin.

g.V("thomas.1")
  .addE("knows")
  .to(g.V("robin.1"));
