# ActionsMinUtils

This is a very small set of utilities for working with [.NET Core][dotnet] based [GitHub Actions][actions].

This library is intended particularly at making it easier to [build Docker container Actions][docker-container-action].

It contains utilities for:

- logging
- dealing with inputs and environment variables
- talking to GitHub APIs (with retry)

## `Logger`

The [`Logger`](./ActionsMinUtils/Logger.cs) class is a simple wrapper around the `System.Console` class. It provides a few convenience methods for logging messages to the console in a way Actions can understand.

## `ActionContext`

The [`ActionContext`](./ActionsMinUtils/ActionContext.cs) class provides methods for reading and validating both environment variables and inputs. As a matter of fact, to avoid having to deal with container `args`, inputs are read from environment variables!

## `github`

This namespace provides various GitHub specific utilities.

### `DefaultVariables`

The [`DefaultVariables`](./ActionsMinUtils/github/DefaultVariables.cs) class defines useful constants for referring to default environment variables exposed to Actions runners.

### `GitHub`

The [`GitHub`](./ActionsMinUtils/github/GitHub.cs) class defines two clients for talking to GitHub APIs, Rest and GraphQL.

## `templating`

### `MarkersParser`

The [`MarkersParser`](./ActionsMinUtils/templating/MarkersParser.cs) class provides utilities for parsing and formatting simple [`Markers`](./ActionsMinUtils/templating/Marker.cs). They are meant to be one-liner embedded in various text files (Markdown, YAML, etc.).

### `Templates`

The [`Templates`](./ActionsMinUtils/templating/Templates.cs) class provides basic templating capabilities using [Liquid][liquid] to parse templates from string or files. It comes with basic support for [`Markers`](./ActionsMinUtils/templating/Marker.cs) too and can be used to render arbitrary supplied variables.

<!-- Refs -->

[dotnet]: https://dotnet.microsoft.com
[actions]: https://docs.github.com/en/actions
[docker-container-action]: https://docs.github.com/en/actions/creating-actions/creating-a-docker-container-action
[liquid]: https://shopify.github.io/liquid/basics/introduction/