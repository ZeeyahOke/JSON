# REST API with JSON

A Unity app that fetches player data from a REST API, deserializes the JSON, and displays it in a clean UI.

## What it does

On launch, the app pulls player data from a JSONbin.io endpoint and shows it on screen — name, level, health, position, and a scrollable inventory list.

Two buttons:
- **Refresh** — re-fetches the data from the API
- **Sort** — toggles the inventory between alphabetical order and quantity order

## The API

Endpoint: `https://api.jsonbin.io/v3/b/6686a992e41b4d34e40d06fa`

Returns player data wrapped inside a `record` object, plus metadata about the bin.

## Project structure

```
Assets/Scripts/
├── Models/
│   ├── ApiResponse.cs
│   ├── Metadata.cs
│   ├── PlayerData.cs
│   ├── Position.cs
│   └── InventoryItem.cs
├── ApiService.cs
├── UIDisplay.cs
└── IPlayerDisplay.cs
```

- **Models** hold the shape of the JSON data.
- **ApiService** handles the network request and deserializes the response.
- **UIDisplay** listens for data and renders it on the Canvas.

The service and UI talk to each other through C# events, so they stay independent.

## Running it

Open the project in Unity 6, load `GamePlay`, press Play.
