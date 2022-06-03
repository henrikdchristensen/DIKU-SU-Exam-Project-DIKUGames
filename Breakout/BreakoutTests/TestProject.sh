#!/bin/sh
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:$(gci TestResults -r -fi coverage.cobertura.xml | % { $_.FullName }) -targetdir:Report -reporttypes:Html