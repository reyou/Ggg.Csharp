// The following query returns the "person" vertices in descending order of their first names:.

g.V()
  .hasLabel("person")
  .order()
  .by("firstName", decr);
