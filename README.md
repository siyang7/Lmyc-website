# LMYC &nbsp;&nbsp;&nbsp;![Build Status](https://travis-ci.org/LMYC/Lmyc-website.svg?branch=develop)

![LMYC Logo](/images/logo.png)
The client, Mr. Dave Senkler, wishes to migrate functionality for booking boats from the old site to the new site. it was decided to create
an API/JavaScript solution that can be injected into the new WordPress site.


## Getting Started
To Run this:
1. Restore dependencies (`dotnet restore`)
2. Build .NET project (`dotnet build`)
3. Publish .NET project (`dotnet publish -o dist`)
4. Start containers (`docker-compose -f docker-compose.yml up`)

### Prerequisites
```
Docker
```

### Useful Command
View all your containers (`docker ps -a`)

Shut down a container (`docker stop [containerId]`)

Remove a container (`docker rm [containerId]`)

### Git Control
Fork the github repository to your own repository.

Follow the following to submit a pull requests
1. Create a feature branch on your own repo (`Git branch {Team}-{label}-name-of-your-branch`)
2. Switch to feature branch (`Git checkout name-of-your-branch`)
3. Make changes as your assigned (`Remember update changelog.md`)
4. Stash your changes (`git stash`)
5. Sync your local repo with team develop (`git pull upstream develop`)
6. Apply the stashed changes (`git stash apply`)
7. Resolve any merge conflicts
8. Commit your change with signing (`git -S -m "Commit messages`)
9. push your local repo to your personal repo (`git push --set-upstream origin {feature/fix}-name-of-your-branch`)
10. Create a pull request on github from your

### Coding style

Comment everything with C # comment
follow the comment standard on [Document your code with XML comments](https://docs.microsoft.com/en-us/dotnet/csharp/codedoc)

## Testing

Uses VSTS for testing with automatic testing cases

## Deployment

Uses Azure for deployment

## Built With

* [ASP.NET](https://www.asp.net/) - The web framework used
* [Anguler.Js](https://angular.io/) - Main front-end language used

## Reference

* [Old Website](http://www.lmyc.ca/)
* [WordPress Site](http://www.sailwhiterock.com/)
* [Docker Tutorial](https://docs.docker.com/get-started/#test-docker-version)
