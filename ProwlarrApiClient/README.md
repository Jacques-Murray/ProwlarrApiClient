# Prowlarr API Client for .NET

[![NuGet Version](https://img.shields.io/nuget/v/JacquesMurray.ProwlarrApiClient.svg)](https://www.nuget.org/packages/JacquesMurray.ProwlarrApiClient/)
[![License](https://img.shields.io/nuget/badge/License-MIT-blue.svg)](LICENSE) A modern, asynchronous C$ client library for interacting with the [Prowlarr](https://prowlarr.com/) API (v1). Built with best practices, providing a clean and efficient way to manage your Prowlarr instance programmatically.

## Features

* **Asynchronous:** Fully `async`/`await` based for non-blocking I/O operations.
* **Strongly-Typed:** Includes models for API responses, reducing runtime errors.
* **API Coverage:** Implements key Prowlarr v1 API endpoints (System, Indexers, Applications). See [Implemented Endpoints](#implemented-endpoints).
* **Error Handling:** Uses custom `ProwlarrApiException` for clear feedback on API errors.
* **Clean Design:** Follows OOP principles for maintainability and ease of use.
* **Extensible:** Designed to be easily extended to support more API endpoints.
* **NuGet Ready:** Includes project configuration for straightforward NuGet packaging.

## Installation

The easiest way to install the library is via NuGet Package Manager.

```powershell
Install-Package JacquesMurray.ProwlarrApiClient
```

Or via the .NET CLI:

```bash
dotnet add package JacquesMurray.ProwlarrApiClient
```

## Usage

Instantiate the `ProwlarrClient` with your Prowlarr instance URL and API key.

```C#
using System;
using System.Threading.Tasks;
using JacquesMurray.ProwlarrApiClient;
using JacquesMurray.ProwlarrApiClient.Models; // Required for model types

public class ProwlarrExample
{
	
}
```