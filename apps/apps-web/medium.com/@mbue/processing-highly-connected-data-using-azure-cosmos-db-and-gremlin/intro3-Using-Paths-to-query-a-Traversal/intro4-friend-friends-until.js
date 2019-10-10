// Let’s take a look at another example. 
// Before we express some query, we assign a unique property to one of Celia’s friend-friends:
g.V('Celia').out().out().limit(1).property('city', 'Speyer');

// The next query searches a path from the Celia vertex to another 
// vertex that has the property “city = Speyer”. The query above ensures that we find a match:
g.V('Celia').repeat(out()).until(has('city', 'Speyer')).path().limit(1);