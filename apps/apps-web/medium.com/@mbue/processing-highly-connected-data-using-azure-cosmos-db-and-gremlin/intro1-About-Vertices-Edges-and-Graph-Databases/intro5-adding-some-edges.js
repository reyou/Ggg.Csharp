g.V('Web App').addE('connects').to(g.V('API Gateway'));
g.V('Mobile Device').addE('connects').to(g.V('API Gateway'));
g.V('API Gateway').addE('connects').to(g.V('Storage Service'));
