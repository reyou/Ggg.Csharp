// Next, let's project the first name for the people who are older than 40 years old.
// Input (filter + projection query):

g.V()
  .hasLabel("person")
  .has("age", gt(40))
  .values("firstName");
