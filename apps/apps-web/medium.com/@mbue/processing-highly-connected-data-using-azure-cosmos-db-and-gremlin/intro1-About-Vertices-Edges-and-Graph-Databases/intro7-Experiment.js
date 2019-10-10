// A) Retrieves incoming edges of the API Gateway
// There are sources [lives], [likes], [works] in Austin.
g.V('API Gateway').inE();

// B) Retrieves the source vertices of the incoming edges of the API Gateway
// Sources are [Alice], [Bob] and [Charlie]
g.V('API Gateway').inE().outV();

// C) Retrieves both incoming and outgoing edges of the API Gateway
// There are sources [lives], [likes], [works] in Austin.
// Austin [located], [officeConnection], [hasAirlines]
g.V('API Gateway').bothE();

// D) Counts the incoming ‘connects’-edges of the API Gateway
// 150 total connections (edge) done to "API Gateway"
g.V('API Gateway').inE().hasLabel('connects').count();

// E) Traverses from the Web App to the target vertices and get their ID
// [200] ids has been collected.
g.V('Web App').outE().inV().id();