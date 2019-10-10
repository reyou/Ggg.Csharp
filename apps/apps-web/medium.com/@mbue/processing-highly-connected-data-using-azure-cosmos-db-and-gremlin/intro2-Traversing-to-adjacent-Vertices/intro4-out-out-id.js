// Let’s traverse one step further and list the names 
// of all the people that are known to all the people known to Celia:
g.V('Celia').out().out().id();
