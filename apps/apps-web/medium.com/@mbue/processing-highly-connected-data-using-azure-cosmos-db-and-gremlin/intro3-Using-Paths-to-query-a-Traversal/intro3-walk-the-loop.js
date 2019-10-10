/* So far, we explicitly expressed every single step within a traversal.
Using repeat , we can enrich queries with various loops.
For example, we already executed the following query: */
g.V('Ron').out().out().id();
// We get the same result by repeating the out statement using a loop:
g.V('Ron').repeat(out()).times(2).id();
