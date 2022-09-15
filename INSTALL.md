## Clone and install dependencies

```bash
# Clone repository
$ git clone https://github.com/gabimariz/ChatPipoca.git

# Open project folder
$ cd ChatPipoca

# Install dotnet-ef global
$ dotnet tool install --global dotnet-ef

# Use package manager dotnet to restore packages
$ dotnet restore

# Install dependencies client
$ cd ClientApp
$ npm install

# Create docker image
$ cd ..
$ docker-compose up -d
```
