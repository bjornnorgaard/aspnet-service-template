# ANT
ASP.NET Template.

```mermaid
flowchart
    gra[Grafana]
    otel[Collector]
    api[Todos API]    
    aspire[Aspire Dashboard]
    prom[(Prometheus)]
    db[(Todos DB)]

    otel --> prom
    otel --> aspire
    api --> otel
    api --> db
    gra --> prom
```
