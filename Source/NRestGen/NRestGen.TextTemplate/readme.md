# NRestGen TextTemplate

The resource model and settings for generating the code are all stored in a yaml file.

## Entities

```yaml
entities:
  - name: Customer
    set_name: Customers
    properties:
      - name: CustomerId
        type: int
      - name: Name
        type: string
      - name: Orders
        type: List<Order>
  - name: Order
    set_name: Orders
    properties:
    - ...
```

## Settings

Values and flags that impact how the code is generated are listed under `settings`.

### OData

OData support gives the client the possibility to tweak the request in order to receive a more focused response.

```yaml
settings:
  odata:
    queryable: true
    select: true
    filter: true
    expand: 1
    count: true
    max: 100
```

Setting | Values | Description
--|--|--
queryable | true \| false | Use `[EnableQuery]` and `IQueryable` on the Controller Actions.
select | true \| false | Enable (true) selecting fields option.
filter | true \| false | Enable (true) filtering on fields values option.
count | true \| false | Enable (true) the count option.
expand | uint | Set how many levels deep an expand may navigate (0 = off).
max | uint | Set the maximum number of records returned from an Action.

### MediatR

MediatR is a small and simple library that decouples request/response handling from the MVC API framework. It implements a pipeline (chain of responsibilities) architecture and allows for easy extensibility of cross-cutting concerns during processing.

```yaml
settings:
  mediatr:
    register_assembly: true
```

Setting | Values | Description
--|--|--
register_assembly | true \| false | Uses `typeof(Startup).Assembly` to register all MediatR objects in the main web project assembly.
