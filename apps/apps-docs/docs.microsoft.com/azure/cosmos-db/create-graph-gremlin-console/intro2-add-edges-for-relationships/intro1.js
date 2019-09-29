g.V()
  .hasLabel("person")
  .has("firstName", "Thomas")
  .addE("knows")
  .to(
    g
      .V()
      .hasLabel("person")
      .has("firstName", "Mary Kay")
  );
g.V()
  .hasLabel("person")
  .has("firstName", "Thomas")
  .addE("knows")
  .to(
    g
      .V()
      .hasLabel("person")
      .has("firstName", "Robin")
  );
g.V()
  .hasLabel("person")
  .has("firstName", "Robin")
  .addE("knows")
  .to(
    g
      .V()
      .hasLabel("person")
      .has("firstName", "Ben")
  );
