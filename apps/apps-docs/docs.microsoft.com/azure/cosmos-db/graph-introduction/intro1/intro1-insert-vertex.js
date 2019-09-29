// First let's look at CRUD. The following Gremlin statement inserts the "Thomas" vertex into the graph:
g.addV("person")
  .property("id", "thomas.1")
  .property("firstName", "Thomas")
  .property("lastName", "Andersen")
  .property("age", 44);
