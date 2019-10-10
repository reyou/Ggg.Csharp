// It is very likely that the result contains some duplicates. 
// If Celia knows two people a, b and both a, b know person c, 
// then c will be listed twice. We can use dedup to de-duplicate them:
g.V('Celia').out().out().dedup().id();
