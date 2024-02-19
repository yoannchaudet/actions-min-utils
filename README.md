# ActionsMinUtils

This is a very small set of utilities for working with [.NET Core][dotnet] based [GitHub Actions][actions].

This library is intended particularly at making it easier to [build Docker container Actions][docker-container-action].

## Logger

The [`Logger`](./ActionsMinUtils/Logger.cs) class is a simple wrapper around the `System.Console` class. It provides a few convenience methods for logging messages to the console in a way Actions can understand.

## ActionContext

The [`ActionContext`](./ActionsMinUtils/ActionContext.cs) provides methods for reading and validating both environment variables and inputs. As a matter
of fact, to avoid having to deal with container `args`, inputs are read from environment variables!

<!-- Refs -->
[dotnet]: https://dotnet.microsoft.com
[actions]: https://docs.github.com/en/actions
[docker-container-action]: https://docs.github.com/en/actions/creating-actions/creating-a-docker-container-action