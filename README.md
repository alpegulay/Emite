# Emite
## _Alpe Nino L. Gulay_

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Building a Call Center Management API. 
This is still a half bake implementatio. Been so busy lately and could not spend good time on this. But never the less, I am sending my solution for the team to check.


## Installation

This project requires [.net6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) to run.

## Database and Migration

In the interest of time, the database use on this is just In Memory. 
So no need to problem about the migration. But it's design to easily inject with preffered DB as the business requires.

## Authorization

Run out of time on implementing this. It's just a half-bake implementation

## API Access

Can be access through swagger or the bruno configuration

## Assumption & Explanation

The solution was designed with N-Tier architecture approach. Basically 3 project level are introduce. 

1 - API : Entry point of the API, This is where the Controller resides

2- Core - Business Layer project. This is where the business logic and validation resides. This is also the representation of the Outside world model.

3- Data - This is where the data-access layer resides. This is the representation of the Backend DB entities. From this, we can control the type of DB we need to the project.

## Bruno collection

Emite.rar
